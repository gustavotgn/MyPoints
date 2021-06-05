using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Commands.Output
{
    public class AddUserAddressCommandResult : ICommandResult
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Complements { get; set; }
    }
}
