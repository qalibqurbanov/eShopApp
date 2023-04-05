using eShopApp.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace shopapp.webui.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"] != null)
            {
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            }

            return View(_categoryService.GetAll());
        }
    }
}