using eShopApp.WebUI.Models;
using eShopApp.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using eShopApp.WebUI.Models.ViewModels;
using eShopApp.Business.Services.Abstract;

namespace eShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        public ShopController(IProductService productService)
        {
            this._productService = productService;
        }

        // id - produktlarini elde edeceyim kateqoriyanin id-sini temsil edir.
        // page - query stringden yaxalanacaq.
        public IActionResult List([FromRoute] int? id, [FromQuery] int page = 1) /* Query String-den 'Page' yaxalanmayada biler, yaxalanmasa Page-e 0 gelecek ve xeta alacayiq, bu sebeble hecne yaxalanmasa 'Page' 1 olsun deyirik */
        {
            const int productCountPerPage = 3;

            /* Secilen sehife haqqinda melumatlari ve hemin o secilen sehifede gosterilecek olan mehsullari gonderirik View-ya: */
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
            if (id == null)
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

        public IActionResult Search([FromQuery]string q) /* '_search' partialinda bu 'q' parametrini yerlewdirmiwdik Query Stringe. Burada 'Search()' actioninda hemin 'q'-nin datasini(?q=...) yaxalayiriq */
        {
            var productListVM = new ProductListViewModel()
            {
                Products = _productService.GetSearchResult(q)
            };

            return View(productListVM);
        }
    }
}