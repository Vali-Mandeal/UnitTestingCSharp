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
    public class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            // Arrange
            var storage = new Mock<IStorage>();
            var service = new OrderService(storage.Object);
            var order = new Order();
            // Act
            service.PlaceOrder(order);

            // Assert
            storage.Verify(st => st.Store(order));
        }
    }
}
