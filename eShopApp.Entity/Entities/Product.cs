namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Her bir mehsulu temsil eden model sinifi.
    /// </summary>
    public class Product
    {
        public int     ProductID          { get; set; }
        public string  ProductName        { get; set; }
        public double  ProductPrice       { get; set; }
        public string  ProductDescription { get; set; }
        public string? ProductImageName   { get; set; }
        public bool    ProductIsApproved  { get; set; }
        public bool    ProductIsHome      { get; set; }

        /* Mehsulun birden cox Kateqoriyasi olacaq: */
        public List<ProductCategory> ProductCategories { get; set; }
    }
}