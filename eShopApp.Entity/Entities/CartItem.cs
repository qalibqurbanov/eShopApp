namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Sebebte elave olunmuw tek bir mehsulu temsil edir.
    /// </summary>
    public class CartItem
    {
        public int ID { get; set; }

        /// <summary>
        /// Mehsuldan nece dene alindigini temsil edir.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Hazirki sebetde yerlewen mehsulun ID-si.
        /// </summary>
        public int ProductID { get; set; }

        /* 'CartItem' icerisinde dolayi yolla 'ProductID'-li mehsulla elaqe qurmuw oluram, burada mene elave olaraq bir navigation property lazimdir ki, ehtiyac olsa hemin bu 'ProductID'-den istifade ede bilim ve s.: */
        /// <summary>
        /// Product cedvelini istifade etmeyimize imkan yaradan navigation property.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 'CartItem'-in(sebetdeki mehsullarin) hansi 'Cart'-a(sebete) aid oldugunu temsil edir..
        /// </summary>
        public int CartID { get; set; }

        /* Eyni wekilde 'CartID' haqqinda elave melumatlara ehtiyacim olsa ve s. hemin bu navigation property-ni iwledecem: */
        /// <summary>
        /// Cart cedvelini istifade etmeyimize imkan yaradan navigation property.
        /// </summary>
        public Cart Cart { get; set; }
    }
}