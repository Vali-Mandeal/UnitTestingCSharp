using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    class ErrorLoggerTests
    {
        private ErrorLogger _errorLogger;

        [SetUp]
        public void SetUp()
        {
            _errorLogger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            _errorLogger.Log("a");

            Assert.That(_errorLogger.LastError, Is.EqualTo("a"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            Assert.That(() => _errorLogger.Log(error), Throws.ArgumentNullException);
        }


        [Test]
        public void Log_ValidError_RaiseErrorLogEvent()
        {
            var id = Guid.Empty;
            // to test an event, simply subscribe to it
            // collect the value in the event handler
            _errorLogger.ErrorLogged += (sender, args) => { id = args; };

            _errorLogger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
