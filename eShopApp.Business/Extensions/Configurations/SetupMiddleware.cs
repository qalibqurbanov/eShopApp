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
                    pattern: "{controller=shop}/{action=list}/{id?}"
                );

                endpoints.MapRazorPages();
            });
        }
    }
}
