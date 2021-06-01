using AutoMapper;
using Microsoft.Extensions.Configuration;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Repositories;
using MyPoints.Account.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;

namespace MyPoints.Account.Data
{
    public class AccountContext : IAccountContext, IDisposable
    {
        private Guid _id;
        public MySqlConnection Connection { get; }
        private readonly IMapper _mapper;

        public AccountContext(IConfiguration configuration,IMapper mapper)
        {
            _mapper = mapper;

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Connection = new MySqlConnection(connectionString);
            Connection.Open();

        }

        private IAccountRepository _accountRepository;
        public IAccountRepository Account => _accountRepository ?? (_accountRepository = new AccountRepository(this, _mapper));

        private ITransactionRepository _TransactionRepository;
        public ITransactionRepository Transaction => _TransactionRepository ?? (_TransactionRepository = new TransactionRepository(this, _mapper));


        public void Dispose() => Connection?.Dispose();
    }
}
