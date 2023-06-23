using eShop.ApiGateways.ShoppingAggregator.Models.Catalog;

namespace eShop.ApiGateways.ShoppingAggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>?> GetCatalog();

        Task<IEnumerable<CatalogModel>?> GetCatalogByCategory(string category);

        Task<CatalogModel?> GetCatalog(string id);
    }
}
