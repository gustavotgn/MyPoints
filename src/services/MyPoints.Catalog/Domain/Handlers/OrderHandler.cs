using Flunt.Notifications;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Entities;
using MyPoints.Catalog.Domain.Interfaces;
using MyPoints.Catalog.Domain.Queries;
using MyPoints.Catalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<AddOrderCommand, AddOrderCommandResult>
    {
        private readonly ICatalogContext _context;
        private readonly IRestService _restService;

        public OrderHandler(ICatalogContext context, IRestService restService)
        {
            _context = context;
            _restService = restService;
        }

        public async Task<ResultWithData<AddOrderCommandResult>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var account = await _restService.SendAsync<AccountQueryResult>(UrlNames.GetAccount, null, RestSharp.Method.GET);

            var user = await _restService.SendAsync<UserQueryResult>(UrlNames.GetUser, null, RestSharp.Method.GET);

            if (user == null)
            {
                AddNotification(new Notification("UserId", "User not found"));
                return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);

            }

            if (user.Address == null)
            {
                AddNotification(new Notification("Address", "User without Address"));
                return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);
            }
            ProductAvailableQueryResult product = await _context.Product.GetAsync(request.ProductId);
            if (product == null)
            {
                AddNotification(new Notification("ProductId", "Product not found"));
                return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);

            }
            if (account.Amount - product.Price < 0)
            {
                AddNotification(new Notification("Balance", "Insuficient Balance"));
                return ResultWithData<AddOrderCommandResult>.Failed(this.Notifications);

            }

            var transaction = await _restService.SendAsync<TransactionQueryResult>(UrlNames.AddTransaction, new {
                TransactionTypeId = 2,
                Value =  - product.Price,
                ProductId = request.ProductId
            });

            request.DeliveryAddress = $"{user?.Address?.Street} - {user?.Address?.City} - {user?.Address?.State} - {user?.Address?.PostalCode} {user?.Address?.Complements}";
            request.TransactionId = transaction.Id;
            request.Price = product.Price;
            AddOrderCommandResult result = await _context.Order.AddAsync(request);

            return ResultWithData<AddOrderCommandResult>.Success(result);

        }
    }
}
