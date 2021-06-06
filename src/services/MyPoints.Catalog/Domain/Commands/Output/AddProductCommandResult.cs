using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Commands.Output
{
    public class AddProductCommandResult : ICommandResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }
        public bool IsActive { get; set; }
        public int Count { get; set; }

    }
}
