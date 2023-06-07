using eShop.Ordering.Application.Contracts.Persistence;
using eShop.Ordering.Domain.Entities;
using eShop.Ordering.Infrastructure.Persistence;

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
            return await GetAsync(
                order => order.UserName == userName,
                order => order.OrderBy(o => o.Id),
                string.Empty,
                true);
        }
    }
}
