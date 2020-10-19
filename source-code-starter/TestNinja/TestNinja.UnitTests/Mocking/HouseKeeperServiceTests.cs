using System;
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
    public class HouseKeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private IEmailSender _emailSenderObject;
        private IXtraMessageBox _messageBoxObject;
        private readonly DateTime _statementDate = new DateTime(2017,1,1);
        private Housekeeper _housekeeper;


        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper()
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };


            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(uow => uow.Query<Housekeeper>())
                .Returns(new List<Housekeeper>()
            {
                _housekeeper
            }.AsQueryable);

            _statementGenerator = new Mock<IStatementGenerator>();
            var emailSender = new Mock<IEmailSender>();
            var messageBox = new Mock<IXtraMessageBox>();

            _emailSenderObject = emailSender.Object;
            _messageBoxObject = messageBox.Object;
            _service = new HousekeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSenderObject,
                _messageBoxObject);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(
                sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_HouseKeepersEmailIsInvalid_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;  

            _service.SendStatementEmails(_statementDate);   

            _statementGenerator.Verify(
                sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), 
                Times.Never);
        }
    }
}
