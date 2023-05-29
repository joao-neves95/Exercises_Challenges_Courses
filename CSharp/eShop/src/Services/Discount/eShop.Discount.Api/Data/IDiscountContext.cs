using Npgsql;

namespace eShop.Discount.Api.Data
{
    public interface IDiscountContext
    {
        public NpgsqlConnection CreateNewConnection();
    }
}
