using eShop.Ordering.Domain.Common;

namespace eShop.Ordering.Domain.Entities
{
    public class DataOrder : EntityBase
    {
        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public DataBillingInformation BillingInformation { get; set; }

        public DataPaymentInformation PaymentInformation { get; set; }
    }
}