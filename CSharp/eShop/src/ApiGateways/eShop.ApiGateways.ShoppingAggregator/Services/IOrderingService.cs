using eShop.ApiGateways.ShoppingAggregator.Models.Order;

namespace eShop.ApiGateways.ShoppingAggregator.Services
{
    public interface IOrderingService
    {
        Task<IEnumerable<OrderModel>?> GetOrdersByUserName(string userName);
    }
}
