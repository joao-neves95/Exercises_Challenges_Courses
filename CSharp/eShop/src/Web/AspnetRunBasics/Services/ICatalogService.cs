using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Models.ProductModel>> GetCatalog();
        Task<IEnumerable<Models.ProductModel>> GetCatalogByCategory(string category);
        Task<Models.ProductModel> GetCatalog(string id);
        Task<Models.ProductModel> CreateCatalog(Models.ProductModel model);
    }
}
