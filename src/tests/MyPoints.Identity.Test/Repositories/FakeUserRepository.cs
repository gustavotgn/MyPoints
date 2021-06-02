using Bogus;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Queries;
using MyPoints.Identity.Repositories.Interfaces;
using MyPoints.Identity.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Identity.Test.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly Faker _faker;
        private readonly bool _userExists;
        private bool addressExists;

        public FakeUserRepository(bool userExists = true)
        {
            _userExists = userExists;
            _faker = new Faker("pt_BR");
        }

        public FakeUserRepository(bool userExists = true, bool addressExists = false) : this(userExists)
        {
            this.addressExists = addressExists;
        }

        public async Task<AddUserCommandResult> AddAsync(AddUserCommand command)
        {
            return new AddUserCommandResult {
                Id = 1,
                Email = command.Email,
                Name = command.Name,
            };
        }

        public async Task<bool> AddressExistsAsync(int userId)
        {
            return addressExists;
        }

        public async Task<UserQueryResult> GetAsync(int id)
        {
            if (_userExists)
            {
                return new UserQueryResult {
                    Id = 1,
                    Email = _faker.Person.Email,
                    Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}",
                    Address = new UserAddressQueryResult {
                        Street = _faker.Person.Address.Street,
                        City = _faker.Person.Address.City,
                        PostalCode = _faker.Person.Address.ZipCode.Replace("-",""),
                        State = _faker.Person.Address.State.Cut(2),
                        Complements = _faker.Address.FullAddress().Cut(255)
                    }
                };

            }
            return null;
        }

        public async Task<LoginCommandResult> GetAsync(LoginCommand request)
        {
            if (_userExists)
            {
                return new LoginCommandResult{
                    Email = request.Email,
                    Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}",                    
                };
            }
            return null;
        }

        public async Task<UserQueryResult> GetByEmailAsync(string email)
        {
            if (_userExists)
            {
                return new UserQueryResult {
                    Id = 1,
                    Email = email,
                    Name = $"{_faker.Person.FirstName} {_faker.Person.LastName}",
                    Address = new UserAddressQueryResult {
                        Id = 1,
                        Number = _faker.Random.Number(0, int.MaxValue).ToString(),
                        Street = _faker.Person.Address.Street,
                        City = _faker.Person.Address.City,
                        PostalCode = _faker.Person.Address.ZipCode.Replace("-", ""),
                        State = _faker.Person.Address.State.Cut(2),
                        Complements = _faker.Address.FullAddress().Cut(255)
                    }
                };
            }
            return null;
        }
    }
}
