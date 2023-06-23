using eShop.ApiGateways.ShoppingAggregator.Models.Basket;

namespace eShop.ApiGateways.ShoppingAggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
