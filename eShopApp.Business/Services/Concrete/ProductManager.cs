using eShopApp.Entity.Entities;
using eShopApp.Business.Services.Abstract;
using eShopApp.DataAccess.UnitOfWork.Abstract;

namespace eShopApp.Business.Services.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region IValidator
        public string ErrorMessage { get; set; }

        public bool Validate(Product entity)
        {
            /* Ilk bawda model valid olmuw olsun: */
            bool isValid = true;

            if(string.IsNullOrEmpty(entity.ProductName))
            {
                ErrorMessage += "Mehsul adi bow buraxila bilmez!\n";
                isValid = false;
            }

            if (entity.ProductPrice < 0)
            {
                ErrorMessage += "Mehsulun qiymeti menfi ola bilmez!\n";
                isValid = false;
            }

            // ve s. mehsul ile elaqeli validasiyalar...

            return isValid;
        }
        #endregion IValidator

        public bool Update(Product entity)
        {
            if (Validate(entity))
            {
                _unitOfWork.Products.Update(entity);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Product entity, int[] CategoryIDs)
        {
            if (Validate(entity))
            {
                if(CategoryIDs.Length == 0 || CategoryIDs == null)
                {
                    /* Mehsulun aid oldugu kateqoriyalarida hazirki 'Update(Product, int[])' metodu icerisinde yenileyecem deye burada qayda qoyuram ki - yenilemek istediyim mehsul en az 1 kateqoriyaya aid edilmelidir. */
                    ErrorMessage += "Mehsulun ugurla yenilene bilmeyi ucun en az 1 kateqoriya secilmelidir.";
                    return false;
                }
                else
                {
                    _unitOfWork.Products.Update(entity, CategoryIDs);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Create(Product entity)
        {
            if(Validate(entity))
            {
                _unitOfWork.Products.Create(entity);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(Product entity, int[] CategoryIDs)
        {
            if (Validate(entity))
            {
                _unitOfWork.Products.Create(entity, CategoryIDs);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(Product entity)
        {
            _unitOfWork.Products.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _unitOfWork.Products.GetAll(true);
        }

        public Product GetByID(int ID)
        {
            return _unitOfWork.Products.GetByID(ID, true);
        }

        public List<Product> GetHomePageProducts()
        {
            return _unitOfWork.Products.GetHomePageProducts(true);
        }

        public int GetProductCountByCategoryID(int? CategoryID)
        {
            return _unitOfWork.Products.GetProductCountByCategoryID(CategoryID, true);
        }

        public Product GetProductDetails(int ID)
        {
            return _unitOfWork.Products.GetProductDetails(ID, true);
        }

        public List<Product> GetProductsByCategoryID(int? CategoryID, int Page, int ProductCountByPage)
        {
            return _unitOfWork.Products.GetProductsByCategoryID(CategoryID, Page, ProductCountByPage, true);
        }

        public List<Product> GetSearchResult(string SearchString)
        {
            return _unitOfWork.Products.GetSearchResult(SearchString, true);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _unitOfWork.Products.GetByIdWithCategories(id, true);
        }
    }
}