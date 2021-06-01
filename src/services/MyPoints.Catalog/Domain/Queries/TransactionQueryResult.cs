using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Queries
{
    public class TransactionQueryResult
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Value { get; set; }
        public int? ProductId { get; set; }
    }
}
