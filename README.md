# CodingCat.Mq.Abstractions


### Summary

Contains the basic interfaces and abstracts for processing input and produce the output of a subscribed application.


### Interfaces

- `IProcessor`, `IProcessor<TInput>` & `IProcessor<TInput, TOutput>` expose the least required properties and functions of the processor
- `ISubscriber` mainly defines the subscriber's supporting life cycle
- `IConnectConfiguration` is an interface for the retry-connect operation when connecting to any external service


### Abstracts

- `BaseProcessor` defines the processor features such as timeout logic, and the error handling
- `BaseProcessor<TInput>` & `BaseProcessor<TInput, TOutput>` are based on the `BaseProcessor` and used to define the abstract `Process` functions
- `BaseDelegatedProcessor<T>` & `BaseDelegatedProcessor<TInput, TOutput>` are based the above, but accepting delegate function for the mentioned abstract `Process` function in the constructor


### Target Frameworks

- .NET 4.6.1+
- .NET Standard 2.0+