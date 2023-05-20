using System.ComponentModel.DataAnnotations;

namespace eShopApp.WebUI.Models.IdentityModels.Login
{
    /// <summary>
    /// Passwordu berpa etmek meqsedile userin formu doldurub post etdiyi datalari temsil edir.
    /// </summary>
    public class ResetModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}