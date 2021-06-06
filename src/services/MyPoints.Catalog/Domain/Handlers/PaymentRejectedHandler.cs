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
    public class PaymentRejectedHandler : IHandler<PaymentRejectedCommand, PaymentRejectedCommandResult>
    {
        private readonly ICatalogContext _context;

        public PaymentRejectedHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<ResultWithData<PaymentRejectedCommandResult>> Handle(PaymentRejectedCommand request, CancellationToken cancellationToken)
        {
            PaymentRejectedCommandResult result = await _context.Order.UpdateOrderAsync(request);

            return ResultWithData<PaymentRejectedCommandResult>.Success(result);
        }
    }
}
