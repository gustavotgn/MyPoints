using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Input
{
    public class AddOrderCommand : Notifiable, ICommand<AddOrderCommandResult>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int TransactionId { get; set; }

        [JsonIgnore]
        public string DeliveryAddress { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()

                .IsGreaterThan(UserId, 0, nameof(UserId), "Can not be empty")
                .IsGreaterThan(ProductId, 0, nameof(ProductId), "Can not be empty")
            );
        }
    }
}
