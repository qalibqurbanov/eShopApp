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
        public ProductManager(IProductRepository productRepository) => _productRepository = productRepository;

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public void Update(Product entity, int[] CategoryIDs)
        {
            _productRepository.Update(entity, CategoryIDs);
        }

        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }

        public void Create(Product entity, int[] CategoryIDs)
        {
            _productRepository.Create(entity, CategoryIDs);
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