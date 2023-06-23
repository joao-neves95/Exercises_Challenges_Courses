
namespace eShop.Ordering.Application.Features.Order.Queries.GetOrdersList
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public BillingInformationDto BillingInformation { get; set; }

        public PaymentInformationDto PaymentInformation { get; set; }
    }
}
