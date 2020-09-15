using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue() //Convention: Method Name -> Scenario -> Expected Result
        {   
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User {IsAdmin = true});

            // Assert
            Assert.IsTrue(result);
        }
            
        [TestMethod]
        public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnsTrue()
        {
            var user = new User();
            var reservation = new Reservation{MadeBy = user};

            var result = reservation.CanBeCancelledBy(user);    

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanbeCancelledBy_AnotherUserCancellingTheReservation_ReturnsFalse()
        {
            var reservation = new Reservation{MadeBy = new User()};

            var result = reservation.CanBeCancelledBy(new User {IsAdmin = false});

            Assert.IsFalse(result);
        }   
    }
}
    