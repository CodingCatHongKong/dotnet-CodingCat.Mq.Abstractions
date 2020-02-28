using CodingCat.Serializers.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace CodingCat.Mq.Abstractions
{
    public abstract class Processor
    {
        public ILogger Logger { get; set; }

        public virtual void OnProcessError(Exception ex)
        {
            this.Logger?.LogError(ex, "");
        }
    }

    public abstract class Processor<TInput> : Processor
    {
        public ISerializer<TInput> InputSerializer { get; protected set; }

        #region Constructor(s)

        protected Processor()
        {
        }

        public Processor(ISerializer<TInput> inputSerializer)
        {
            this.InputSerializer = inputSerializer;
        }

        #endregion Constructor(s)

        public abstract void Process(TInput input);
    }

    public abstract class Processor<TInput, TOutput> : Processor
    {
        public ISerializer<TInput> InputSerializer;
        public ISerializer<TOutput> OutputSerializer;

        #region Constructor(s)

        protected Processor()
        {
        }

        public Processor(
            ISerializer<TInput> inputSerializer,
            ISerializer<TOutput> outputSerializer
        )
        {
            this.InputSerializer = inputSerializer;
            this.OutputSerializer = outputSerializer;
        }

        #endregion Constructor(s)

        public abstract TOutput Process(TInput input);
    }
}