namespace eShopApp.WebUI.Models.Cart
{
    /// <summary>
    /// 'Cart (Sebet)' ile elaqeli melumatlari temsil edir.
    /// </summary>
    public class CartModel
    {
        /// <summary>
        /// Usere tehkim olunmuw 'Cart (Sebet)'-in ID-si.
        /// </summary>
        public int CartID { get; set; }

        /// <summary>
        /// Userin 'Cart (Sebet)'-indeki her bir mehsulu ozunde saxlayir.
        /// </summary>
        public List<CartItemModel> CartItems { get; set; }

        /// <summary>
        /// Userin 'Cart (Sebet)'-na elave etdiyi mehsullarin cemi qiymetini hesablayir.
        /// </summary>
        /// <returns>Geriye userin 'Cart (Sebet)'-na elave etdiyi mehsullarin cemi qiymetini dondurur.</returns>
        public double TotalPrice()
        {
            if(CartItems != null && CartItems.Count > 0)
            {
                return CartItems.Sum(cartItem => cartItem.Price * cartItem.Quantity);
            }

            return 0;
        }
    }

    /// <summary>
    /// 'Cart (Sebet)'-de olan mehsulu temsil edir.
    /// </summary>
    public class CartItemModel
    {
        public int    CartItemID  { get; set; }
        public int    ProductID   { get; set; }
        public string Name        { get; set; }
        public double Price       { get; set; }
        public string Description { get; set; }
        public string ImageName   { get; set; }
        public int    Quantity    { get; set; }
    }
}