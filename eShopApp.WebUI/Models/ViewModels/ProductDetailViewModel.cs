using eShopApp.Entity.Entities;

namespace eShopApp.WebUI.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
