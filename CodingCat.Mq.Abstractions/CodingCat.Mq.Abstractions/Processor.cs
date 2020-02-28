using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodingCat.Mq.Abstractions
{
    public abstract class Processor
    {
        public ILogger Logger { get; set; }
        public TimeSpan Timeout { get; set; }

        public bool IsTimeoutEnabled => this.Timeout.TotalMilliseconds > 0;

        public virtual void OnProcessError(Exception ex)
        {
            this.Logger?.LogError(ex, "");
        }

        protected void Process(Action action)
        {
            var notifier = new AutoResetEvent(false);
            Task.Run(() =>
            {
                try
                {
                    if (action == null)
                        this.Logger?.LogWarning($"{nameof(action)} is null");

                    action?.Invoke();
                }
                catch (Exception ex)
                {
                    this.OnProcessError(ex);
                }
                notifier.Set();
            });

            this.WaitFor(notifier);
        }

        protected void WaitFor(EventWaitHandle notifier)
        {
            if (this.IsTimeoutEnabled)
                using (notifier)
                    notifier.WaitOne(this.Timeout);
            else
                using (notifier)
                    notifier.WaitOne();
        }
    }

    public abstract class Processor<TInput> : Processor
    {
        #region Constructor(s)

        public Processor() : base()
        {
        }

        #endregion Constructor(s)

        public virtual void HandleInput(TInput input)
        {
            this.Process(() => this.Process(input));
        }

        protected abstract void Process(TInput input);
    }

    public abstract class Processor<TInput, TOutput> : Processor
    {
        public TOutput DefaultOutput { get; set; } = default(TOutput);

        #region Constructor(s)

        public Processor() : base()
        {
        }

        #endregion Constructor(s)

        public virtual TOutput HandleInput(TInput input)
        {
            var output = this.DefaultOutput;

            this.Process(() => output = this.Process(input));

            return output;
        }

        protected abstract TOutput Process(TInput input);
    }
}