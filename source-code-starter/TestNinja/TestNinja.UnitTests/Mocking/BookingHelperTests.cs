﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests_OverlappingBookingExists
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(r => r.GetActiveBookings(1))
                .Returns(new List<Booking>{_existingBooking}.AsQueryable);
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days:2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking 
            {
                Id = 1, 
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {   
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {    
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsInTheMiddleOfAnExistingBookingAndFinishesAfter_ReturnsExistingBookingsReference()
        {   
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1, 
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days:2),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCanceled_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1, 
                ArrivalDate = _existingBooking.ArrivalDate,
                DepartureDate = _existingBooking.DepartureDate,
                Status = "Cancelled"
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        private static DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
            
        private static DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(+days);
        }

        private static DateTime ArriveOn(int year, int mounth, int day)
        {
            return new DateTime(year, mounth, day, 14, 0, 0);
        }
            
        private static DateTime DepartOn(int year, int mounth, int day)
        {
            return new DateTime(year, mounth, day, 10, 0, 0);
        }
    }   
}
    