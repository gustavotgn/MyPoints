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
    public class UpdateOrderStatusHandler : Notifiable, IHandler<UpdateOrderStatusCommand, UpdateOrderStatusCommandResult>
    {
        private readonly ICatalogContext _context;

        public UpdateOrderStatusHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<ResultWithData<UpdateOrderStatusCommandResult>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            await _context.Order.UpdateOrderAsync(request);
            return ResultWithData<UpdateOrderStatusCommandResult>.Success(null);
        }
    }
}
