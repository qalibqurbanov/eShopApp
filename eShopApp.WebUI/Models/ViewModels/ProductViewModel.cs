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

        [Required]
        [StringLength(maximumLength:50, MinimumLength = 3)]
        [Display(Name = "Name", Prompt = "Enter product name")]
        public string ProductName        { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Price", Prompt = "Enter product price")]
        public double? ProductPrice      { get; set; }

        [Required]
        [Display(Name = "Description", Prompt = "Enter product description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Image")]
        public string? ProductImageName  { get; set; }

        [Display(Name = "Is product approved")]
        public bool ProductIsApproved    { get; set; }

        [Display(Name = "Show on homepage")]
        public bool ProductIsHome        { get; set; }

        /* Adini 'SelectedCategories' ona gore qoymuwam ki sehifede bu kolleksiya komeile iwareliyecem checkboxlari, yeni bu kolleksiya - mehsulun hansi kateqoriyalara aid olmasindan elave hemde hansi kateqoriyalarin secileceyinide ifade edir: */
        public List<Category> SelectedCategories { get; set; }
    }
}

/* Yuxarida Data Annotation-lar ile teleb etdiyim validasiya qaydalarini gedib 'Product' sinifinin ozunde yazmagim dogru olmazdi - cunki 'Entity' qatini tek 'UI' qati iwletmeyecek ve hemin bu diger qatlarda 'Product' ferqli meqsedlerle iwledile biler - sebeb budur. */