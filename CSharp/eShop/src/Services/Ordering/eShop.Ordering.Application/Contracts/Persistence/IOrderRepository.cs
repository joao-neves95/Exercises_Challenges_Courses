using eShop.Ordering.Domain.Entities;

namespace eShop.Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<DataOrder>
    {
        public Task<IEnumerable<DataOrder>> GetOrdersByUserName(string userName);

        public Task<DataOrder> GetOrderById(int id);
    }
}
