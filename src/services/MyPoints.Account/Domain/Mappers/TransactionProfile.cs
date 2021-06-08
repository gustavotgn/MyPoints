using AutoMapper;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Queries;
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

            CreateMap<AddPurchaseTransactionCommand, AddPurchaseTransactionCommandResult>();
            CreateMap<AddRechargeTransactionCommand, AddRechargeTransactionCommandResult>();

            CreateMap<TransactionItemQueryResult, AddPurchaseTransactionCommandResult>()
                .ForMember(opt => opt.TransactionId,member => member.MapFrom(src => src.Id));
        }
    }
}
