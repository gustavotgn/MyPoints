using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Enums;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Commands.Input
{
    public class AddRechargeTransactionCommand : Notifiable, ICommand<AddRechargeTransactionCommandResult>
    {
        public int UserId { get; set; }
        public ETransactionType TransactionTypeId { get => ETransactionType.Recharge; }

        public decimal Value { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()

               .IsGreaterThan(UserId, 0, nameof(UserId), "Can not be empty")
               .IsGreaterThan(Value, 0, nameof(UserId), "Recharge needs be more than 0")
            );
        }
    }
}
