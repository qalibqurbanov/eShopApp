using System.ComponentModel.DataAnnotations;

namespace eShopApp.WebUI.Models.IdentityModels.User
{
    /// <summary>
    /// Editlenmesi istenen user haqqinda melumatlari temsil edir.
    /// </summary>
    public class UserEditModel
    {
        public int    UserID         { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName      { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName       { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName       { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email          { get; set; }

        public bool   EmailConfirmed { get; set; }

        /// <summary>
        /// Hazirda userin hansi rollarda oldugunu saxlayir.
        /// </summary>
        public IEnumerable<string> RolesOfUser { get; set;}
    }
}
