namespace CodingCat.Mq.Abstractions
{
    public abstract class OutputProcessor<T> : Processor<object, T>
    {
        public virtual T HandleInput() => this.HandleInput(null);
    }
}