using eShopApp.Entity.Entities;
using eShopApp.Business.Validation.Abstract;

namespace eShopApp.Business.Services.Abstract
{
    public interface IProductService : IValidator<Product>
    {
        /* IRepository */
        Product GetByID(int ID);
        List<Product> GetAll();
        bool Create(Product entity);
        bool Create(Product entity, int[] CategoryIDs);
        bool Update(Product entity);
        bool Update(Product entity, int[] CategoryIDs);
        void Delete(Product entity);

        /* IProductRepository */
        Product GetProductDetails(int ID);
        List<Product> GetProductsByCategoryID(int? CategoryID, int Page, int ProductCountByPage);
        int GetProductCountByCategoryID(int? CategoryID);
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string SearchString);
        Product GetByIdWithCategories(int id);
    }
}