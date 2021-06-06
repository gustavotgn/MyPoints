using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Enums;
using System.Text.Json.Serialization;
using MyPoints.CommandContract.Interfaces;


namespace MyPoints.Account.Domain.Commands.Input
{
    public class AddPurchaseTransactionCommand : Notifiable, ICommand<AddPurchaseTransactionCommandResult>
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public ETransactionType TransactionTypeId { get => ETransactionType.Purchase; }
        public EOrderStatus StatusId { get; set; }

        private decimal _value;
        public decimal Value { get => _value  ; set => _value = value > 0 ? value * -1 : value; }

        public int? ProductId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()

               .IsGreaterThan(UserId, 0, nameof(UserId), "Can not be empty")
            );
            if (TransactionTypeId == ETransactionType.Recharge && Value < 0)
            {
                AddNotification(new Notification("Transaction", "Recharge needs be more than 0"));
            }
            if (TransactionTypeId == ETransactionType.Purchase && Value > 0)
            {
                AddNotification(new Notification("Transaction", "Buy needs be less than 0"));
            }
            if (TransactionTypeId == ETransactionType.Purchase && !ProductId.HasValue)
            {
                AddNotification(new Notification("ProductId", "Can not be null"));
            }

            if (TransactionTypeId == ETransactionType.Recharge && ProductId.HasValue)
            {
                AddNotification(new Notification("ProductId", "Can not be field"));
            }
        }
    }
}
