using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.Business.Validation.Abstract;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;
using eShopApp.Entity.Entities;

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
