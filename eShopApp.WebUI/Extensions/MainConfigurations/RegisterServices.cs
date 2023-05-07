using System.Reflection;
using eShopApp.Business.Services.Abstract;
using eShopApp.Business.Services.Concrete;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopApp.WebUI.Extensions.MainConfigurations
{
    /// <summary>
    /// ASP Core-un default servislerini ve menim custom servislerimi IoC Containera yerlewdirme emeliyyatlarini saxlayan sinifdir.
    /// </summary>
    public static class RegisterServices
    {
        /* ASP Core-un built-in/default servislerini ve ya proyektde iwletdiyim kitabxanalari(EF Core ucun DbContext ve s.) IoC Containera yerlewdirir. */
        public static void AddDefaultServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ShopSqlConnectionString1"));
            });

            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly()); /* Hazirki assembly icerisinde reflection komeyile 'Profile'-den miras alan profil siniflerimi tapacaq */

            serviceCollection.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            serviceCollection.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            serviceCollection.AddRazorPages();
        }

        /* Custom servislerimi IoC Containera yerlewdirir. */
        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();

            serviceCollection.AddScoped<IProductService, ProductManager>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
        }
    }
}