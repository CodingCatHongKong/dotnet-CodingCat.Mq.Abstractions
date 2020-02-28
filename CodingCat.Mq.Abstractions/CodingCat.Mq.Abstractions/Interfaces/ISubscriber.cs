using System;

namespace CodingCat.Mq.Abstractions.Interfaces
{
    public interface ISubscriber : IDisposable
    {
        event EventHandler Subscribed;

        event EventHandler Completed;

        event EventHandler Disposing;

        event EventHandler Disposed;

        ISubscriber Subscribe();
    }
}