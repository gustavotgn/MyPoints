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
        Task<AddPurchaseTransactionCommandResult> AddAsync(AddPurchaseTransactionCommand request);
        Task<AddRechargeTransactionCommandResult> AddAsync(AddRechargeTransactionCommand request);
        Task<TransactionItemQueryResult> GetByOrderIdAsync(int orderId);
    }
}
