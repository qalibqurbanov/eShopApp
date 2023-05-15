using Microsoft.AspNetCore.Mvc;
using eShopApp.WebUI.Models.ViewModels;
using eShopApp.Business.Services.Abstract;

namespace eShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ProductListViewModel productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetHomePageProducts()
            };

            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
    }
}