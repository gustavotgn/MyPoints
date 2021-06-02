using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoints.Identity.Domain.Commands.Input;

namespace MyPoints.Identity.Test
{
    [TestClass]
    public class LoginCommandTest
    {
        private LoginCommand command;
        private Faker _faker;

        public LoginCommandTest()
        {
            _faker = new Faker("pt_BR");
        }

        [TestMethod]
        public void ValidCommand()
        {
            command =  new LoginCommand();

            command.Email = _faker.Person.Email;
            command.Password = _faker.Random.String(6,20,char.MinValue,char.MaxValue);

            command.Validate();
            Assert.IsTrue(command.Valid);
        }

        [TestMethod]
        public void InvalidPasswordMinLength()
        {

            command = new LoginCommand();
            command.Email = _faker.Person.Email;
            command.Password = _faker.Random.String(1, 5, char.MinValue, char.MaxValue);
            command.Validate();

            Assert.IsTrue(command.Invalid);

        }
        [TestMethod]
        public void InvalidPasswordMaxLength()
        {
            command = new LoginCommand();
            command.Email = _faker.Person.Email;
            command.Password = _faker.Random.String(21,25,char.MinValue,char.MaxValue);
            command.Validate();

            Assert.IsTrue(command.Invalid);

        }

        [TestMethod]
        public void InvalidEmail()
        {
            command = new LoginCommand();

            command.Email = _faker.Person.Email.Replace("@","");
            command.Password = _faker.Random.String(6, 20, char.MinValue, char.MaxValue);

            command.Validate();
            Assert.IsTrue(command.Invalid);
        }
    }
}
