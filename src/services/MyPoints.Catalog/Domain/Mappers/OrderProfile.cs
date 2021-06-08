using AutoMapper;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Dtos;
using MyPoints.Catalog.Domain.Queries;

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
                .ForMember(x => x.OrderId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AddOrderCommandResult, ValidateAddressDto>()
                .ForMember(x => x.OrderId, opt => opt.MapFrom(src => src.Id));

            CreateMap<InvalidOrderCommand, InvalidOrderCommandResult>();

            CreateMap<PaymentMadeCommand, PaymentMadeCommandResult>();

            CreateMap<OrderQueryResult, ValidateAddressDto>()
                .ForMember(opt => opt.OrderId, member => member.MapFrom(src => src.Id))
                .ForMember(opt => opt.ProductId, member => member.MapFrom(src => src.ProductId))
                .ForMember(opt => opt.UserId, member => member.MapFrom(src => src.UserId))
                .ForMember(opt => opt.Value, member => member.MapFrom(src => src.Value));

            CreateMap<OrderQueryResult, AddPurchaseTransactionDto>()
                .ForMember(opt => opt.OrderId, member => member.MapFrom(src => src.Id))
                .ForMember(opt => opt.ProductId, member => member.MapFrom(src => src.ProductId))
                .ForMember(opt => opt.UserId, member => member.MapFrom(src => src.UserId))
                .ForMember(opt => opt.Value, member => member.MapFrom(src => src.Value));

            CreateMap<OrderQueryResult, ReprocessOrderCommandResult>();

            CreateMap<OrderAddressQueryResult, AddOrderAddressCommandResult>();
        }
    }
}
