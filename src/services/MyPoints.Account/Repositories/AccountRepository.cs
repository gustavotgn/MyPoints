using AutoMapper;
using Dapper;
using MyPoints.Account.Data;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Queries;
using MyPoints.Account.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext _context;
        private IMapper _mapper;

        public AccountRepository(AccountContext AccountContext, IMapper mapper)
        {
            this._context = AccountContext;
            _mapper = mapper;
        }

        public async Task<AddAccountCommandResult> AddAsync(AddAccountCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {
                var id = await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO Account (UserId,Amount)
                        VALUES(@UserId,0);
                        SELECT LAST_INSERT_ID();", request);

                transaction.Commit();

                var result = _mapper.Map<AddAccountCommandResult>(request);
                result.Id = id;
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task<AccountQueryResult> GetAsync(int id)
        {
            try
            {

                var result = await _context.Connection.QueryFirstOrDefaultAsync<AccountQueryResult>(@"
                    SELECT * FROM Account WHERE Id = @id;", new { id });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<AccountQueryResult> GetByUserIdAsync(int userId)
        {
            try
            {

                var result = await _context.Connection.QueryFirstOrDefaultAsync<AccountQueryResult>(@"
                    SELECT * FROM Account WHERE UserId = @userId;", new { userId });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
