using eShop.Basket.Api.Entities;

using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;

namespace eShop.Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<ShoppingCart?> GetAsync(string username)
        {
            var basket = await _distributedCache.GetStringAsync(username);

            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> UpdateAsync(ShoppingCart shoppingCart)
        {
            await _distributedCache.SetStringAsync(shoppingCart.Username, JsonConvert.SerializeObject(shoppingCart));

            return await GetAsync(shoppingCart.Username);
        }

        public async Task<bool> DeleteAsync(string username)
        {
            await _distributedCache.RemoveAsync(username);

            return (await GetAsync(username)) is null;
        }
    }
}
