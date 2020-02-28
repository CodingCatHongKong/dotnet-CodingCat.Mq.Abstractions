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

        public virtual void OnProcessError(Exception ex)
        {
            this.Logger?.LogError(ex, "");
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
            var willTimeout = this.Timeout.TotalMilliseconds > 0;
            var notifier = new AutoResetEvent(false);

            if (willTimeout)
            {
                Task.Delay(this.Timeout)
                    .ContinueWith(task => notifier.Set());
            }

            Task.Run(() =>
            {
                try { this.OnInput(input); }
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