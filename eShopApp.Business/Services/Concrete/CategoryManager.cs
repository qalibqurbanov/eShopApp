using eShopApp.Entity.Entities;
using eShopApp.Business.Services.Abstract;
using eShopApp.DataAccess.UnitOfWork.Abstract;

namespace eShopApp.Business.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region IValidator
        public string ErrorMessage { get; set; }

        public bool Validate(Category entity)
        {
            /* Ilk bawda model valid olmuw olsun: */
            bool isValid = true;

            if(string.IsNullOrEmpty(entity.CategoryName))
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
            if(Validate(entity))
            {
                _unitOfWork.Categories.Update(entity);
                _unitOfWork.Commit();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(Category entity)
        {
            if(Validate(entity))
            {
                _unitOfWork.Categories.Create(entity);
                _unitOfWork.Commit();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(Category entity)
        {
            _unitOfWork.Categories.Delete(entity);
            _unitOfWork.Commit();
        }

        public void DeleteProductFromCategory(int ProductID, int CategoryID)
        {
            _unitOfWork.Categories.DeleteProductFromCategory(ProductID, CategoryID);
            _unitOfWork.Commit();
        }

        public List<Category> GetAll()
        {
            return _unitOfWork.Categories.GetAll(true);
        }

        public Category GetByID(int ID)
        {
            return _unitOfWork.Categories.GetByID(ID, true);
        }

        public Category GetByIdWithProducts(int CategoryID)
        {
            return _unitOfWork.Categories.GetByIdWithProducts(CategoryID, true);
        }
    }
}