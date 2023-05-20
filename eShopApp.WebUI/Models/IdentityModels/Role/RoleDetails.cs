using eShopApp.WebUI.Identity.Entities;

namespace eShopApp.WebUI.Models.IdentityModels.Role
{
    /// <summary>
    /// Rol haqqinda elave melumatlari(hansi rolda hansi userler var ve s.) temsil edir.
    /// </summary>
    public class RoleDetails
    {
        /// <summary>
        /// Rolu temsil edir.
        /// </summary>
        public AppRole Role { get; set; }

        /// <summary>
        /// Hazirki rolda hansi userlerin oldugunu temsil edir.
        /// </summary>
        public IEnumerable<AppUser> Members { get; set; }

        // <summary>
        /// Hazirki rolda hansi userlerin olmadigini temsil edir.
        /// </summary>
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}