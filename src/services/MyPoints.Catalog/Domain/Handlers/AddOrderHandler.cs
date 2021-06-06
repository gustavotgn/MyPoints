using Flunt.Notifications;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.CommandContract.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MyPoints.CommandContract.Entities;
using MyPoints.Catalog.Domain.Queries;
using MyPoints.Catalog.Configurations;
using AutoMapper;
using MyPoints.Catalog.Domain.Dtos;
using MyPoints.Catalog.Domain.Enums;

namespace MyPoints.Catalog.Domain.Handlers
{
    public class AddOrderHandler : Notifiable, IHandler<AddOrderCommand, AddOrderCommandResult>
    {
        private readonly ICatalogContext _context;
        private readonly IMessageService _message;
        private readonly IMapper _mapper;

        public AddOrderHandler(ICatalogContext context, IMessageService restService,  IMapper mapper)
        {
            _context = context;
            _message = restService;
            _mapper = mapper;
        }

        public async Task<ResultWithData<AddOrderCommandResult>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                AddNotification(new Notification("AddOrderCommand", "AddOrderCommand can not be null"));
                return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);
            }
            request?.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddOrderCommandResult>.Failed(request.Notifications);
            }

            ProductAvailableQueryResult product = await _context.Product.GetAsync(request.ProductId);
            if (product == null)
            {
                AddNotification(new Notification("ProductId", "Product not found"));
                return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);

            }
            if (request.IsRegisteredAddress)
            {
                request.StatusId = EOrderStatus.OrderInProcess;
            }
            else
            {
                request.StatusId = EOrderStatus.MakingPayment;
            }

            request.Value = product.Value;
            AddOrderCommandResult result = await _context.Order.AddAsync(request);

            if (request.IsRegisteredAddress)
            {
                _message.Enqueue("validate-order-address", _mapper.Map<ValidateAddressDto>(result));
            }
            else
            {
                _message.Enqueue("add-purchase-transaction", _mapper.Map<AddPurchaseTransactionDto>(result));
            }

            result.Message = "Registered order, wait until processing finishes";

            return ResultWithData<AddOrderCommandResult>.Success(result);
        }
    }
}
