
namespace eShop.Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public string Username { get; set; }

        public IEnumerable<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
