using CodingCat.Mq.Abstractions.Interfaces;

namespace CodingCat.Mq.Abstractions.Tests.Impls
{
    public class SimpleOutputProcessor<T>
        : BaseDelegatedOutputProcessor<T>, INoInputProcessor<T>
    {
        #region Constructor(s)

        public SimpleOutputProcessor(
            OutputProcessDelegate delegatedOutputProcess
        ) : base(delegatedOutputProcess) { }

        #endregion Constructor(s)
    }
}