using System;

namespace CodingCat.Mq.Abstractions.Interfaces
{
    public interface IConnectConfiguration
    {
        TimeSpan TimeoutPerTry { get; }
        TimeSpan RetryInterval { get; }
        uint RetryUpTo { get; }
    }
}