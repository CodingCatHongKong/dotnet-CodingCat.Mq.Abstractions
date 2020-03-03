namespace CodingCat.Mq.Abstractions
{
    public abstract class BaseOutputProcessor<T> : BaseProcessor<object, T>
    {
        #region Constructor(s)

        public BaseOutputProcessor()
        {
            this.DefaultInput = null;
        }

        #endregion Constructor(s)
    }
}