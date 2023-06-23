using eShop.ApiGateways.ShoppingAggregator.Extensions;
using eShop.ApiGateways.ShoppingAggregator.Models.Catalog;

namespace eShop.ApiGateways.ShoppingAggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogModel>?> GetCatalog()
        {
            var response = await _client.GetAsync("/api/v1/catalog/products");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel?> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/api/v1/catalog/products/id/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>?> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/catalog/products/category/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }
    }
}
