using System;

namespace CodingCat.Mq.Abstractions.Tests.Impls
{
    public class SimpleProcessor<T> : Processor<T>
    {
        public delegate void HandleInput(T input);
        public delegate void HandleProcessException(Exception ex);

        public HandleInput InputHandler { get; }
        public HandleProcessException ProcessExceptionHandler { get; }

        #region Constructor(s)

        public SimpleProcessor(
            HandleInput inputHandler,
            HandleProcessException processExceptionHandler = null
        )
        {
            this.InputHandler = inputHandler;
            this.ProcessExceptionHandler = processExceptionHandler;
        }

        #endregion Constructor(s)

        public override void OnProcessError(Exception ex)
        {
            this.ProcessExceptionHandler?.Invoke(ex);
            base.OnProcessError(ex);
        }

        protected override void OnInput(T input) => this.InputHandler(input);
    }
}