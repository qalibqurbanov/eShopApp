using Microsoft.EntityFrameworkCore;
using eShopApp.WebUI.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eShopApp.WebUI.Identity.DatabaseContext
{
    public class ShopIdentityContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ShopIdentityContext(DbContextOptions<ShopIdentityContext> options) : base(options)
        {
            
        }
    }
}