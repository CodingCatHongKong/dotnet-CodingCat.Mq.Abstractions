namespace CodingCat.Mq.Abstractions.Tests.Impls
{
    public class SimpleOutputProcessor<T> : OutputProcessor<T>
    {
        public delegate T ProcessDelegate();

        public ProcessDelegate ProcessHandler { get; }

        #region Constructor(s)

        public SimpleOutputProcessor(ProcessDelegate processHandler)
        {
            this.ProcessHandler = processHandler;
        }

        #endregion Constructor(s)

        protected override T Process(object input) =>
            this.ProcessHandler();
    }
}