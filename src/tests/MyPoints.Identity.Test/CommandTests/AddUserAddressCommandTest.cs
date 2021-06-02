using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Identity.Test.CommandTests
{
    [TestClass]
    public class AddUserAddressCommandTest
    {
        private AddUserAddressCommand command;
        private Faker _faker;

        public AddUserAddressCommandTest()
        {
            _faker = new Faker("pt_BR");
        }


        [TestMethod]
        public void ValidCommand()
        {
            command = new AddUserAddressCommand();

            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-","").Cut(8);
            command.State = _faker.Person.Address.State.Cut(2);

            command.Validate();

            Assert.IsTrue(command.Valid);
        }

        [TestMethod]
        public void InValidStreetMaxLength()
        {
            command = new AddUserAddressCommand();

            command.Street = _faker.Random.String(256, 257, char.MinValue, char.MaxValue);
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-","").Cut(8);
            command.State = _faker.Person.Address.State.Cut(2);

            command.Validate();

            Assert.IsTrue(command.Invalid);
        }
        [TestMethod]
        public void InValidCityMaxLength()
        {
            command = new AddUserAddressCommand();

            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Random.String(101, 102, char.MinValue, char.MaxValue);
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-", "").Cut(8);
            command.State = _faker.Person.Address.State.Cut(2);

            command.Validate();

            Assert.IsTrue(command.Invalid);
        }
        [TestMethod]
        public void InValidComplementsMaxLength()
        {
            command = new AddUserAddressCommand();

            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Random.String(256, 257, char.MinValue, char.MaxValue);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-", "").Cut(8);
            command.State = _faker.Person.Address.State.Cut(2);

            command.Validate();

            Assert.IsTrue(command.Invalid);
        }
        [TestMethod]
        public void InValidNumberMaxLength()
        {
            command = new AddUserAddressCommand();

            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Random.String(21, 22, char.MinValue, char.MaxValue);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-", "").Cut(8);
            command.State = _faker.Person.Address.State.Cut(2);

            command.Validate();

            Assert.IsTrue(command.Invalid);
        }
        [TestMethod]
        public void InValidPostalCodeMaxLength()
        {
            command = new AddUserAddressCommand();

            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode;
            command.State = _faker.Person.Address.State.Cut(2);

            command.Validate();

            Assert.IsTrue(command.Invalid);

        }

        [TestMethod]
        public void InValidStateMaxLength()
        {
            command = new AddUserAddressCommand();

            command.Street = _faker.Person.Address.Street;
            command.City = _faker.Person.Address.City;
            command.Complements = _faker.Address.FullAddress().Cut(255);
            command.Number = _faker.Address.BuildingNumber().Cut(20);
            command.PostalCode = _faker.Person.Address.ZipCode.Replace("-", "").Cut(8);
            command.State = _faker.Person.Address.State;

            command.Validate();

            Assert.IsTrue(command.Invalid);

        }
    }
}
