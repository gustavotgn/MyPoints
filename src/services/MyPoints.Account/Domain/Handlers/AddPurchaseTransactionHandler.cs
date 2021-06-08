using AutoMapper;
using Flunt.Notifications;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Queries;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Handlers
{
    public class AddPurchaseTransactionHandler : Notifiable,
        IHandler<AddPurchaseTransactionCommand, AddPurchaseTransactionCommandResult>
    {
        private readonly IAccountContext _context;
        private readonly IMessageService _message;
        private readonly IMapper _mapper;

        public AddPurchaseTransactionHandler(IAccountContext context, IMessageService message, IMapper mapper)
        {
            _context = context;
            _message = message;
            _mapper = mapper;
        }

        public async Task<ResultWithData<AddPurchaseTransactionCommandResult>> Handle(AddPurchaseTransactionCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddPurchaseTransactionCommandResult>.Failed(request.Notifications);
            }
            var account = await _context.Account.GetByUserIdAsync(request.UserId);

            if (account == null)
            {
                AddNotification(new Notification("Account", "Account not found, call the support"));
                request.AddNotifications(this.Notifications);
                request.StatusId = Enums.EOrderStatus.PaymentRejected;
                _message.Enqueue("payment-rejected",request );
                return ResultWithData<AddPurchaseTransactionCommandResult>.Failed(this.Notifications);
            }

            if ((account.Amount + request.Value) < 0)
            {
                AddNotification(new Notification("Account", "insuficient balance, recharge and reprocess the order"));

            }
            if (Invalid)
            {
                request.StatusId = Enums.EOrderStatus.PaymentRejected;
                request.AddNotifications(this.Notifications);
                _message.Enqueue("payment-rejected",request );
                return ResultWithData<AddPurchaseTransactionCommandResult>.Success(null);
            }
            TransactionItemQueryResult transaction = await _context.Transaction.GetByOrderIdAsync(request.OrderId);

            AddPurchaseTransactionCommandResult result;
            if (transaction is null)
            {
                result = await _context.Transaction.AddAsync(request);
            }
            else
            {
                result = _mapper.Map<AddPurchaseTransactionCommandResult>(transaction);
            }


            _message.Enqueue("payment-made", result);

            return ResultWithData<AddPurchaseTransactionCommandResult>.Success(result);
        }
    }
}
