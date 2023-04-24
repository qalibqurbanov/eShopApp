using eShopApp.Business.Services.Abstract;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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