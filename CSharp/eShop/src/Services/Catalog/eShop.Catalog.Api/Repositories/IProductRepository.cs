
using eShop.Catalog.Api.Entities;

namespace eShop.Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<DataProduct>> GetAllAsync();

        Task<DataProduct> GetByIdAsync(string id);

        Task<IEnumerable<DataProduct>> GetByNameAsync(string name);

        Task<IEnumerable<DataProduct>> GetByCategoryAsync(string categoryName);

        Task CreateAsync(DataProduct product);

        Task<bool> UpdateAsync(DataProduct product);

        Task<bool> DeleteAsync(string id);
    }
}
