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

        public virtual void Process(TInput input)
        {
            try
            {
                this.OnInput(input);
            }
            catch (Exception ex)
            {
                this.OnProcessError(ex);
            }
        }

        protected abstract void OnInput(TInput input);
    }
}