using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoints.Identity.Domain.Commands.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Identity.Test.CommandTests
{
    [TestClass]
    public class AddUserCommandTest
    {
        private AddUserCommand command;
        private Faker _faker;

        public AddUserCommandTest()
        {
            _faker = new Faker("pt_BR");
        }

        [TestMethod]
        public void ValidCommand()
        {
            command = new AddUserCommand();

            command.Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}";
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);
            command.Email = _faker.Person.Email;
            command.Validate();

            Assert.IsTrue(command.Valid);


        }

        [TestMethod]
        public void InvalidNameMaxLength()
        {
            command = new AddUserCommand();

            command.Name = _faker.Random.String(81, 90, char.MinValue, char.MaxValue);
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);
            command.Email = _faker.Person.Email;
            command.Validate();

            Assert.IsTrue(command.Invalid);

        }
        [TestMethod]
        public void InvalidNameMinLength()
        {
            command = new AddUserCommand();

            command.Name = _faker.Random.String(0, 1, char.MinValue, char.MaxValue);
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);
            command.Email = _faker.Person.Email;
            command.Validate();

            Assert.IsTrue(command.Invalid);


        }
        [TestMethod]
        public void InvalidPasswordMinLength()
        {

            command = new AddUserCommand();

            command.Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}";
            command.Email = _faker.Person.Email;
            command.Password = _faker.Random.String(1, 5, char.MinValue, char.MaxValue);
            command.Validate();

            Assert.IsTrue(command.Invalid);

        }
        [TestMethod]
        public void InvalidPasswordMaxLength()
        {
            command = new AddUserCommand();

            command.Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}";
            command.Email = _faker.Person.Email;
            command.Password = _faker.Random.String(21, 25, char.MinValue, char.MaxValue);
            command.Validate();

            Assert.IsTrue(command.Invalid);

        }

        [TestMethod]
        public void InvalidEmail()
        {
            command = new AddUserCommand();

            command.Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}";
            command.Email = _faker.Person.Email.Replace("@", "");
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);

            command.Validate();
            Assert.IsTrue(command.Invalid);
        }
    }
}
