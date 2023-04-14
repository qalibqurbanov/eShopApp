using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Mehsul ve Kateqoriyalar arasinda coxun-coxa elaqe qurmaq ucun iwledeceyim 'Cross-Table (ortaq cedvel)'-dir.
    /// </summary>
    public class ProductCategory
    {
        public int      CategoryID { get; set; }
        public Category Category   { get; set; }

        public int      ProductID  { get; set; }
        public Product  Product    { get; set; }
    }
}