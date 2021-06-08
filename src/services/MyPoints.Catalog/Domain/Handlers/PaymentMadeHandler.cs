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
    public class PaymentMadeHandler : Notifiable, IHandler<PaymentMadeCommand, PaymentMadeCommandResult>
    {
        private readonly ICatalogContext _context;

        public PaymentMadeHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<ResultWithData<PaymentMadeCommandResult>> Handle(PaymentMadeCommand request, CancellationToken cancellationToken)
        {
            PaymentMadeCommandResult result = await _context.Order.UpdateOrderAsync(request);

            return ResultWithData<PaymentMadeCommandResult>.Success(result);
        }
    }
}
