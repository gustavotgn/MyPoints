using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Enums;
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
        internal int UserId { get; set; }

        internal EOrderStatus StatusId { get; set; }

        internal decimal Value { get; set; }


        public int ProductId { get; set; }  

        public bool IsRegisteredAddress { get; set; }

        public AddOrderAddressCommand Address { get; set; }


        public void Validate()
        {
            AddNotifications(new Contract()

                .IsGreaterThan(UserId, 0, nameof(UserId), "Can not be empty")
                .IsGreaterThan(ProductId, 0, nameof(ProductId), "Can not be empty")
            );

            if (!IsRegisteredAddress)
            {
                if (Address is null)
                {
                    AddNotification(new Notification("Address", "Address can not be null"));
                    return;
                }
                Address.Validate();
                if (Address.Invalid)
                {
                    AddNotifications(Address.Notifications);
                }
            }
            else
            {
                if (Address != null)
                {
                    AddNotification(new Notification("Address", "Address can not be filled if Registered Address is selected"));
                    return;
                }
            }
        }
    }
}
