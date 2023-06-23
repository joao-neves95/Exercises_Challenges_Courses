using eShop.Basket.Api.Entities;

namespace eShop.Basket.Api.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetAsync(string username);

        Task<ShoppingCart?> UpdateAsync(ShoppingCart shoppingCart);

        Task<bool> DeleteAsync(string username);
    }
}
