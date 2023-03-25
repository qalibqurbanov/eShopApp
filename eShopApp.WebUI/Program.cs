using eShopApp.Business.Extensions.Configurations;
using eShopApp.DataAccess;

namespace eShopApp.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDefaultServices(builder.Configuration);
            builder.Services.AddCustomServices();

            var app = builder.Build();

            app.AddDefaultConfiguration(builder.Environment);

            app.Run();
        }
    }
}