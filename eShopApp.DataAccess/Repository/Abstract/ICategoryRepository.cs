using eShopApp.Entity.Entities;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;

namespace eShopApp.DataAccess.Repository.Abstract
{
    /// <summary>
    /// Kateqoriya ile elaqeli operasiyalarin skletini saxlayir.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByIdWithProducts(int CategoryID, bool DisableChangeTracker);
        void DeleteProductFromCategory(int ProductID, int CategoryID);
    }
}