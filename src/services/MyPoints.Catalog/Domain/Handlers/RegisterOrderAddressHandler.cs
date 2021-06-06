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
    public class RegisterOrderAddressHandler : IHandler<RegisterOrderAddressCommand, RegisterOrderAddressCommandResult>
    {
        private readonly ICatalogContext _context;

        public RegisterOrderAddressHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<ResultWithData<RegisterOrderAddressCommandResult>> Handle(RegisterOrderAddressCommand request, CancellationToken cancellationToken)
        {
            await _context.Order.AddAddressAsync(request);

            return ResultWithData<RegisterOrderAddressCommandResult>.Success(null);
        }
    }
}
