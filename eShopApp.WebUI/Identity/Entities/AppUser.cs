using Microsoft.AspNetCore.Identity;

namespace eShopApp.WebUI.Identity.Entities
{
    /// <summary>
    /// Application-mizdaki her hansi bir user-i temsil edir.
    /// </summary>
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}