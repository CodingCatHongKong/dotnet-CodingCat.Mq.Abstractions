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
        TOutput DefaultOutput { get; }

        TOutput ProcessInput(TInput input);
    }

    public interface INoInputProcessor<TInput, TOutput>
        : IProcessor<TInput, TOutput>
    {
        TInput DefaultInput { get; }

        TOutput ProcessWithDefaultInput();
    }
}