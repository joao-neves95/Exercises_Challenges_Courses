using eShop.Discount.Shared.Entities;

namespace eShop.Discount.Shared.Repositories
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Coupon>> GetAll();

        Task<Coupon> Get(string productName);

        Task<bool> Create(Coupon coupon);

        Task<bool> Update(Coupon coupon);

        Task<bool> Delete(string productName);
    }
}
