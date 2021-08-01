using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Entities;
using MyPoints.Identity.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace MyPoints.Identity.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser,Guid>
    {
        Task<UserQueryResult> GetAsync(Guid id);
        Task<AddUserCommandResult> AddAsync(AddUserCommand command);
        Task<UserQueryResult> GetByEmailAsync(string email);
        Task<bool> AddressExistsAsync(int userId);
        Task<LoginCommandResult> GetAsync(LoginCommand request);
    }
}
