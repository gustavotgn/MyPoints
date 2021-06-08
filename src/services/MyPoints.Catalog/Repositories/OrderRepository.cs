using AutoMapper;
using Dapper;
using MyPoints.Catalog.Data;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Enums;
using MyPoints.Catalog.Domain.Queries;
using MyPoints.Catalog.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CatalogContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(CatalogContext catalogContext, IMapper mapper)
        {
            _context = catalogContext;
            _mapper = mapper;
        }



        public async Task<AddOrderCommandResult> AddAsync(AddOrderCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {
                int? addressId = null;
                if (request.Address != null)
                {
                    addressId = await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO OrderAddress (Street,City,PostalCode,State,Complements,Number)
                            VALUES(@Street,@City,@PostalCode,@State,@Complements,@Number);
                        SELECT LAST_INSERT_ID();", request.Address);
                }
                
                var id = await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO `Order` (Value,UserId,ProductId,AddressId,StatusId)
                        VALUES(@Value,@UserId,@ProductId,@AddressId,@StatusId);
                        SELECT LAST_INSERT_ID();", new { 
                    request.Value,
                    request.UserId,
                    request.ProductId,
                    AddressId = addressId,
                    request.StatusId
                });

                transaction.Commit();

                var result = _mapper.Map<AddOrderCommandResult>(request);
                result.Id = id;
                if (addressId.HasValue)
                {
                    result.Address.Id = addressId.Value;
                }
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IList<OrderQueryResult>> GetByUserIdAsync(int userId)
        {
            try
            {
                var result = new List<OrderQueryResult>();
                var sql = @"
                     SELECT 
                        `Order`.*, 
                        Product.Description AS Product,
                        OrderStatus.Description AS Status
                    FROM `Order`          
                        LEFT JOIN Product ON Product.Id = `Order`.ProductId
                        LEFT JOIN OrderStatus ON OrderStatus.Id = `Order`.StatusId
                    WHERE Order.UserId = @userId;
                    SELECT `OrderNotification`.* FROM `OrderNotification` 
                        INNER JOIN `Order` ON `OrderNotification`.OrderId = `Order`.Id
                    WHERE Order.UserId = @userId;
                    SELECT OrderAddress.* FROM OrderAddress
                        INNER JOIN `Order` ON  `Order`.AddressId = OrderAddress.Id
                    WHERE Order.UserId = @userId;";

                using (var multi = await _context.Connection.QueryMultipleAsync(sql, new { userId }))
                {
                    result = (await multi.ReadAsync<OrderQueryResult>()).ToList();

                    var notifications = await multi.ReadAsync<OrderNotificationQueryResult>();                    
                    result.ForEach( order => order.Notifications = notifications.Where(notification => notification.OrderId == order.Id).ToList()); 
                    
                    var addresses = await multi.ReadAsync<OrderAddressQueryResult>();                    
                    result.ForEach( order => order.Address = addresses.FirstOrDefault(address => address.Id == order.AddressId));
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<InvalidOrderCommandResult> UpdateOrderAsync(InvalidOrderCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {
                request.Notifications.ToList().ForEach(async x =>
                {
                    await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO `OrderNotification`(Property,Message,StatusId,OrderId)
                            VALUES(@Property,@Message,@StatusId,@OrderId);
                        SELECT LAST_INSERT_ID();", new { 
                        x.Property,
                        x.Message,
                        request.StatusId,
                        request.OrderId
                    });
                });

                var id = await _context.Connection.ExecuteAsync(@"
                        UPDATE `Order` SET StatusId = @StatusId
                        WHERE Id = @OrderId", request);

                transaction.Commit();

                var result = _mapper.Map<InvalidOrderCommandResult>(request);
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<PaymentRejectedCommandResult> UpdateOrderAsync(PaymentRejectedCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {
                request.Notifications.ToList().ForEach(async x =>
                {
                    await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO `OrderNotification`(Property,Message,StatusId,OrderId)
                            VALUES(@Property,@Message,@StatusId,@OrderId);
                        SELECT LAST_INSERT_ID();", new {
                        x.Property,
                        x.Message,
                        request.StatusId,
                        request.OrderId
                    });
                });

                var id = await _context.Connection.ExecuteAsync(@"
                        UPDATE `Order` SET StatusId = @StatusId
                        WHERE Id = @OrderId", request);

                transaction.Commit();

                var result = _mapper.Map<PaymentRejectedCommandResult>(request);
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task UpdateOrderAsync(int orderId, EOrderStatus statusId)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {

                var id = await _context.Connection.ExecuteAsync(@"
                        UPDATE `Order` SET StatusId = @StatusId
                        WHERE Id = @orderId AND StatusId < @statusId", new { orderId, statusId});

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task AddAddressAsync(RegisterOrderAddressCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {
                
                var addressId=  await _context.Connection.QueryFirstOrDefaultAsync<int?>("SELECT AddressId FROM `Order` WHERE Id = @OrderId", request);
                if (addressId.HasValue)
                {
                    await _context.Connection.ExecuteScalarAsync<int>(@"
                        UPDATE Address SET Street = @Street, City = @City,PostalCode = @PostalCode,State = @State,Complements = @Complements,Number = @Number
                        WHERE Id = @id;", new { id = addressId.Value, request.Address});
                }
                else
                {
                    var id = await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO `OrderAddress` (Street,City,PostalCode,State,Complements,Number)
                        VALUES(@Street,@City,@PostalCode,@State,@Complements,@Number);
                        SELECT LAST_INSERT_ID();", request.Address);

                    await _context.Connection.ExecuteAsync(@"
                        UPDATE `Order` SET AddressId = @addressId WHERE Id = @OrderId;", new { AddressId = id, request.OrderId});
                }

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<PaymentMadeCommandResult> UpdateOrderAsync(PaymentMadeCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {

                var id = await _context.Connection.ExecuteAsync(@"
                        UPDATE `Order` SET StatusId = @StatusId, TransactionId= @TransactionId
                        WHERE Id = @OrderId", request);

                transaction.Commit();

                var result = _mapper.Map<PaymentMadeCommandResult>(request);
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<OrderQueryResult> GetAsync(int id, int userId)
        {
            try
            {
                var result = new OrderQueryResult();
                var sql = @"
                     SELECT 
                        `Order`.*, 
                        Product.Description AS Product,
                        OrderStatus.Description AS Status
                    FROM `Order`          
                        LEFT JOIN Product ON Product.Id = `Order`.ProductId
                        LEFT JOIN OrderStatus ON OrderStatus.Id = `Order`.StatusId
                    WHERE `Order`.UserId = @userId AND `Order`.Id = @id;
                    SELECT `OrderNotification`.* FROM `OrderNotification` 
                        INNER JOIN `Order` ON `OrderNotification`.OrderId = `Order`.Id
                    WHERE `Order`.UserId = @userId AND `Order`.Id = @id;
                    SELECT OrderAddress.* FROM OrderAddress
                        INNER JOIN `Order` ON  `Order`.AddressId = OrderAddress.Id
                    WHERE `Order`.UserId = @userId AND `Order`.Id = @id;";

                using (var multi = await _context.Connection.QueryMultipleAsync(sql, new { id, userId }))
                {
                    result = (await multi.ReadFirstOrDefaultAsync<OrderQueryResult>());

                    result.Notifications = (await multi.ReadAsync<OrderNotificationQueryResult>()).ToList() ;

                    result.Address = await multi.ReadFirstOrDefaultAsync<OrderAddressQueryResult>();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
