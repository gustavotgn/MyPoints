using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Enums;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Input
{
    public class UpdateOrderStatusCommand : ICommand<UpdateOrderStatusCommandResult>
    {
        public int OrderId { get; set; }
        public EOrderStatus StatusId { get; set; }

        public void Validate()
        {
        }
    }
}
