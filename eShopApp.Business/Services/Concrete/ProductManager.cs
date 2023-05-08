using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.Business.Services.Abstract;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete;
using eShopApp.Entity.Entities;

namespace eShopApp.Business.Services.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
                _productRepository.Update(entity);

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
                    /* Mehsulun aid oldugu kateqoriyalarida yeniliyirem 'Update()' icerisinde yenileyecem deye burada qayda qoyuram ki - yenilemek istediyim mehsul en az 1 kateqoriyaya aid edilmelidir. */
                    ErrorMessage += "Mehsulun ugurla yenilene bilmeyi ucun en az 1 kateqoriya secilmelidir.";
                    return false;
                }
                else
                {
                    _productRepository.Update(entity, CategoryIDs);
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
                _productRepository.Create(entity);

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
                _productRepository.Create(entity, CategoryIDs);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll(true);
        }

        public Product GetByID(int ID)
        {
            return _productRepository.GetByID(ID, true);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productRepository.GetHomePageProducts(true);
        }

        public int GetProductCountByCategoryID(int? CategoryID)
        {
            return _productRepository.GetProductCountByCategoryID(CategoryID, true);
        }

        public Product GetProductDetails(int ID)
        {
            return _productRepository.GetProductDetails(ID, true);
        }

        public List<Product> GetProductsByCategoryID(int? CategoryID, int Page, int ProductCountByPage)
        {
            return _productRepository.GetProductsByCategoryID(CategoryID, Page, ProductCountByPage, true);
        }

        public List<Product> GetSearchResult(string SearchString)
        {
            return _productRepository.GetSearchResult(SearchString, true);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productRepository.GetByIdWithCategories(id, true);
        }
    }
}