using MyPoints.Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Queries
{
    public class TransactionQueryResult
    {
        public TransactionAccountQueryResult Account { get; set; }
        public List<TransactionItemQueryResult> TransactionItems { get; set; }
    }

    public class TransactionAccountQueryResult
    {
        public decimal Amount { get; set; }
        public int  Id { get; set; }

    }
    public class TransactionItemQueryResult
    {
        public int Id { get; set; }
        public ETransactionType TransactionTypeId { get; set; }
        public decimal Value { get; set; }
        public decimal AmountAfter { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
    }
}
