using Flunt.Notifications;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Input
{
    public class ReprocessOrderCommand : Notifiable, ICommand<ReprocessOrderCommandResult>
    {
        public int OrderId { get; set; }
        internal int UserId { get; set; }

        public void Validate()
        {

        }
    }
}
