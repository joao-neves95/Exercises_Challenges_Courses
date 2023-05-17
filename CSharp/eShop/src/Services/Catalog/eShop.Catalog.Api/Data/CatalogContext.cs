using Microsoft.Extensions.Options;

using eShop.Catalog.Api.Entities;
using eShop.Catalog.Api.Models.Config;

using MongoDB.Driver;

namespace eShop.Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IOptions<MongoDbConfig> mongoDbConfig)
        {
            var client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            Products = database.GetCollection<DataProduct>(mongoDbConfig.Value.CollectionName);

            SeedDataAsync().GetAwaiter().GetResult();
        }

        public IMongoCollection<DataProduct> Products { get; }

        public async Task SeedDataAsync()
        {
            await CatalogContextSeed.SeedDataAsync(Products);
        }
    }
}
