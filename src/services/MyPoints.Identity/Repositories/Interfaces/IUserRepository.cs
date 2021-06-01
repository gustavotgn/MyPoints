using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Queries;
using System.Threading.Tasks;

namespace MyPoints.Identity.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserQueryResult> GetAsync(int id);
        Task<AddUserCommandResult> AddAsync(AddUserCommand command);
        Task<UserQueryResult> GetByEmailAsync(string email);
        Task<bool> AddressExistsAsync(int userId);
        Task<LoginCommandResult> GetAsync(LoginCommand request);
    }
}
