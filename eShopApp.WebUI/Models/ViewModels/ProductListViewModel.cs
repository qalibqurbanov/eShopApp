using eShopApp.Entity.Entities;

namespace eShopApp.WebUI.Models.ViewModels
{
    /// <summary>
    /// View-ya gonderilecek olan mehsullar ve hemin mehsullarin gosterilecek oldugu sehife haqqinda melumatlari saxlayir.
    /// </summary>
    public class ProductListViewModel
    {
        /// <summary>
        /// View-ya gonderilecek olan mehsul.
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// View-ya gonderilecek olan hazirki sehife haqqinda melumatlar.
        /// </summary>
        public PageInfo PageInfo { get; set; }
    }
}
