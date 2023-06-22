using eShop.ApiGateways.ShoppingAggregator.Models.Basket;
using eShop.ApiGateways.ShoppingAggregator.Models.Order;

namespace eShop.ApiGateways.ShoppingAggregator.Models
{
    public class ShoppingModel
    {
        public string Username { get; set; }

        public BasketModel Basket { get; set; }

        public IEnumerable<OrderModel> Orders { get; set; }
    }
}
