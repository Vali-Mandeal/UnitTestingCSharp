using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCanceledBy_UserIsAdmin_ReturnsTrue() //Convention: Method Name -> Scenario -> Expected Result
        {   
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User {IsAdmin = true});

            // Assert
            Assert.IsTrue(result);
        }   
    }
}
    