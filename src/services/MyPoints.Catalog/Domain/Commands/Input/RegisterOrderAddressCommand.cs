using Flunt.Notifications;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Input
{
    public class RegisterOrderAddressCommand :Notifiable, ICommand<RegisterOrderAddressCommandResult>
    {
        public int OrderId { get; set; }
        public RegisterOrderAddresAddressCommand Address { get; set; }
        public void Validate()
        {

        }
    }
}
