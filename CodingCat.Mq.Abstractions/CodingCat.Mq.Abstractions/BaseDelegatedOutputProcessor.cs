namespace CodingCat.Mq.Abstractions
{
    public abstract class BaseDelegatedOutputProcessor<T>
        : BaseDelegatedProcessor<object, T>
    {
        public delegate T OutputProcessDelegate();

        #region Constructor(s)

        public BaseDelegatedOutputProcessor(
            OutputProcessDelegate delegatedOutputProcess
        ) : base(value => delegatedOutputProcess())
        {
            this.DefaultInput = null;
        }

        #endregion Constructor(s)
    }
}