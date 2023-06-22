using eShop.ApiGateways.ShoppingAggregator.Extensions;
using eShop.ApiGateways.ShoppingAggregator.Models.Basket;

namespace eShop.ApiGateways.ShoppingAggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BasketModel?> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
