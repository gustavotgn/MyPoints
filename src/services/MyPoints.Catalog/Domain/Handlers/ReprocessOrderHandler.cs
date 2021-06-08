using AutoMapper;
using Flunt.Notifications;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Dtos;
using MyPoints.Catalog.Domain.Enums;
using MyPoints.Catalog.Domain.Queries;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Handlers
{
    public class ReprocessOrderHandler : Notifiable, IHandler<ReprocessOrderCommand, ReprocessOrderCommandResult>
    {
        private readonly ICatalogContext _context;
        private readonly IMessageService _message;
        private readonly IMapper _mapper;

        public ReprocessOrderHandler(ICatalogContext context, IMessageService message, IMapper mapper)
        {
            _context = context;
            _message = message;
            _mapper = mapper;
        }

        public async Task<ResultWithData<ReprocessOrderCommandResult>> Handle(ReprocessOrderCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                AddNotification(new Notification("AddOrderCommand", "AddOrderCommand can not be null"));
                return ResultWithData<ReprocessOrderCommandResult>.Failed(this.Notifications);
            }
            request?.Validate();
            if (request.Invalid)
            {
                return ResultWithData<ReprocessOrderCommandResult>.Failed(request.Notifications);
            }
            OrderQueryResult order = await _context.Order.GetAsync(request.OrderId, request.UserId);

            if (order is null)
            {
                AddNotification(new Notification("Order", "Order not found"));
                return ResultWithData<ReprocessOrderCommandResult>.Failed(this.Notifications);
            }

            if (order.AddressId.HasValue)
            {
                order.StatusId = EOrderStatus.MakingPayment;
            }
            else
            {
                order.StatusId = EOrderStatus.OrderInProcess;
            }

            await _context.Order.UpdateOrderAsync(order.Id,order.StatusId);

            if (order.AddressId.HasValue)
            {
                _message.Enqueue("add-purchase-transaction", _mapper.Map<AddPurchaseTransactionDto>(order));
            }
            else
            {
                _message.Enqueue("validate-order-address", _mapper.Map<ValidateAddressDto>(order));
            }
            var result = _mapper.Map<ReprocessOrderCommandResult>(order);
            result.Message = "Reprocessing order, wait until processing finishes";

            return ResultWithData<ReprocessOrderCommandResult>.Success(result);
        }
    }
}
