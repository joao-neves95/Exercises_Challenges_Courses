using eShop.Ordering.Domain.Common;

namespace eShop.Ordering.Domain.Entities
{
    public class DataPaymentInformation : EntityBase
    {
        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string Expiration { get; set; }

        public string CVV { get; set; }

        public int PaymentMethod { get; set; }
    }
}
