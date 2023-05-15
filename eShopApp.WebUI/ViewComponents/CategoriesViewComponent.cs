using Microsoft.AspNetCore.Mvc;
using eShopApp.Business.Services.Abstract;

namespace shopapp.webui.ViewComponents
{
    /// <summary>
    /// Kateqoriyalari elde etdiyimiz ViewComponent.
    /// </summary>
    public class CategoriesViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        /* VC-in Entry Point-i olan hemin bu 'Invoke()' icerisinde (hemin bu VC-in View-sunda) lazimim olacaq datalari elde ederek gonderirem 'CategoriesViewComponent' VC-nin View-suna: */
        public IViewComponentResult Invoke()
        {
            /* Route-da '{action}' varsa: */
            if (RouteData.Values["action"] != null)
            {
                /* Optional olan '{id}'-ni yaxalayiriq route-dan: */
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            }

            /* 'CategoriesViewComponent'-nin 'View'-suna kateqoriyalari gonderirik: */
            return View(_categoryService.GetAll());
        }
    }
}