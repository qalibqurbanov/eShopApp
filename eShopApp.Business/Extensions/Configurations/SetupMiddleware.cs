using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace eShopApp.Business.Extensions.Configurations
{
    /// <summary>
    /// Middleware-lari saxlayan sinifdir.
    /// </summary>
    public static class SetupMiddleware
    {
        /// <summary>
        /// Bu metod cagirilanda Middleware-lari aktivlewdirir.
        /// </summary>
        public static void AddDefaultConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // "/search/"   : Sorgu sirf bu wekilde atilsa '_noproduct' render olunacaq, cunki axtarilacaq olan mehsul adi verilmeyib.
                // "/search?q=" : '_search' partialinda form submit olunanda bu 'q' yazilacaq Query Stringe ve Actionda bu 'q'-ni yaxalayaraq DB-da axtaracayiq mehsulu.
                endpoints.MapControllerRoute
                (
                    name:"search",
                    pattern: "search",
                    defaults: new { controller = "Shop", action = "search" }
                );

                // "/products/"
                // "/products/id?"
                endpoints.MapControllerRoute
                (
                    name: "getProductsByCetegoryId",
                    pattern: "products/{id?}", /* 'id' : Route-un optional olan bu hissesi produktlarini elde edeceyimiz kateqoriyanin ID-sini temsil edir */
                    defaults: new { controller = "Shop", action = "List" }
                );

                // "/{controller}/
                // "/{controller}/{action}/
                // "/{controller}/{action}/{id}/
                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapRazorPages();
            });
        }
    }
}
