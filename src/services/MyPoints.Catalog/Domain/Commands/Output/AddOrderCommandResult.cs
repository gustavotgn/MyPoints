using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Output
{
    public class AddOrderCommandResult : ICommandResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int TransactionId { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
