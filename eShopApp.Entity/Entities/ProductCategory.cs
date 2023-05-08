using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Mehsul ve Kateqoriyalar arasinda coxun-coxa elaqe qurmaq ucun iwledeceyim 'Cross-Table (ortaq cedvel)'-dir.
    /// </summary>
    public class ProductCategory
    {
        [Key, Column(Order = 0)]
        public int      CategoryID { get; set; }
        public Category Category   { get; set; }

        [Key, Column(Order = 1)]
        public int      ProductID  { get; set; }
        public Product  Product    { get; set; }
    }
}