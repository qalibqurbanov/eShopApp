using System.ComponentModel.DataAnnotations;
using eShopApp.WebUI.Models.Cart;

namespace eShopApp.WebUI.Models.Order
{
    /// <summary>
    /// 'Order (Sifariw)' ile elaqeli melumatlari temsil edir.
    /// </summary>
    public class OrderDetailsModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Note { get; set; }

        /* Userin sebetinde olan mehsullarla bagli melumat almaq ucun iwledecem bu propertyni: */
        public CartModel CartModel { get; set; }
    }
}