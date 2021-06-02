using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Handlers;
using MyPoints.Identity.Test.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Identity.Test.HandlerTests
{
    [TestClass]
    public class LoginHandlerTest
    {
        private LoginHandler _handler;
        private Faker _faker;

        public LoginHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _handler = new LoginHandler(new FakeIdentityContext());

        }

        [TestMethod]
        public async Task ValidHandler()
        {
            var command = new LoginCommand();

            command.Email = _faker.Person.Email;
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);
            var result = await _handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public async Task LoginFailedHandler()
        {
            _handler = new LoginHandler(new FakeIdentityContext(userExists: false));

            var command = new LoginCommand();

            command.Email = _faker.Person.Email;
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);
            var result = await _handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsFalse(result.Succeeded);
        }
    }
}
