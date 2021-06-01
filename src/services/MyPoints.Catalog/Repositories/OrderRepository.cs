using AutoMapper;
using Dapper;
using MyPoints.Catalog.Data;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
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

                var id = await _context.Connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO `Order` (Price,UserId,ProductId,TransactionId,DeliveryAddress)
                        VALUES(@Price,@UserId,@ProductId,@TransactionId,@DeliveryAddress);
                        SELECT LAST_INSERT_ID();", request);

                transaction.Commit();

                var result = _mapper.Map<AddOrderCommandResult>(request);
                result.Id = id;
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IList<OrderQueryResult>> GetAsync(int userId)
        {
            try
            {

                var result = await _context.Connection.QueryAsync<OrderQueryResult>(@"
                     SELECT 
                        `Order`.*, 
                        Product.Description AS Product 
                    FROM `Order`
                        INNER JOIN Product ON Product.Id = `Order`.ProductId
                    WHERE Order.UserId = @userId", new { userId });

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
