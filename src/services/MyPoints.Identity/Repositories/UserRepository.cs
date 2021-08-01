using MyPoints.Identity.Data;
using MyPoints.Identity.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Dapper;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Queries;
using MyPoints.Identity.Domain.Commands.Output;
using AutoMapper;
using System.Linq;
using MyPoints.Identity.Domain.Entities;
using System.Collections.Generic;

namespace MyPoints.Identity.Repositories
{
    public class UserRepository :  IUserRepository
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public UserRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<AddUserCommandResult> AddAsync(AddUserCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddressExistsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id, Guid updatedUser)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRange(IEnumerable<ApplicationUser> obj)
        {
            throw new NotImplementedException();
        }

        public Task<UserQueryResult> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<LoginCommandResult> GetAsync(LoginCommand request)
        {
            throw new NotImplementedException();
        }

        public Task<UserQueryResult> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public Task InsertRange(IEnumerable<ApplicationUser> obj)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> Select(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ApplicationUser>> Select()
        {
            throw new NotImplementedException();
        }

        public Task Update(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRange(IEnumerable<ApplicationUser> obj)
        {
            throw new NotImplementedException();
        }
    }
}
