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
    public class ProductRepository : IProductRepository
    {
        private CatalogContext _context;
        private IMapper _mapper;

        public ProductRepository(CatalogContext CatalogContext, IMapper mapper)
        {
            this._context = CatalogContext;
            _mapper = mapper;
        }

        public async Task<AddProductCommandResult> AddAsync(AddProductCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {

                var id = await _context.Connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO Product (Description,Price,IsActive,Count)
                    VALUES (@Description,@Price,@IsActive,@Count);
                    SELECT LAST_INSERT_ID();", request);
                

                transaction.Commit();

                var result = _mapper.Map<AddProductCommandResult>(request);
                result.Id = id;
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<ProductAvailableQueryResult> GetAsync(int id)
        {
            try
            {

                var product = await _context.Connection.QueryFirstOrDefaultAsync<ProductAvailableQueryResult>(@"
                    SELECT * FROM Product WHERE Count > 0 AND IsActive = 1 AND Id = @id",new { id });

                return product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IList<ProductAvailableQueryResult>> GetAvailableAsync()
        {
            try
            {

                var products = await _context.Connection.QueryAsync<ProductAvailableQueryResult> (@"
                    SELECT * FROM Product WHERE Count > 0 AND IsActive = 1");

                return products.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
