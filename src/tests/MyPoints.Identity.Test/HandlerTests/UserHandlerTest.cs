using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Handlers;
using MyPoints.Identity.Test.Data;
using MyPoints.Identity.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Identity.Test.HandlerTests
{
    [TestClass]
    public class UserHandlerTest
    {
        private UserHandler _handler;
        private Faker _faker;

        public UserHandlerTest()
        {
            _faker = new Faker("pt_BR");
            _handler = new UserHandler(new FakeIdentityContext(), new FakeMessageService());
        }

        [TestMethod]
        public async Task ValidAddUserHandler()
        {
            var command = new AddUserCommand();
            _handler = new UserHandler(new FakeIdentityContext(userExists: false), new FakeMessageService());


            command.Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}";
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);
            command.Email = _faker.Person.Email;
            var result = await _handler.Handle(command,new System.Threading.CancellationToken());

            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public async Task ValidAddAddressHandler()
        {
            var command = new AddUserAddressCommand();
            _handler = new UserHandler(new FakeIdentityContext(addressExists: false), new FakeMessageService());


            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-", "");
            command.State = _faker.Person.Address.State.Cut(2);

            var result = await _handler.Handle(command,new System.Threading.CancellationToken());

            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public async Task UserAddressAlreadyRegistered()
        {
            var command = new AddUserAddressCommand();
            _handler = new UserHandler(new FakeIdentityContext(addressExists: true), new FakeMessageService());


            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-", "");
            command.State = _faker.Person.Address.State.Cut(2);

            var result = await _handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public async Task UserAlreadyRegistered()
        {
            var command = new AddUserCommand();
            _handler = new UserHandler(new FakeIdentityContext(userExists: true), new FakeMessageService());


            command.Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}";
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);
            command.Email = _faker.Person.Email;
            var result = await _handler.Handle(command,new System.Threading.CancellationToken());

            Assert.IsFalse(result.Succeeded);
        }
    }
}
