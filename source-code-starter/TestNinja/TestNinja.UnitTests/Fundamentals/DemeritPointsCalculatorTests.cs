using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalculator;

        [SetUp]
        public void SetUp()
        {
            _demeritPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void
            CalculateDemeritsPoints_SpeedIsOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
        {   
            Assert.That(() => 
                _demeritPointsCalculator    
                    .CalculateDemeritPoints(
                        speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritsPoints_WhenCalled_ReturnsDemeritPoints(int speed, int points)
        {
            var result = _demeritPointsCalculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(points));
        }   
    }       
}
