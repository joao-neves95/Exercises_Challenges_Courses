using AspnetRunBasics.Models;
using AspnetRunBasics.Models.Config;

using Microsoft.Extensions.Options;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace AspnetRunBasics.Data
{
    public sealed class CatalogContext : ICatalogContext
    {
        public CatalogContext(IOptions<MongoDbConfig> mongoDbConfig)
        {
            var client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            Products = database.GetCollection<ProductModel>(mongoDbConfig.Value.CollectionName);

            SeedDataAsync().GetAwaiter().GetResult();
        }

        public IMongoCollection<ProductModel> Products { get; }

        public async Task SeedDataAsync()
        {
            await CatalogContextSeed.SeedDataAsync(Products);
        }
    }
}
