using eShopApp.Entity.Entities;
using eShopApp.Business.Validation.Abstract;

namespace eShopApp.Business.Services.Abstract
{
    public interface ICategoryService : IValidator<Category>
    {
        /* IRepository */
        Category GetByID(int ID);
        List<Category> GetAll();
        bool Create(Category entity);
        bool Update(Category entity);
        void Delete(Category entity);

        /* ICategoryRepository */
        Category GetByIdWithProducts(int CategoryID);
        void DeleteProductFromCategory(int ProductID, int CategoryID);
    }
}