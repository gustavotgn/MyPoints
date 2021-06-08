using MyPoints.Catalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Queries
{
    public class OrderQueryResult
    {
        public int Id { get; set; }
        internal int ProductId { get; set; }
        internal int UserId { get; set; }
        public string Product { get; set; }
        public decimal Value { get; set; }
        public string Status { get; set; }
        public int TransactionId { get; set; }
        internal int? AddressId { get; set; }
        public OrderAddressQueryResult Address { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<OrderNotificationQueryResult> Notifications { get; set; }
        internal bool IsRegisteredAddress { get; set; }
        internal EOrderStatus StatusId { get; set; }
    }
}
