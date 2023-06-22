
namespace eShop.ApiGateways.ShoppingAggregator.Models.Basket
{
    public class BasketModel
    {
        public string Username { get; set; }

        public IEnumerable<BasketItemModel> Items { get; set; } = Enumerable.Empty<BasketItemModel>();

        public decimal TotalPrice { get; set; }
    }
}
