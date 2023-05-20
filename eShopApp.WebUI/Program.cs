using eShopApp.DataAccess;
using eShopApp.WebUI.Identity;
using eShopApp.WebUI.Extensions.MainConfigurations;

namespace eShopApp.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDefaultServices(builder.Configuration);
            builder.Services.AddCustomServices(builder.Configuration);

            WebApplication app = builder.Build();

            app.AddDefaultConfiguration(builder.Environment);

            if(builder.Environment.IsDevelopment())
                app.SeedDummyDatasToDB();
            else
                app.SeedIdentityDatasToDB(builder.Configuration).Wait();

            app.Run();
        }
    }
}