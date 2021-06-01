using MyPoints.Account.Domain.Enums;
using MyPoints.Account.Domain.Interfaces;

namespace MyPoints.Account.Domain.Commands.Output
{
    public class AddTransactionCommandResult: ICommandResult
    {
        public int Id { get; set; }
        public ETransactionType TransactionTypeId { get; set; }
        public decimal Value { get; set; }
        public int? ProductId { get; set; }
    }
}
