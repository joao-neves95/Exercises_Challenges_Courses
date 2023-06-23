using eShop.Discount.Grpc.Protos;

namespace eShop.Basket.Api.Services
{
    public class DiscountGrpClient : IDiscountService
    {
        private readonly DiscountGrpcService.DiscountGrpcServiceClient _client;

        public DiscountGrpClient(DiscountGrpcService.DiscountGrpcServiceClient client)
        {
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponModel> GetCouponAsync(string productName)
        {
            return await _client.GetAsync(new GetDiscountRequest() { ProductName = productName });
        }
    }
}
