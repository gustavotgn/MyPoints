using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Queries
{
    public class AccountQueryResult
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}
