using eShop.Catalog.Api.Entities;

using MongoDB.Driver;

namespace eShop.Catalog.Api.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<DataProduct> Products { get; }

        Task SeedDataAsync();
    }
}
