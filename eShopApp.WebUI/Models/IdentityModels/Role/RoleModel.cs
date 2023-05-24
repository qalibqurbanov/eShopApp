using System.ComponentModel.DataAnnotations;

namespace eShopApp.WebUI.Models.IdentityModels.Role
{
    /// <summary>
    /// Rolu temsil edir.
    /// </summary>
    public class RoleModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string Name { get; set; }
    }
}