using System.ComponentModel.DataAnnotations;
using eShopApp.Entity.Entities;

namespace eShopApp.WebUI.Models.ViewModels
{
    /// <summary>
    /// Kateqoriya elave etme, editleme formunu temsil eden, bu form-dan datalari controllere ve ya eksine dawiyan sinifdir.
    /// </summary>
    public class CategoryViewModel
    {
        public int CategoryID         { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Display(Name = "Name", Prompt = "Enter category name")]
        public string CategoryName    { get; set; }

        /// <summary>
        /// Hazirki kateqoriyada olan mehsullari saxlayir.
        /// </summary>
        public List<Product> Products { get; set; }
    }
}
