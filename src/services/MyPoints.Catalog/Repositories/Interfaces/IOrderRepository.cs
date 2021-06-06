using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<AddOrderCommandResult> AddAsync(AddOrderCommand request);
        Task<IList<OrderQueryResult>> GetByUserIdAsync(int userId);
        Task<InvalidOrderCommandResult> UpdateOrderAsync(InvalidOrderCommand invalidOrderCommand);
        Task<PaymentRejectedCommandResult> UpdateOrderAsync(PaymentRejectedCommand request);
        Task UpdateOrderAsync(UpdateOrderStatusCommand request);
        Task AddAddressAsync(RegisterOrderAddressCommand request);
    }
}
