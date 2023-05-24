using eShopApp.Entity.Entities;
using eShopApp.Business.Services.Abstract;
using eShopApp.DataAccess.Repository.Abstract;

namespace eShopApp.Business.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        #region IValidator
        public string ErrorMessage { get; set; }

        public bool Validate(Category entity)
        {
            /* Ilk bawda model valid olmuw olsun: */
            bool isValid = true;

            if (string.IsNullOrEmpty(entity.CategoryName))
            {
                ErrorMessage += "Kateqoriya adi bow buraxila bilmez!\n";
                isValid = false;
            }

            // ve s. kateqoriya ile elaqeli validasiyalar...

            return isValid;
        }
        #endregion IValidator

        public bool Update(Category entity)
        {
            if (Validate(entity))
            {
                _categoryRepository.Update(entity);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(Category entity)
        {
            if (Validate(entity))
            {
                _categoryRepository.Create(entity);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll(true);
        }

        public Category GetByID(int ID)
        {
            return _categoryRepository.GetByID(ID, true);
        }

        public Category GetByIdWithProducts(int CategoryID)
        {
            return _categoryRepository.GetByIdWithProducts(CategoryID, true);
        }

        public void DeleteProductFromCategory(int ProductID, int CategoryID)
        {
            _categoryRepository.DeleteProductFromCategory(ProductID, CategoryID);
        }
    }
}