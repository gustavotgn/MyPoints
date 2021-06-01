using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Queries
{
    public class TransactionQueryResult
    {
        public TransactionAccountQueryResult Account { get; set; }
        public List<TransactionItemsQueryResult> TransactionItems { get; set; }
    }

    public class TransactionAccountQueryResult
    {
        public decimal Amount { get; set; }
        public int  Id { get; set; }

    }
    public class TransactionItemsQueryResult
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Value { get; set; }
        public decimal AmountAfter { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ProductId { get; set; }
    }
}
