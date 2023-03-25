using eShopApp.Business.Services.Abstract;
using eShopApp.Business.Services.Concrete;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopApp.Business.Extensions.Configurations
{
    public static class RegisterServices
    {
        public static void AddDefaultServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ShopDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ShopSqlConnectionString1")));

            //serviceCollection.AddDefaultIdentity<IdentityUser>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //}).AddEntityFrameworkStores<ApplicationDbContext>();

            serviceCollection.AddControllersWithViews()
                             .AddRazorRuntimeCompilation();

            serviceCollection.AddRazorPages();
        }

        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();

            serviceCollection.AddScoped<IProductService, ProductManager>();

        }


    }
}
