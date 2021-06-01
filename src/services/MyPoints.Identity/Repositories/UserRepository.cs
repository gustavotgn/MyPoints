using MyPoints.Identity.Data;
using MyPoints.Identity.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Dapper;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Queries;
using MyPoints.Identity.Domain.Commands.Output;
using AutoMapper;

namespace MyPoints.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IdentityContext _context;
        private IMapper _mapper;

        public UserRepository(IdentityContext identityContext, IMapper mapper)
        {
            _context = identityContext;
            _mapper = mapper;
        }

        public async Task<AddUserCommandResult> AddAsync(AddUserCommand user)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {
                var id = await _context.Connection.ExecuteScalarAsync<int>("INSERT INTO User (Name,Email,Password)VALUES(@Name,@Email,@Password);SELECT LAST_INSERT_ID();", user);
                
                transaction.Commit();

                var result = _mapper.Map<AddUserCommandResult>(user);
                result.Id = id;

                return result;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

        }

        public async Task<bool> AddressExistsAsync(int id)
        {
            return (await _context.Connection.QueryFirstAsync<int?>("SELECT AddressId FROM User WHERE Id=@id", new { id })).HasValue;
        }

        public async Task<UserQueryResult> GetByEmailAsync(string email)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<UserQueryResult>("SELECT * FROM User WHERE Email=@email", new { email });
        }

        public async Task<LoginCommandResult> GetAsync(LoginCommand request)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<LoginCommandResult>("SELECT Email,Name,Id FROM User WHERE Email=@Email AND `Password`=@Password",request);
        }

        public async Task<UserQueryResult> GetAsync(int id)
        {
            var result = new UserQueryResult();
            var sql = @"SELECT * FROM User WHERE Id=@id;
                        SELECT Address.* FROM Address
                        INNER JOIN User ON User.AddressId = Address.Id WHERE User.Id = @id;";

            using (var multi = await _context.Connection.QueryMultipleAsync(sql, new { id }))
            {
                result = await multi.ReadFirstOrDefaultAsync<UserQueryResult>();
                result.Address= await multi.ReadFirstOrDefaultAsync<UserAddressQueryResult>();
            }
            return result;
        }
    }
}
