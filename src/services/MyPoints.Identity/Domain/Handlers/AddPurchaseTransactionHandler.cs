using AutoMapper;
using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Handlers
{
    public class AddPurchaseTransactionHandler : Notifiable, IHandler<AddPurchaseTransactionCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AddPurchaseTransactionHandler> _logger;
        private readonly IMessageService _message;
        private readonly IMediator _mediator;

        public AddPurchaseTransactionHandler(IMapper mapper, ILogger<AddPurchaseTransactionHandler> logger, IMessageService message, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _message = message;
            _mediator = mediator;
        }

        public async Task Handle(AddPurchaseTransactionCommand request, CancellationToken cancellationToken)
        {
            _message.Enqueue("add-purchase-transaction", request);
        }
    }
}
