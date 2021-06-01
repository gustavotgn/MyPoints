using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Enums;
using MyPoints.Account.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace MyPoints.Account.Domain.Commands.Input
{
    public class AddTransactionCommand : Notifiable, ICommand<AddTransactionCommandResult>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public ETransactionType TransactionTypeId { get; set; }
        public decimal Value { get; set; }
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
            if (TransactionTypeId == ETransactionType.Buy && Value > 0)
            {
                AddNotification(new Notification("Transaction", "Buy needs be less than 0"));
            }
            if (TransactionTypeId == ETransactionType.Buy && !ProductId.HasValue)
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
