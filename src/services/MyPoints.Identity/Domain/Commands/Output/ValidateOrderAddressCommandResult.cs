using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Commands.Output
{
    public class ValidateOrderAddressCommandResult : ICommandResult
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public ETransactionType TransactionTypeId { get; set; }
        public decimal Value { get; set; }
        public int ProductId { get; set; }
    }
}
