using eShop.Discount.Api.Models.Config;

using Microsoft.Extensions.Options;

using Dapper;
using Npgsql;

namespace eShop.Discount.Api.Data
{
    public class DiscountContext : IDiscountContext
    {
        private readonly PgsqlConfig _pgsqlConfig;

        public DiscountContext(IOptions<PgsqlConfig> pgsqlConfig)
        {
            _pgsqlConfig = (pgsqlConfig ?? throw new ArgumentNullException(nameof(pgsqlConfig))).Value;
        }

        public NpgsqlConnection CreateNewConnection()
        {
            return new NpgsqlConnection(_pgsqlConfig.ConnectionString);
        }

        public static void SeedDevelopmentData(PgsqlConfig pgsqlConfig)
        {
            using var conn = new NpgsqlConnection(pgsqlConfig.ConnectionString);

            bool exists;

            try
            {
                exists = conn.ExecuteScalar<bool>("SELECT EXISTS( SELECT COUNT(1) FROM Coupon )");
            }
            catch (NpgsqlException)
            {
                exists = false;
            }

            if (exists)
            {
                return;
            }

            conn.Execute(
                @"CREATE TABLE Coupon(
                    ID          SERIAL PRIMARY KEY NOT NULL,
                    ProductName VARCHAR(24) NOT NULL,
                    Description TEXT,
                    Amount      INT
                  );

                  INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);
                  INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);");
        }
    }
}
