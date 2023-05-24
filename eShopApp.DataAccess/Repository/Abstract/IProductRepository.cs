using eShopApp.Entity.Entities;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;

namespace eShopApp.DataAccess.Repository.Abstract
{
    /// <summary>
    /// Mehsul ile elaqeli operasiyalarin skletini saxlayir.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(int ID, bool DisableChangeTracker);
        List<Product> GetProductsByCategoryID(int? CategoryID, int Page, int ProductCountByPage, bool DisableChangeTracker);
        int GetProductCountByCategoryID(int? CategoryID, bool DisableChangeTracker);
        List<Product> GetHomePageProducts(bool DisableChangeTracker);
        List<Product> GetSearchResult(string SearchString, bool DisableChangeTracker);
        Product GetByIdWithCategories(int id, bool DisableChangeTracker);
    }
}