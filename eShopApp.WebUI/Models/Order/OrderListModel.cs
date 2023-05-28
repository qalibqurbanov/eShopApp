using eShopApp.Entity.Enums;

namespace eShopApp.WebUI.Models.Order
{
    /// <summary>
    /// 'Orders' sehifesinde siralayacagim 'Order (Sifariw)'-leri temsil edir.
    /// </summary>
    public class OrderListModel
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

        public List<OrderItemModel> OrderItems { get; set; }

        /// <summary>
        /// Userin sifariw etmiw oldugu mehsullarin cemi qiymetini hesablayir.
        /// </summary>
        /// <returns>Geriye userin sifariw etmiw oldugu mehsullarin cemi qiymetini dondurur.</returns>
        public double TotalPrice()
        {
           return OrderItems.Sum(orderItem => orderItem.OrderItemPrice * orderItem.OrderItemQuantity);
        }
    }

    /// <summary>
    /// 'Orders' sehifesindeki her bir sifariwi temsil edir.
    /// </summary>
    public class OrderItemModel
    {
        public int    OrderItemID        { get; set; }
        public double OrderItemPrice     { get; set; }
        public string OrderItemName      { get; set; }
        public string OrderItemImageName { get; set; }
        public int    OrderItemQuantity  { get; set; }
    }
}