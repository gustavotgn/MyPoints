using AutoMapper;
using Microsoft.Extensions.Configuration;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Repositories;
using MyPoints.Catalog.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;

namespace MyPoints.Catalog.Data
{
    public class CatalogContext : ICatalogContext, IDisposable
    {
        private Guid _id;
        public MySqlConnection Connection { get; }
        private readonly IMapper _mapper;

        public CatalogContext(IConfiguration configuration,IMapper mapper)
        {
            _mapper = mapper;

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Connection = new MySqlConnection(connectionString);
            Connection.Open();

        }

        private IProductRepository _productRepository;
        public IProductRepository Product => _productRepository ?? (_productRepository = new ProductRepository(this, _mapper));

        private IOrderRepository _orderRepository;
        public IOrderRepository Order => _orderRepository ?? (_orderRepository = new OrderRepository(this, _mapper));

        public void Dispose() => Connection?.Dispose();
    }
}
