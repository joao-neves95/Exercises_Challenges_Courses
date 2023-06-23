using eShop.Ordering.Application.Contracts.Persistence;
using eShop.Ordering.Domain.Entities;
using eShop.Ordering.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace eShop.Ordering.Infrastructure.Repositories
{
    public sealed class OrderRepository : RepositoryBase<DataOrder>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<DataOrder>> GetOrdersByUserName(string userName)
        {
            var includes = new List<Expression<Func<DataOrder, object>>>()
            {
                (data) => data.BillingInformation,
                (data) => data.PaymentInformation,
            };

            return await GetAsync(
                order => order.UserName == userName,
                order => order.OrderBy(o => o.Id),
                includes,
                true);
        }

        public async Task<DataOrder> GetOrderById(int id)
        {
            return await _dbContext.Set<DataOrder>()
                .Include(set => set.PaymentInformation)
                .Include(set => set.BillingInformation)
                .FirstOrDefaultAsync(data => data.Id == id);
        }
    }
}
