using eShop.Ordering.Domain.Common;

namespace eShop.Ordering.Domain.Entities
{
    public class DataBillingInformation : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string AddressLine { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}
