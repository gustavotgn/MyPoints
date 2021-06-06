using AutoMapper;
using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Handlers
{
    public class UpdateOrderStatusHandler : Notifiable, IHandler<AddPurchaseTransactionCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderStatusHandler> _logger;
        private readonly IMessageService _message;
        private readonly IMediator _mediator;

        public UpdateOrderStatusHandler(IMapper mapper, ILogger<UpdateOrderStatusHandler> logger, IMessageService message, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _message = message;
            _mediator = mediator;
        }

        public async Task Handle(AddPurchaseTransactionCommand request, CancellationToken cancellationToken)
        {
            
            var dto = _mapper.Map<UpdateOrderStatusDto>(request);
            dto.StatusId = Enums.EOrderStatus.MakingPayment;
            _message.Enqueue("update-order-status", dto);

        }
    }
}
