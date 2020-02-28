namespace CodingCat.Mq.Abstractions.Tests.Impls
{
    public class SimpleOutputProcessor<T> : DelegatedOutputProcessor<T>
    {
        #region Constructor(s)

        public SimpleOutputProcessor(
            OutputProcessDelegate delegatedOutputProcess
        ) : base(delegatedOutputProcess) { }

        #endregion Constructor(s)
    }
}