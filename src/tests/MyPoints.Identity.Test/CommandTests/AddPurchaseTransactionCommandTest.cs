using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoints.Identity.Domain.Commands.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Identity.Test.CommandTests
{
    [TestClass]

    public class AddPurchaseTransactionCommandTest
    {
        private AddPurchaseTransactionCommand command;
        private Faker _faker;

        public AddPurchaseTransactionCommandTest()
        {
            _faker = new Faker("pt_BR");
        }

        [TestMethod]
        public void ValidCommand()
        {
            command = GetValidCommand();

            command.Validate();

            Assert.IsTrue(command.Valid);
        }
        public static AddPurchaseTransactionCommand GetValidCommand()
        {
            Faker _faker = new Faker("pt_BR");
            var command = new AddPurchaseTransactionCommand();

            command.OrderId = _faker.Random.Int(1);
            command.ProductId = _faker.Random.Int(1);
            command.UserId = _faker.Random.Int(1);
            command.Value = _faker.Random.Decimal((Decimal)0.01);
            return command;
        }
    }
}
