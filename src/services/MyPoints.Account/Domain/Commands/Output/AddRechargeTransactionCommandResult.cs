using MyPoints.Account.Domain.Enums;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Commands.Output
{
    public class AddRechargeTransactionCommandResult : ICommandResult
    {
        public int Id { get; set; }
        public ETransactionType TransactionTypeId { get; set; }
        public decimal Value { get; set; }
    }
}
