using System.ComponentModel.DataAnnotations.Schema;
using eShop.Ordering.Domain.Common;

namespace eShop.Ordering.Domain.Entities
{
    public class DataOrder : EntityBase
    {
        public string UserName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public DataBillingInformation BillingInformation { get; set; }

        public DataPaymentInformation PaymentInformation { get; set; }
    }
}
