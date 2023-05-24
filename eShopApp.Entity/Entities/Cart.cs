namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Cart (Sebet)'-i temsil edir.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// 'Cart (Sebet)'-in ID-si.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Cart hansi ID-li Usere aiddir.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Cart (Sebet)' icerisinde yerlewen mehsullari temsil edir.
        /// </summary>
        public List<CartItem> CartItems { get; set; }
    }
}