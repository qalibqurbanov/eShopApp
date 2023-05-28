namespace eShopApp.Entity.Entities
{
    public class OrderItem
    {
        public int     ID        { get; set; }
        public int     OrderID   { get; set; }

        /* Verilmiw sifariw haqqinda miqdar ve qiymet melumatlarini saxlayirlar: */
        public double  Price     { get; set; }
        public int     Quantity  { get; set; }

        /* FK yaratmagimin bir cox xeyri var, hem mehsul haqqinda melumati rahat bir wekilde elde edecem, hem FK sayesinde cedveldeki melumat butovluyunu qorumuw olacam ve s. */
        public int     ProductID { get; set; }
        public Product Product   { get; set; }

        /* 'OrderItem'-in 1 'Order'-i ola biler: */
        public Order   Order     { get; set; }
    }
}