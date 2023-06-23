using eShop.Discount.Shared.Data;
using eShop.Discount.Shared.Entities;

using Dapper;

namespace eShop.Discount.Shared.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountContext _discountContext;

        public DiscountRepository(IDiscountContext discountContext)
        {
            _discountContext = discountContext ?? throw new ArgumentNullException(nameof(discountContext));
        }

        public async Task<IEnumerable<Coupon>> GetAll()
        {
            using var conn = _discountContext.CreateNewConnection();

            var coupons = await conn.QueryAsync<Coupon>(
                @"
                 SELECT *
                 FROM Coupon
                "
            );

            return coupons?.Any() == false
                ? Enumerable.Empty<Coupon>()
                : coupons!;
        }

        public async Task<Coupon> Get(string productName)
        {
            using var conn = _discountContext.CreateNewConnection();

            var coupon = await conn.QueryFirstOrDefaultAsync<Coupon>(
                @"SELECT *
                  FROM Coupon
                  WHERE ProductName = @ProductName
                ",
                new { ProductName = productName }
            );

            return coupon ?? new Coupon()
            {
                Amount = 0,
                ProductName = productName,
            };
        }

        public async Task<bool> Create(Coupon coupon)
        {
            using var conn = _discountContext.CreateNewConnection();

            var created = await conn.ExecuteAsync(
                @"INSERT INTO Coupon(ProductName, Description, Amount)
                  VALUES(@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            return created > 0;
        }

        public async Task<bool> Update(Coupon coupon)
        {
            using var conn = _discountContext.CreateNewConnection();

            var updated = await conn.ExecuteAsync(
                @"UPDATE Coupon
                  SET ProductName = @ProductName,
                      Description = @Description,
                      Amount = @Amount
                  WHERE Id = @Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            return updated > 0;
        }

        public async Task<bool> Delete(string productName)
        {
            using var conn = _discountContext.CreateNewConnection();

            var deleted = await conn.ExecuteAsync(
                "DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            return deleted > 0;
        }
    }
}
