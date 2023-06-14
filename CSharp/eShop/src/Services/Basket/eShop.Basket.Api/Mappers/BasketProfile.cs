using eShop.Basket.Api.Entities;
using eShop.Shared.EventBus.Messages.Events;

using AutoMapper;

namespace eShop.Basket.Api.Mappers
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
