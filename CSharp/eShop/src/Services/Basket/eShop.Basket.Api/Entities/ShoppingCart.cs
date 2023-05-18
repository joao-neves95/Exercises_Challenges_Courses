
namespace eShop.Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; }

        public IEnumerable<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
