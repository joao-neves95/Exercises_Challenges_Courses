using eShop.Ordering.Application.Contracts.Persistence;
using eShop.Ordering.Domain.Entities;

using System.Linq.Expressions;

namespace eShop.Ordering.Infrastructure.Persistence
{
    public sealed class OrderRepository : IOrderRepository
    {
        public Task<DataOrder> AddAsync(DataOrder entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DataOrder entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DataOrder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DataOrder>> GetAsync(Expression<Func<DataOrder, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DataOrder>> GetAsync(Expression<Func<DataOrder, bool>> predicate = null, Func<IQueryable<DataOrder>, IOrderedQueryable<DataOrder>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DataOrder>> GetAsync(Expression<Func<DataOrder, bool>> predicate = null, Func<IQueryable<DataOrder>, IOrderedQueryable<DataOrder>> orderBy = null, List<Expression<Func<DataOrder, object>>> includes = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<DataOrder> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataOrder>> GetOrdersByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(DataOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
