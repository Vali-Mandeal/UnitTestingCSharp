using NUnit.Framework;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        // SetUp
        private Math _math;
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        // TearDown

        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            // Arrange

            // Act
            var result = _math.Add(1, 2);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnsTheFirstArgument()
        {

            var result = _math.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));
        }   

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnsTheSecondArgument()
        {

            var result = _math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnsTheSameArgument()
        {

            var result = _math.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }
    }   
}
    