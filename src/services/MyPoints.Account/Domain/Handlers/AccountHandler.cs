using Flunt.Notifications;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Entities;
using MyPoints.Account.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Handlers
{
    public class AccountHandler : Notifiable, 
        IHandler<AddAccountCommand, AddAccountCommandResult>,
        IHandler<AddTransactionCommand, AddTransactionCommandResult>
    {
        private readonly IAccountContext _context;

        public AccountHandler(IAccountContext context)
        {
            _context = context;
        }

        public async Task<ResultWithData<AddAccountCommandResult>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddAccountCommandResult>.Failed(request.Notifications);
            }
            var account = await _context.Account.GetByUserIdAsync(request.UserId);

            if (account != null)
            {
                AddNotification(new Notification("Account", "Account already registered"));

            }
            if (Invalid)
            {
                return ResultWithData<AddAccountCommandResult>.Failed(this.Notifications);
            }

            var result = await _context.Account.AddAsync(request);

            return ResultWithData<AddAccountCommandResult>.Success(result);
        }

        public async Task<ResultWithData<AddTransactionCommandResult>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddTransactionCommandResult>.Failed(request.Notifications);
            }
            var account = await _context.Account.GetByUserIdAsync(request.UserId);

            if (account == null)
            {
                AddNotification(new Notification("Account", "Account not found"));

            }

            if (account.Amount + request.Value < 0)
            {
                AddNotification(new Notification("Account", "insuficient balance"));

            }
            if (Invalid)
            {
                return ResultWithData<AddTransactionCommandResult>.Failed(this.Notifications);
            }

            AddTransactionCommandResult result = await _context.Transaction.AddAsync(request);

            return ResultWithData<AddTransactionCommandResult>.Success(result);        
        }
    }
}
