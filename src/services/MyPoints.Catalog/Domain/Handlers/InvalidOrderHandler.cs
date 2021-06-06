using Flunt.Notifications;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Handlers
{
    public class InvalidOrderHandler : Notifiable, IHandler<InvalidOrderCommand, InvalidOrderCommandResult>
    {
        private readonly ICatalogContext _context;

        public InvalidOrderHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<ResultWithData<InvalidOrderCommandResult>> Handle(InvalidOrderCommand request, CancellationToken cancellationToken)
        {

            InvalidOrderCommandResult result = await _context.Order.UpdateOrderAsync(request);

            return ResultWithData<InvalidOrderCommandResult>.Success(result);
        }
    }
}
