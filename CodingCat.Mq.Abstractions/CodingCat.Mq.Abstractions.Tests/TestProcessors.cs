using CodingCat.Mq.Abstractions.Tests.Impls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace CodingCat.Mq.Abstractions.Tests
{
    [TestClass]
    public class TestProcessors
    {
        [TestMethod]
        public void Test_ProcessInput_Ok()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();
            var actual = null as string;

            // Act
            new SimpleProcessor<string>(value => actual = value)
            {
                Timeout = TimeSpan.FromMilliseconds(100)
            }.HandleInput(expected);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ProcessInput_ExceptionHandled()
        {
            // Arrange
            var expected = new Exception();
            var actual = null as Exception;

            // Act
            new SimpleProcessor<string>(
                value => throw expected,
                ex => actual = ex
            ).HandleInput(null);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ProcessInput_Timeout()
        {
            // Arrange
            var expected = null as string;
            var actual = expected;

            // Act
            new SimpleProcessor<string>(
                value =>
                {
                    Thread.Sleep(1500);
                    actual = Guid.NewGuid().ToString();
                }
            )
            {
                Timeout = TimeSpan.FromMilliseconds(100)
            }.HandleInput(null);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ProcessOutput_Ok()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();

            // Act
            var actual = new SimpleProcessor<object, string>(
                value => expected
            )
            {
                Timeout = TimeSpan.FromSeconds(1)
            }.HandleInput(null);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ProcessOutput_ExceptionHandled()
        {
            // Arrange
            var expected = new Exception();
            var actual = expected;

            // Act
            new SimpleProcessor<object, string>(
                value => throw expected,
                ex => actual = ex
            ).HandleInput(null);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ProcessOutput_Timeout()
        {
            // Arrange
            var expected = int.MinValue;

            // Act
            var actual = new SimpleProcessor<object, int>(
                value =>
                {
                    Thread.Sleep(1500);
                    return int.MaxValue;
                }
            )
            {
                DefaultOutput = expected,
                Timeout = TimeSpan.FromMilliseconds(100)
            }.HandleInput(null);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_OutputProcessor_Ok()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();

            // Act
            var actual = new SimpleOutputProcessor<string>(() => expected)
            {
                Timeout = TimeSpan.FromSeconds(1)
            }.Process();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}