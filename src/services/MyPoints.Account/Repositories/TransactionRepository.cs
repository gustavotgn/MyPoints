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
    public class TransactionRepository : ITransactionRepository
    {
        private AccountContext _context;
        private IMapper _mapper;

        public TransactionRepository(AccountContext accountContext, IMapper mapper)
        {
            _context = accountContext;
            _mapper = mapper;
        }

        public async Task<AddTransactionCommandResult> AddAsync(AddTransactionCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {
                var account = await _context.Account.GetByUserIdAsync(request.UserId);

                account.Amount += request.Value;

                var id = await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO Transaction (ProductId,Value,AmountAfter,TransactionTypeId,AccountId)
                        VALUES(@ProductId,@Value,@AmountAfter,@TransactionTypeId,@AccountId);
                        SELECT LAST_INSERT_ID();", new { 
                    request.ProductId,
                    request.Value,
                    AmountAfter = account.Amount,
                    request.TransactionTypeId,
                    AccountId = account.Id
                });

                await _context.Connection.ExecuteAsync("UPDATE Account SET Amount = @Amount WHERE Id = @Id", account);

                transaction.Commit();

                var result = _mapper.Map<AddTransactionCommandResult>(request);
                result.Id = id;
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<TransactionQueryResult> GetByAccountIdAsync(int userId)
        {
            try
            {
                var result = new TransactionQueryResult();

                var sql = @"
                    SELECT * FROM Account WHERE UserId = @userId;
                    SELECT * FROM Transaction
                        INNER JOIN Account ON Account.Id = Transaction.AccountId
                    WHERE Account.UserId = @userId;                    
                ";
                using (var multi = await _context.Connection.QueryMultipleAsync(sql,new { userId}))
                {
                    result.Account = await multi.ReadFirstOrDefaultAsync<TransactionAccountQueryResult>();
                    result.TransactionItems = (await multi.ReadAsync<TransactionItemsQueryResult>()).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
