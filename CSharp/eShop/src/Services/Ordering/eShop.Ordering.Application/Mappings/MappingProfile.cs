using eShop.Ordering.Application.Features.Order.Queries.GetOrdersList;
using eShop.Ordering.Domain.Entities;

using AutoMapper;
using eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder;

namespace eShop.Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataOrder, OrderDto>().ReverseMap();

            // TODO:
            CreateMap<DataOrder, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
