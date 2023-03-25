using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Her bir mehsulu temsil eden model sinifi.
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public bool ProductIsApproved { get; set; }

        /* Mehsulun birden cox Kateqoriyasi olacaq: */
        public List<Order> ProductCategories { get; set; }
    }
}
