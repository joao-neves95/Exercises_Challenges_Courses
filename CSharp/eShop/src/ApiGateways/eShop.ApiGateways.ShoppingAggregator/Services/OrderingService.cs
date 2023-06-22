using eShop.ApiGateways.ShoppingAggregator.Extensions;
using eShop.ApiGateways.ShoppingAggregator.Models.Order;

namespace eShop.ApiGateways.ShoppingAggregator.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly HttpClient _client;

        public OrderingService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<OrderModel>?> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/orders/{userName}");
            return await response.ReadContentAs<List<OrderModel>>();
        }
    }
}
