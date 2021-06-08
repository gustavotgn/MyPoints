using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoints.Identity.Domain.Commands.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Identity.Test.CommandTests
{
    [TestClass]
    public class ValidateOrderAddressCommandTest
    {
        private ValidateOrderAddressCommand command;
        private Faker _faker;

        public ValidateOrderAddressCommandTest()
        {
            _faker = new Faker("pt_BR");
        }

        [TestMethod]
        public void ValidCommand()
        {
            command = new ValidateOrderAddressCommand();

            command.OrderId = _faker.Random.Int(1);
            command.ProductId = _faker.Random.Int(1);
            command.TransactionTypeId = (Domain.Enums.ETransactionType) _faker.Random.Int(1,2);
            command.UserId = _faker.Random.Int(1);
            command.Value = _faker.Random.Decimal();

            command.Validate();

            Assert.IsTrue(command.Valid);
        }
    }
}