using AspnetRunBasics.Models;

using System.Threading.Tasks;

using MongoDB.Driver;

namespace AspnetRunBasics.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<ProductModel> Products { get; }

        Task SeedDataAsync();
    }
}
