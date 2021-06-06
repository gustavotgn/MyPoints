using AutoMapper;
using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Data.Interfaces;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Dtos;
using MyPoints.Identity.Domain.Enums;
using MyPoints.Identity.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Handlers
{
    public class ValidateOrderAddressHandler : Notifiable, 
        IHandler<ValidateOrderAddressCommand, ValidateOrderAddressCommandResult>
    {
        private readonly IIdentityContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ValidateOrderAddressHandler> _logger;
        private readonly IMessageService _message;
        private readonly IMediator _mediator;
        public ValidateOrderAddressHandler(IIdentityContext context, IMapper mapper, ILogger<ValidateOrderAddressHandler> logger, IMessageService message, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _message = message;
            _mediator = mediator;
        }

        public async Task<ResultWithData<ValidateOrderAddressCommandResult>> Handle(ValidateOrderAddressCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                AddNotification(new Notification("ValidateAddressCommand", "ValidateAddressCommand can not be null"));
            }
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<ValidateOrderAddressCommandResult>.Failed(request.Notifications);
            }
            AddressQueryResult result = await _context.Address.GetByUserIdAsync(request.UserId);
            if (result is null)
            {
                var dto = _mapper.Map<InvalidOrderDto>(request);
                dto.AddNotification(new Notification("Address", "User without address registered. Please, register a address and reprocess the order"));

                dto.StatusId = EOrderStatus.InvalidOrder;

                _message.Enqueue("invalid-order", dto);
            }
            else
            {
                var newcommand = _mapper.Map<AddPurchaseTransactionCommand>(request);
                await _mediator.Publish(newcommand,cancellationToken);

                var dto = _mapper.Map<RegisterOrderAddressAddressDto>(result);
                _message.Enqueue("register-order-address",new RegisterOrderAddressDto { OrderId = request.OrderId, Address = dto} );
            }

            return ResultWithData<ValidateOrderAddressCommandResult>.Success(_mapper.Map<ValidateOrderAddressCommandResult>(request));
        }



    }
}
