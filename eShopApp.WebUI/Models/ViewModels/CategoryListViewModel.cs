using eShopApp.Entity.Entities;

namespace eShopApp.WebUI.Models.ViewModels
{
    /// <summary>
    /// View-ya gonderilecek olan kateqoriyalar haqqinda melumatlari saxlayir.
    /// </summary>
    public class CategoryListViewModel
    {
        /// <summary>
        /// View-ya gonderilecek olan kateqoriyalar.
        /// </summary>
        public List<Category> Categories { get; set; }
    }
}