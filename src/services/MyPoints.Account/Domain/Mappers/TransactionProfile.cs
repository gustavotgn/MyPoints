using AutoMapper;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Mappers
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {

            CreateMap<AddTransactionCommand, AddTransactionCommandResult>();
        }
    }
}
