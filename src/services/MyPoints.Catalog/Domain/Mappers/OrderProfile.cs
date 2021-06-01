using AutoMapper;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddOrderCommand, AddOrderCommandResult>();
        }
    }
}
