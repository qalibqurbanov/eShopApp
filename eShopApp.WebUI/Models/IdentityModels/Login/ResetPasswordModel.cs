using System.ComponentModel.DataAnnotations;

namespace eShopApp.WebUI.Models.IdentityModels.Login
{
    /// <summary>
    /// Userin formda yazaraq POST etdiyi yeni paswordu temsil edir.
    /// </summary>
    public class ResetPasswordModel
    {
        [Required]
        [StringLength(30, MinimumLength = 12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 12)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))] /* 'RePassword'-a gelmiw data 'Password' ile eyni olsun/olmalidir */
        public string RePassword { get; set; }
    }
}