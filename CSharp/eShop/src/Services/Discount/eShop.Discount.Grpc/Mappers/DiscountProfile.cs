using eShop.Discount.Grpc.Protos;
using eShop.Discount.Shared.Entities;

using AutoMapper;

namespace eShop.Discount.Grpc.Mappers
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
