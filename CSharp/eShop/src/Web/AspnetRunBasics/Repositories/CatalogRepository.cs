using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AspnetRunBasics.Data;
using AspnetRunBasics.Models;

using MongoDB.Driver;

namespace AspnetRunBasics.Services
{
    public class CatalogRepository : ICatalogService
    {
        private readonly ICatalogContext _context;

        public CatalogRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Models.ProductModel>> GetCatalog()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task<Models.ProductModel> GetCatalog(string id)
        {
            return await _context.Products.Find(data => data.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Models.ProductModel>> GetCatalogByCategory(string categoryName)
        {
            return await _context
                .Products
                .Find(Builders<ProductModel>.Filter.Eq(data => data.Category, categoryName))
                .ToListAsync();
        }

        public async Task<Models.ProductModel> CreateCatalog(Models.ProductModel product)
        {
            await _context.Products.InsertOneAsync(product);

            return product;
        }
    }
}
