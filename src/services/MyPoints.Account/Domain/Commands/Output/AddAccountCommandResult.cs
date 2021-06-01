using MyPoints.Account.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Commands.Output
{
    public class AddAccountCommandResult : ICommandResult
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }

    }
}
