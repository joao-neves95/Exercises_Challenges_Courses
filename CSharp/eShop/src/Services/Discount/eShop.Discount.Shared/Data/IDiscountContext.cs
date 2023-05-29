using Npgsql;

namespace eShop.Discount.Shared.Data
{
    public interface IDiscountContext
    {
        public NpgsqlConnection CreateNewConnection();
    }
}
