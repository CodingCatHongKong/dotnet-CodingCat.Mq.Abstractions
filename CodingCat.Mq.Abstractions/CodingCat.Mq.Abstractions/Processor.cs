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

        public virtual ManualResetEvent GetProcessedNotifier()
        {
            var notifier = new ManualResetEvent(false);
            if (this.IsTimeoutEnabled)
            {
                Task.Delay(this.Timeout)
                    .ContinueWith(task => notifier.Set());
            }
            return notifier;
        }
    }

    public abstract class Processor<TInput> : Processor
    {
        #region Constructor(s)

        public Processor() : base()
        {
        }

        #endregion Constructor(s)

        public virtual void Process(TInput input)
        {
            var notifier = this.GetProcessedNotifier();

            Task.Run(() =>
            {
                try
                {
                    this.OnInput(input);
                }
                catch (Exception ex)
                {
                    this.OnProcessError(ex);
                }
                notifier.Set();
            });

            using (notifier) notifier.WaitOne();
        }

        protected abstract void OnInput(TInput input);
    }
}