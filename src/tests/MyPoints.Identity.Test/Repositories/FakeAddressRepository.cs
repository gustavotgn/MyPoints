using Bogus;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Queries;
using MyPoints.Identity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Identity.Test.Repositories
{
    public class FakeAddressRepository : IAddressRepository
    {
        private bool addressExists;
        private readonly Faker _faker;

        public FakeAddressRepository(bool addressExists)
        {
            this.addressExists = addressExists;
            _faker = new Faker("pt_BR");
        }

        public async Task<AddUserAddressCommandResult> AddAsync(AddUserAddressCommand request)
        {
            return new AddUserAddressCommandResult {
                Id = 1,
                City = request.City,
                Complements = request.Complements,
                PostalCode = request.PostalCode,
                State = request.State,
                Street = request.Street
            };
        }

        public async Task<AddressQueryResult> GetByUserIdAsync(int userId)
        {
            return new AddressQueryResult {
                Id = 1,
                City = _faker.Person.Address.City,
                Complements = String.Join(' ', _faker.Lorem.Words(100)),
                PostalCode = _faker.Person.Address.ZipCode.Replace("-",""),
                State = _faker.Person.Address.State,
                Street = _faker.Person.Address.Street
            };
        }
    }
}
