using MyPoints.Catalog.Domain.Enums;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Output
{
    public class InvalidOrderCommandResult : ICommandResult
    {
        public EOrderStatus StatusId { get; set; }
        public int OrderId { get; set; }

    }
}
