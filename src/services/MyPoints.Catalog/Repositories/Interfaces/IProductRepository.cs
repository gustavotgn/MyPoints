using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<AddProductCommandResult> AddAsync(AddProductCommand request);
        Task<IList<ProductAvailableQueryResult>> GetAvailableAsync();
        Task<ProductAvailableQueryResult> GetAsync(int id);
    }
}
