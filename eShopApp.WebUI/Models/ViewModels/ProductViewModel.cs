using System.ComponentModel.DataAnnotations;
using eShopApp.Entity.Entities;

namespace eShopApp.WebUI.Models.ViewModels
{
    /// <summary>
    /// Mehsul elave etme, editleme formunu temsil eden, bu form-dan datalari controllere ve ya eksine dawiyan sinifdir.
    /// </summary>
    public class ProductViewModel
    {
        public int    ProductID          { get; set; }
        [Display(Name = "Name:", Prompt = "Enter product name")]
        public string ProductName        { get; set; }
        [Display(Name = "Price:", Prompt = "Enter product price")]
        public double ProductPrice       { get; set; }
        [Display(Name = "Description:", Prompt = "Enter product description")]
        public string ProductDescription { get; set; }
        public string ProductImageName   { get; set; }
        [Display(Name = "Is product approved:")]
        public bool ProductIsApproved    { get; set; }
        [Display(Name = "Show on homepage:")]
        public bool ProductIsHome        { get; set; }
    
        /* Adini 'SelectedCategories' ona gore qoymuwam ki sehifede bu kolleksiya komeile iwareliyecem checkboxlari, yeni bu kolleksiya - mehsulun hansi kateqoriyalara aid olmasindan elave hemde hansi kateqoriyalarin secileceyinide ifade edir: */
        public List<Category> SelectedCategories { get; set; }
    }
}