namespace CodingCat.Mq.Abstractions
{
    public abstract class BaseDelegatedProcessor<T> : BaseProcessor<T>
    {
        public delegate void ProcessDelegate(T input);

        public ProcessDelegate DelegatedProcess { get; }

        #region Constructor(s)

        public BaseDelegatedProcessor(ProcessDelegate delegatedProcess) : base()
        {
            this.DelegatedProcess = delegatedProcess;
        }

        #endregion Constructor(s)

        protected override void Process(T input) => this.DelegatedProcess(input);
    }

    public abstract class BaseDelegatedProcessor<TInput, TOutput>
        : BaseProcessor<TInput, TOutput>
    {
        public delegate TOutput ProcessDelegate(TInput input);

        public ProcessDelegate DelegatedProcess { get; }

        #region Constructor(s)

        public BaseDelegatedProcessor(ProcessDelegate delegatedProcess) : base()
        {
            this.DelegatedProcess = delegatedProcess;
        }

        #endregion Constructor(s)

        protected override TOutput Process(TInput input) => this.DelegatedProcess(input);
    }
}