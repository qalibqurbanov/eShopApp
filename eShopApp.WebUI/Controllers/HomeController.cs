using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public IActionResult Index()
        {
            ProductListViewModel productViewModel = new ProductListViewModel()
            {
                Products = _productRepository.GetHomePageProducts(true)
            };

            return View(productViewModel);
        }

        // localhost:5000/home/about
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}