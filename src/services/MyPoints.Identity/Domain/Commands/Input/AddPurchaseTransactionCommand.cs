using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Commands.Input
{
    public class AddPurchaseTransactionCommand : Notifiable, ICommand
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public ETransactionType TransactionTypeId { get => ETransactionType.Purchase; }
        public decimal Value { get; set; }
        public int ProductId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsGreaterThan(OrderId, 0, nameof(OrderId), "OrderId can not be null")
                .IsGreaterThan(UserId, 0, nameof(UserId), "UserId can not be null")
                .IsNotNull(TransactionTypeId, nameof(TransactionTypeId), "TransactionTypeId can not be null")
                .IsGreaterThan(Value, 0, nameof(Value), "Value can not be 0 or less")
                .IsGreaterThan(ProductId, 0, nameof(ProductId), "Value can not be 0 or less")
            );
        }
    }
}
