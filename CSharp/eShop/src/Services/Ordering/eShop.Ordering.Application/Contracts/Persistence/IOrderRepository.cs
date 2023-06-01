using eShop.Ordering.Domain.Entities;

namespace eShop.Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<DataOrder>
    {
        Task<IEnumerable<DataOrder>> GetOrdersByUserName(string userName);
    }
}
