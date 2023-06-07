
namespace eShop.Ordering.Application.Models
{
    public class EmailConfig
    {
        public const string KeyName = "EmailSettings";

        public string ApiKey { get; set; }

        public string FromAddress { get; set; }

        public string FromName { get; set; }
    }
}
