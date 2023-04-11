using eShopApp.Business.Services.Abstract;
using eShopApp.Entity.Entities;
using eShopApp.WebUI.Models;
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
        // page - query stringden yaxalanacaq.
        public IActionResult List([FromRoute]int? id, [FromQuery]int page = 1) /* Query String-den 'Page' yaxalanmayada biler, yaxalanmasa Page-e 0 gelecek ve xeta alacayiq, bu sebeble hecne yaxalanmasa 'Page' 1 olsun deyirik */
        {
            // GetAll() lazim deyilse sil
            
            const int productCountPerPage = 3;

            ProductListViewModel productListVM = new ProductListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalProducts = _productService.GetProductCountByCategoryID(id),
                    CurrentPage = page,
                    ProductsPerPage = productCountPerPage,
                    CurrentCategoryID = id
                },
                Products = _productService.GetProductsByCategoryID(id, page, productCountPerPage)
            };

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