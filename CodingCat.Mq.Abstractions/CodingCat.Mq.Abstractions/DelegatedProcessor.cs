namespace CodingCat.Mq.Abstractions
{
    public abstract class DelegatedProcessor<T> : Processor<T>
    {
        public delegate void ProcessDelegate(T input);

        public ProcessDelegate DelegatedProcess { get; }

        #region Constructor(s)

        public DelegatedProcessor(ProcessDelegate delegatedProcess) : base()
        {
            this.DelegatedProcess = delegatedProcess;
        }

        #endregion Constructor(s)

        protected override void Process(T input) =>
            this.DelegatedProcess(input);
    }

    public abstract class DelegatedProcessor<TInput, TOutput>
        : Processor<TInput, TOutput>
    {
        public delegate TOutput ProcessDelegate(TInput input);

        public ProcessDelegate DelegatedProcess { get; }

        #region Constructor(s)

        public DelegatedProcessor(ProcessDelegate delegatedProcess) : base()
        {
            this.DelegatedProcess = delegatedProcess;
        }

        #endregion Constructor(s)

        protected override TOutput Process(TInput input) =>
            this.DelegatedProcess(input);
    }
}