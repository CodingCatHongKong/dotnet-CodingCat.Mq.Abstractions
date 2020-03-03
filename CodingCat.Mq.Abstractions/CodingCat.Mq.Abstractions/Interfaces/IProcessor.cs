using System;

namespace CodingCat.Mq.Abstractions.Interfaces
{
    public interface IProcessor
    {
        void OnProcessError(Exception ex);
    }

    public interface IProcessor<TInput> : IProcessor
    {
        void HandleInput(TInput input);
    }

    public interface IProcessor<TInput, TOutput> : IProcessor
    {
        TInput DefaultInput { get; }
        TOutput DefaultOutput { get; }

        TOutput ProcessInput(TInput input);
    }

    public interface INoInputProcessor<TOutput>
        : IProcessor<object, TOutput>
    {
        TOutput ProcessWithDefaultInput();
    }
}