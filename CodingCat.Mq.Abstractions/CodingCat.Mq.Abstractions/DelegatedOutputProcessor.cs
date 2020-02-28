namespace CodingCat.Mq.Abstractions
{
    public abstract class DelegatedOutputProcessor<T>
        : DelegatedProcessor<object, T>
    {
        public delegate T OutputProcessDelegate();

        #region Constructor(s)

        public DelegatedOutputProcessor(
            OutputProcessDelegate delegatedOutputProcess
        ) : base(value => delegatedOutputProcess())
        {
            this.DefaultInput = null;
        }

        #endregion Constructor(s)
    }
}