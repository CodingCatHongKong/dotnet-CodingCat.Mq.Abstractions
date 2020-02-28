using System;

namespace CodingCat.Mq.Abstractions.Tests.Impls
{
    public class SimpleProcessor<T> : Processor<T>
    {
        public delegate void ProcessDelegate(T input);

        public delegate void OnProcessErrorDelegate(Exception ex);

        public ProcessDelegate ProcessHandler { get; }
        public OnProcessErrorDelegate ProcessExceptionHandler { get; }

        #region Constructor(s)

        public SimpleProcessor(
            ProcessDelegate processHandler,
            OnProcessErrorDelegate processExceptionHandler = null
        )
        {
            this.ProcessHandler = processHandler;
            this.ProcessExceptionHandler = processExceptionHandler;
        }

        #endregion Constructor(s)

        public override void OnProcessError(Exception ex)
        {
            this.ProcessExceptionHandler?.Invoke(ex);
            base.OnProcessError(ex);
        }

        protected override void Process(T input) => this.ProcessHandler(input);
    }

    public class SimpleProcessor<TInput, TOutput>
        : Processor<TInput, TOutput>
    {
        public delegate TOutput ProcessDelegate(TInput input);

        public delegate void OnProcessErrorDelegate(Exception ex);

        public ProcessDelegate ProcessHandler { get; }
        public OnProcessErrorDelegate ProcessExceptionHandler { get; }

        #region Constructor(s)

        public SimpleProcessor(
            ProcessDelegate processHandler,
            OnProcessErrorDelegate processExceptionHandler = null
        )
        {
            this.ProcessHandler = processHandler;
            this.ProcessExceptionHandler = processExceptionHandler;
        }

        #endregion Constructor(s)

        public override void OnProcessError(Exception ex)
        {
            this.ProcessExceptionHandler?.Invoke(ex);
            base.OnProcessError(ex);
        }

        protected override TOutput Process(TInput input) =>
            this.ProcessHandler(input);
    }
}