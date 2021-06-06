using MyPoints.Catalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Dtos
{
    public class AddPurchaseTransactionDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public ETransactionType TransactionTypeId { get => ETransactionType.Purchase; }
        public decimal Value { get; set; }
        public int ProductId { get; set; }
    }
}
