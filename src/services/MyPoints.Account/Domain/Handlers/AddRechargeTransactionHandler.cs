using AutoMapper;
using Flunt.Notifications;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Handlers
{
    public class AddRechargeTransactionHandler : Notifiable, IHandler<AddRechargeTransactionCommand, AddRechargeTransactionCommandResult>
    {
        private readonly IAccountContext _context;
        private readonly IMessageService _message;
        private readonly IMapper _mapper;

        public AddRechargeTransactionHandler(IAccountContext context, IMessageService message, IMapper mapper)
        {
            _context = context;
            _message = message;
            _mapper = mapper;
        }

        public async Task<ResultWithData<AddRechargeTransactionCommandResult>> Handle(AddRechargeTransactionCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddRechargeTransactionCommandResult>.Failed(request.Notifications);
            }
            var account = await _context.Account.GetByUserIdAsync(request.UserId);

            if (account == null)
            {
                AddNotification(new Notification("Account", "Account not found, call the support"));

                return ResultWithData<AddRechargeTransactionCommandResult>.Failed(this.Notifications);
            }

            if (Invalid)
            {
                return ResultWithData<AddRechargeTransactionCommandResult>.Failed(this.Notifications);
            }

            AddRechargeTransactionCommandResult result = await _context.Transaction.AddAsync(request);

            return ResultWithData<AddRechargeTransactionCommandResult>.Success(result);
        }
    }
}
