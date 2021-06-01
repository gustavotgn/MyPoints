using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Queries
{
    public class OrderQueryResult
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int TransactionId { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
