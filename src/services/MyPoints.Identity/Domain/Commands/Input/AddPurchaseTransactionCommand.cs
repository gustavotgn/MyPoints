using Flunt.Notifications;
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

        }
    }
}
