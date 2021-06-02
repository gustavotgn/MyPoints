using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
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

        public FakeAddressRepository(bool addressExists)
        {
            this.addressExists = addressExists;
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
    }
}
