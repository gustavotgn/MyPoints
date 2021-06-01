using MyPoints.Catalog.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Data.Interfaces
{
    public interface ICatalogContext
    {
        IProductRepository Product { get; }
        IOrderRepository Order { get; }
    }
}
