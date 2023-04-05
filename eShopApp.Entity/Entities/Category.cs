using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Her bir kateqoriyani temsil eden model sinifi.
    /// </summary>
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        /* Kateqoriyanon birden cox Mehsulu olacaq: */
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
