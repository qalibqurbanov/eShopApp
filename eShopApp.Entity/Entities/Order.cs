using eShopApp.Entity.Enums;

namespace eShopApp.Entity.Entities
{
    /// <summary>
    /// Her bir sifariwi temsil eden model sinifi.
    /// </summary>
    public class Order
    {
        public int        ID          { get; set; }
        public int        OrderNumber { get; set; }
        public DateTime   OrderDate   { get; set; }
        public OrderState OrderState  { get; set; }

        public int        UserID      { get; set; }
        public string     FirstName   { get; set; }
        public string     LastName    { get; set; }
        public string     City        { get; set; }
        public string     Address     { get; set; }
        public string     PostalCode  { get; set; }
        public string     Phone       { get; set; }
        public string     Email       { get; set; }
        public string     Note        { get; set; }

        /// <summary>
        /// Userin etdiyi sifariwleri temsil edir.
        /// </summary>
        public List<OrderItem> OrderItems { get; set; } /* 'Order'-in 1-den cox 'OrderItem'-i ola biler */
    }
}