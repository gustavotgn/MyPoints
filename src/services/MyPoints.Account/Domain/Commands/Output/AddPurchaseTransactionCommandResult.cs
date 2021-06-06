using MyPoints.Account.Domain.Enums;
using MyPoints.CommandContract.Interfaces;

namespace MyPoints.Account.Domain.Commands.Output
{
    public class AddPurchaseTransactionCommandResult: ICommandResult
    {
        public int TransactionId { get; set; }
        public ETransactionType TransactionTypeId { get; set; }
        public decimal Value { get; set; }
        public int? ProductId { get; set; }
    }
}
