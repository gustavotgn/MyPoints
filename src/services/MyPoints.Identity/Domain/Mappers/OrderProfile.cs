using AutoMapper;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Dtos;
using MyPoints.Identity.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ValidateOrderAddressCommand, InvalidOrderDto>();
            CreateMap<ValidateOrderAddressCommand, AddPurchaseTransactionCommand>();
            CreateMap<ValidateOrderAddressCommand, ValidateOrderAddressCommandResult>();

            CreateMap<AddPurchaseTransactionCommand,UpdateOrderStatusDto>();

            CreateMap<AddressQueryResult, RegisterOrderAddressAddressDto>();

        }
    }
}
