using AspnetRunBasics.Extensions;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Models.ProductModel>> GetCatalog()
        {
            var response = await _client.GetAsync("/catalog/products");
            return await response.ReadContentAs<List<Models.ProductModel>>();
        }

        public async Task<Models.ProductModel> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/catalog/products/id/{id}");
            return await response.ReadContentAs<Models.ProductModel>();
        }

        public async Task<IEnumerable<Models.ProductModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/catalog/products/category/{category}");
            return await response.ReadContentAs<List<Models.ProductModel>>();
        }

        public async Task<Models.ProductModel> CreateCatalog(Models.ProductModel model)
        {
            var response = await _client.PostAsJson($"/catalog/products", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Models.ProductModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
