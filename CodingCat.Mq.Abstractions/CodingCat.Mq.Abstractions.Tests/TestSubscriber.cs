using CodingCat.Mq.Abstractions.Tests.Impls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CodingCat.Mq.Abstractions.Tests
{
    [TestClass]
    public class TestSubscriber
    {
        [TestMethod]
        public void Test_Subscriber_IsSubscribed()
        {
            const int QUEUING_AMOUNT = 50;

            // Arrange
            var queue = new Queue<string>();
            var subscriber = new SimpleSubscriber<string>(queue);
            var notifier = new AutoResetEvent(false);

            var expected = new string[QUEUING_AMOUNT]
                .Select(val => Guid.NewGuid().ToString())
                .ToArray();
            var actual = new List<string>();

            // Act
            subscriber.Processed += (sender, args) =>
            {
                Console.WriteLine(subscriber.ServedAmount);
                notifier.Set();
            };
            subscriber.Subscribe();

            foreach (var value in expected)
            {
                queue.Enqueue(value);
                notifier.WaitOne();
                actual.Add(subscriber.LastInput);
            }

            // Assert
            Assert.AreEqual(QUEUING_AMOUNT, actual.Count);
            Assert.IsTrue(actual
                .Intersect(expected)
                .Count()
                .Equals(QUEUING_AMOUNT)
            );

            subscriber.Dispose();
        }

        [TestMethod]
        public void Test_Subscriber_LifeCycle()
        {
            // Arrange
            var queue = new Queue<string>();
            var subscriber = new SimpleSubscriber<string>(queue);

            var notifier = new AutoResetEvent(false);
            var notExpected = Guid.NewGuid().ToString();

            var hadSubscribed = false;
            var hadDisposing = false;
            var hadDisposed = false;

            subscriber.Processed += (sender, args) => notifier.Set();
            subscriber.Subscribed += (sender, args) => hadSubscribed = true;
            subscriber.Disposing += (sender, args) => hadDisposing = true;
            subscriber.Disposed += (sender, args) => hadDisposed = true;

            // Act
            subscriber.Subscribe();
            subscriber.Dispose();
            queue.Enqueue(notExpected);

            notifier.WaitOne(1500);
            var actual = subscriber.LastInput;

            // Assert
            Assert.IsTrue(hadSubscribed);
            Assert.IsTrue(hadDisposing);
            Assert.IsTrue(hadDisposed);

            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod]
        public void Test_Subscriber_SupportGracefulShutdown()
        {
            const int QUEUING_AMOUNT = 50;

            // Arrange
            var queue = new Queue<string>();
            var subscriber = new SimpleSubscriber<string>(queue);

            var expected = new string[QUEUING_AMOUNT]
                .Select(val => Guid.NewGuid().ToString())
                .ToArray();
            var actual = new List<string>();

            subscriber.Processed += (sender, args) =>
                actual.Add(subscriber.LastInput);

            // Act
            subscriber.Subscribe();
            foreach (var value in expected)
                queue.Enqueue(value);

            subscriber.Dispose();

            // Assert
            Assert.AreEqual(QUEUING_AMOUNT, actual.Count);
            Assert.IsTrue(actual
                .Intersect(expected)
                .Count()
                .Equals(QUEUING_AMOUNT)
            );
        }
    }
}