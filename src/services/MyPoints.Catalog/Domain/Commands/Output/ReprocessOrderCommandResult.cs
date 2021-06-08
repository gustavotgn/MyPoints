using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Output
{
    public class ReprocessOrderCommandResult : ICommandResult
    {
        public string Message { get; set; }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public decimal Value { get; set; }

        public int TransactionId { get; set; }

        public AddOrderAddressCommandResult Address { get; set; }
    }
}
