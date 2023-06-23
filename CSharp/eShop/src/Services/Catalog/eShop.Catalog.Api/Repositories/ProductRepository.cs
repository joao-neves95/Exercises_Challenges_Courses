using eShop.Catalog.Api.Data;
using eShop.Catalog.Api.Entities;

using MongoDB.Driver;

namespace eShop.Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<DataProduct>> GetAllAsync()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<DataProduct>> GetByCategoryAsync(string categoryName)
        {
            return await _context
                .Products
                .Find(Builders<DataProduct>.Filter.Eq(data => data.Category, categoryName))
                .ToListAsync();
        }

        public async Task<DataProduct> GetByIdAsync(string id)
        {
            return await _context.Products.Find(data => data.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataProduct>> GetByNameAsync(string name)
        {
            return await _context
                .Products
                .Find(Builders<DataProduct>.Filter.Eq(data => data.Name, name))
                .ToListAsync();
        }

        public async Task CreateAsync(DataProduct product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateAsync(DataProduct product)
        {
            var result = await _context
                .Products
                .ReplaceOneAsync((data) => data.Id == product.Id, product);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context
                .Products
                .DeleteOneAsync((data) => data.Id == id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
