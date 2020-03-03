using System;

namespace CodingCat.Mq.Abstractions.Tests.Impls
{
    public class SimpleProcessor<T> : BaseDelegatedProcessor<T>
    {
        public delegate void OnProcessErrorDelegate(Exception ex);

        public OnProcessErrorDelegate ProcessExceptionHandler { get; }

        #region Constructor(s)

        public SimpleProcessor(
            ProcessDelegate processHandler,
            OnProcessErrorDelegate processExceptionHandler = null
        ) : base(processHandler)
        {
            this.ProcessExceptionHandler = processExceptionHandler;
        }

        #endregion Constructor(s)

        public override void OnProcessError(Exception ex)
        {
            this.ProcessExceptionHandler?.Invoke(ex);
            base.OnProcessError(ex);
        }
    }

    public class SimpleProcessor<TInput, TOutput>
        : BaseDelegatedProcessor<TInput, TOutput>
    {
        public delegate void OnProcessErrorDelegate(Exception ex);

        public OnProcessErrorDelegate ProcessExceptionHandler { get; }

        #region Constructor(s)

        public SimpleProcessor(
            ProcessDelegate processHandler,
            OnProcessErrorDelegate processExceptionHandler = null
        ) : base(processHandler)
        {
            this.ProcessExceptionHandler = processExceptionHandler;
        }

        #endregion Constructor(s)

        public override void OnProcessError(Exception ex)
        {
            this.ProcessExceptionHandler?.Invoke(ex);
            base.OnProcessError(ex);
        }
    }
}