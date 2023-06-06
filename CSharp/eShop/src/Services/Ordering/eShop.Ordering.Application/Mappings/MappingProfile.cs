using eShop.Ordering.Application.Features.Order.Queries.GetOrdersList;
using eShop.Ordering.Domain.Entities;

using AutoMapper;
using eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder;
using eShop.Ordering.Application.Features.Order.Commands.UpdateOrder;

namespace eShop.Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TODO:
            CreateMap<DataOrder, OrderDto>().ReverseMap();
            CreateMap<DataOrder, CheckoutOrderCommand>().ReverseMap();
            CreateMap<DataOrder, UpdateOrderCommand>().ReverseMap();
        }
    }
}
