using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionQueryResult> GetByAccountIdAsync(int userId);
        Task<AddTransactionCommandResult> AddAsync(AddTransactionCommand request);
    }
}
