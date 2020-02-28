namespace CodingCat.Mq.Abstractions
{
    public abstract class OutputProcessor<T> : Processor<object, T>
    {
        #region Constructor(s)
        public OutputProcessor()
        {
            this.DefaultInput = null;
        }
        #endregion
    }
}