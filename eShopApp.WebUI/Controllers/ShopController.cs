using eShopApp.Business.Services.Abstract;
using eShopApp.Entity.Entities;
using eShopApp.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            this._productService = productService;
        }

        // id - produktlarini elde edeceyim kateqoriyanin id-sini temsil edir.
        public IActionResult List(int? id)
        {
            ProductListViewModel productListVM;

            if (id == null || id == 0)
            {
                productListVM = new ProductListViewModel()
                {
                    Products = _productService.GetAll()
                };
            }
            else
            {
                productListVM = new ProductListViewModel()
                {
                    Products = _productService.GetProductsByCategoryID(id)
                };
            }

            return View(productListVM);
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
                return NotFound();

            Product product = _productService.GetProductDetails((int)id);

            if (product == null)
                return NotFound();

            return View(new ProductDetailViewModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(cat => cat.Category).ToList()
            });
        }
    }
}