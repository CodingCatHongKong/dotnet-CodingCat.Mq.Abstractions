using CodingCat.Mq.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodingCat.Mq.Abstractions.Tests.Impls
{
    public class SimpleSubscriber<T> : BaseProcessor<T>, ISubscriber
    {
        private object lockable { get; } = new object();
        private Queue<T> inputQueue { get; set; }

        public int ServedAmount { get; private set; } = 0;

        public event EventHandler Subscribed;

        public event EventHandler Processed;

        public event EventHandler Disposing;

        public event EventHandler Disposed;

        public T LastInput { get; private set; }

        #region Constructor(s)

        public SimpleSubscriber(Queue<T> inputQueue) : base()
        {
            this.inputQueue = inputQueue;
        }

        #endregion Constructor(s)

        protected override void Process(T input)
        {
            this.LastInput = input;
            Console.WriteLine(this.LastInput.ToString());
        }

        public ISubscriber Subscribe()
        {
            Task.Run(() =>
            {
                while (this.inputQueue != null)
                {
                    lock (this.lockable)
                    {
                        if (this.inputQueue == null) break;
                        if (this.inputQueue.Count > 0)
                        {
                            this.Process(this.inputQueue.Peek());
                            this.inputQueue.Dequeue();

                            this.ServedAmount += 1;
                            this.Processed?.Invoke(this, null);
                        }
                    }
                    Thread.Sleep(50);
                }
            });

            this.Subscribed?.Invoke(this, null);
            return this;
        }

        public void Dispose()
        {
            this.Disposing?.Invoke(this, null);

            var notifier = new AutoResetEvent(false);
            lock (this.lockable)
            {
                if (this.inputQueue.Count <= 0)
                {
                    this.inputQueue = null;
                    notifier.Set();
                }
                else
                {
                    this.inputQueue = new Queue<T>(this.inputQueue);
                    this.Processed += (sender, args) =>
                    {
                        if (this.inputQueue.Count <= 0)
                        {
                            this.inputQueue = null;
                            notifier.Set();
                        }
                    };
                }
            }

            notifier.WaitOne();
            this.Disposed?.Invoke(this, null);
        }
    }
}