using Flunt.Notifications;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.CommandContract.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MyPoints.CommandContract.Entities;

namespace MyPoints.Catalog.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<AddOrderCommand, AddOrderCommandResult>
    {
        private readonly ICatalogContext _context;
        private readonly IMessageService _message;

        public OrderHandler(ICatalogContext context, IMessageService restService)
        {
            _context = context;
            _message = restService;
        }

        public async Task<ResultWithData<AddOrderCommandResult>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            //var account = await _message.SendAsync<AccountQueryResult>(UrlNames.GetAccount, null, RestSharp.Method.GET);

            //var user = await _message.SendAsync<UserQueryResult>(UrlNames.GetUser, null, RestSharp.Method.GET);

            //if (user == null)
            //{
            //    AddNotification(new Notification("UserId", "User not found"));
            //    return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);

            //}

            //if (user.Address == null)
            //{
            //    AddNotification(new Notification("Address", "User without Address"));
            //    return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);
            //}
            //ProductAvailableQueryResult product = await _context.Product.GetAsync(request.ProductId);
            //if (product == null)
            //{
            //    AddNotification(new Notification("ProductId", "Product not found"));
            //    return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);

            //}
            //if (account.Amount - product.Price < 0)
            //{
            //    AddNotification(new Notification("Balance", "Insuficient Balance"));
            //    return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);

            //}

            //var transaction = await _message.SendAsync<TransactionQueryResult>(UrlNames.AddTransaction, new {
            //    TransactionTypeId = 2,
            //    Value =  - product.Price,
            //    ProductId = request.ProductId
            //});

            //request.DeliveryAddress = $"{user?.Address?.Street} - {user?.Address?.City} - {user?.Address?.State} - {user?.Address?.PostalCode} {user?.Address?.Complements}";
            //request.TransactionId = transaction.Id;
            //request.Price = product.Price;
            //AddOrderCommandResult result = await _context.Order.AddAsync(request);

            //return ResultWithData<AddOrderCommandResult>.Success(result);
            return ResultWithData<AddOrderCommandResult>.Failed();
        }
    }
}
