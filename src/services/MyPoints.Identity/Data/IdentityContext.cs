using AutoMapper;
using Microsoft.Extensions.Configuration;
using MyPoints.Identity.Data.Interfaces;
using MyPoints.Identity.Repositories;
using MyPoints.Identity.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;

namespace MyPoints.Identity.Data
{
    public class IdentityContext : IIdentityContext, IDisposable
    {
        private Guid _id;
        public MySqlConnection Connection { get; }
        private readonly IMapper _mapper;

        public IdentityContext(IConfiguration configuration,IMapper mapper)
        {
            _mapper = mapper;

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Connection = new MySqlConnection(connectionString);
            Connection.Open();

        }
        private IUserRepository _userRepository;
        public IUserRepository User => _userRepository ?? (_userRepository = new UserRepository(this,_mapper));

        private IAddressRepository _addressRepository;
        public IAddressRepository Address => _addressRepository ?? (_addressRepository = new AddressRepository(this, _mapper));

        public void Dispose() => Connection?.Dispose();
    }
}
