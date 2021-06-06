using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<AddUserAddressCommandResult> AddAsync(AddUserAddressCommand request);
        Task<AddressQueryResult> GetByUserIdAsync(int userId);
    }
}
