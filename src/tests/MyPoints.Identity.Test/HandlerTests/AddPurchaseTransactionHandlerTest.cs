using AutoMapper;
using Bogus;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Handlers;
using MyPoints.Identity.Domain.Mappers;
using MyPoints.Identity.Test.CommandTests;
using MyPoints.Identity.Test.Data;
using MyPoints.Identity.Test.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Identity.Test.HandlerTests
{
    [TestClass]
    public class AddPurchaseTransactionHandlerTest
    {

        private AddPurchaseTransactionHandler _handler;
        private Faker _faker;

        public AddPurchaseTransactionHandlerTest()
        {
            var mock = new Mock<ILogger<AddPurchaseTransactionHandler>>();
            ILogger<AddPurchaseTransactionHandler> logger = mock.Object;

            //or use this short equivalent 
            logger = Mock.Of<ILogger<AddPurchaseTransactionHandler>>();


            _faker = new Faker("pt_BR");
            _handler = new AddPurchaseTransactionHandler(AutoMapperFake.Get(),logger,new FakeMessageService(),new MediatorFake());

        }

        [TestMethod]
        public async Task ValidHandler()
        {
            var command = AddPurchaseTransactionCommandTest.GetValidCommand();

             await _handler.Handle(command,new System.Threading.CancellationToken());

            Assert.IsTrue(true);
        }

    }
}