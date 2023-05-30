using eShop.Discount.Grpc.Protos;

namespace eShop.Basket.Api.Services
{
    public interface IDiscountService
    {
        public Task<CouponModel> GetCouponAsync(string productName);
    }
}
