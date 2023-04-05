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
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository) => _productRepository = productRepository;

        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetByID(int ID)
        {
            return _productRepository.GetByID(ID);
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(int ID)
        {
            return _productRepository.GetProductDetails(ID);
        }

        public List<Product> GetProductsByCategoryID(int? CategoryID)
        {
            return _productRepository.GetProductsByCategoryID(CategoryID);
        }
    }
}
