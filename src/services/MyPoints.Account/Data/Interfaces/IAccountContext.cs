using MyPoints.Account.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Data.Interfaces
{
    public interface IAccountContext
    {
        IAccountRepository Account { get; }
        ITransactionRepository Transaction { get; }
    }
}
