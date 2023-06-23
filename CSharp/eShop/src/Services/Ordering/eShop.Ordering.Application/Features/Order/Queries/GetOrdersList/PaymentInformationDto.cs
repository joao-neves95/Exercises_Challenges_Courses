
namespace eShop.Ordering.Application.Features.Order.Queries.GetOrdersList
{
    public class PaymentInformationDto
    {
        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string Expiration { get; set; }

        public string CVV { get; set; }

        public int PaymentMethod { get; set; }
    }
}
