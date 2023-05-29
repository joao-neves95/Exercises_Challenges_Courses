using eShop.Discount.Api.Models.Config;

using Npgsql;

namespace eShop.Discount.Api.Data
{
    public class DiscountContext : IDiscountContext
    {
        private readonly PgsqlConfig _pgsqlConfig;

        public DiscountContext(PgsqlConfig pgsqlConfig)
        {
            _pgsqlConfig = pgsqlConfig ?? throw new ArgumentNullException(nameof(pgsqlConfig));
        }

        public NpgsqlConnection CreateNewConnection()
        {
            return new NpgsqlConnection(_pgsqlConfig.ConnectionString);
        }
    }
}
