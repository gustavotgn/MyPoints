using AutoMapper;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Dtos;

namespace MyPoints.Catalog.Domain.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddOrderCommand, AddOrderCommandResult>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(x => x.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<AddOrderAddressCommand, AddOrderAddressCommandResult>();

            CreateMap<AddOrderCommandResult, AddPurchaseTransactionDto>()
                .ForMember(x => x.OrderId,opt => opt.MapFrom(src => src.Id));

            CreateMap<AddOrderCommandResult, ValidateAddressDto>()
                .ForMember(x => x.OrderId,opt => opt.MapFrom(src => src.Id));

            CreateMap<InvalidOrderCommand, InvalidOrderCommandResult>();

        }
    }
}
