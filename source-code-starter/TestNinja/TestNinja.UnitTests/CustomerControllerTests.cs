using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class CustomerControllerTests
    {
        private CustomerController _customerController;

        [SetUp]
        public void Setup()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_IdIsZero_ReturnsNotFound()
        {
            var result = _customerController.GetCustomer(0);

            // Typeof
            Assert.That(result, Is.TypeOf<NotFound>());

            // Typeof or one of its derivatives
            //Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnsOk()
        {
            var result = _customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
