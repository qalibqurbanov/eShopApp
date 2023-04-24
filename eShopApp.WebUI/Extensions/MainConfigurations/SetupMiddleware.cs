using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace eShopApp.WebUI.Extensions.MainConfigurations
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

            // app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // "~/admin/products/" : 'default' route-dan once yazilmalidir ki, "~/admin/products/" sxeminin default route-a uygun gelmesi sebebile default route ile ilk qarwilawaraq default route iwlemesin.
                endpoints.MapControllerRoute
                (
                    name: "adminCategoryList",
                    pattern: "admin/categories",
                    defaults: new { controller = "Admin", action = "CategoryList" }
                );

                // "~/admin/category/"
                // "~/admin/category/id?"
                endpoints.MapControllerRoute
                (
                    name: "adminCategoryEdit",
                    pattern: "admin/category/{id?}",
                    defaults: new { controller = "Admin", action = "EditCategory" }
                );

                // "~/admin/products/" : 'default' route-dan once yazilmalidir ki, "~/admin/products/" sxeminin default route-a uygun gelmesi sebebile default route ile ilk qarwilawaraq default route iwlemesin.
                endpoints.MapControllerRoute
                (
                    name: "adminProductList",
                    pattern: "admin/products",
                    defaults: new { controller = "Admin", action = "ProductList" }
                );

                // "~/admin/products/"
                // "~/admin/products/id?"
                endpoints.MapControllerRoute
                (
                    name: "adminProductEdit",
                    pattern: "admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "EditProduct" }
                );

                // "~/search/"   : Sorgu sirf bu wekilde atilsa '_noproduct' render olunacaq, cunki axtarilacaq olan mehsul adi verilmeyib Query Stringe("?q=..."),
                // verilmediyi ucun de actiona bow string gedir ve bow addada nese bir mehsul tapilmadigina gore '_noproduct' render olunur.
                // "~/search?q=" : '_search' partialinda form submit olunanda bu 'q' yazilacaq Query Stringe ve Actionda bu 'q'-ni yaxalayaraq DB-da axtaracayiq mehsulu.
                endpoints.MapControllerRoute
                (
                    name: "search",
                    pattern: "search",
                    defaults: new { controller = "Shop", action = "search" }
                );

                // "~/products/"
                // "~/products/id?"
                endpoints.MapControllerRoute
                (
                    name: "getProductsByCetegoryId",
                    pattern: "products/{id?}", /* 'id' : Route-un optional olan bu hissesi produktlarini elde edeceyimiz kateqoriyanin ID-sini temsil edir */
                    defaults: new { controller = "Shop", action = "List" }
                );

                // "~/home/index/"
                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );

                endpoints.MapRazorPages();
            });
        }
    }
}
