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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                #region Cart Controller
                // "~/orders/"
                endpoints.MapControllerRoute
                (
                    name: "orderList",
                    pattern: "orders",
                    defaults: new { controller = "Cart", action = "OrderList" }
                );



                // "~/checkout/"
                endpoints.MapControllerRoute
                (
                    name: "checkout",
                    pattern: "checkout",
                    defaults: new { controller = "Cart", action = "Checkout" }
                );



                // "~/cart/"
                endpoints.MapControllerRoute
                (
                    name: "cart",
                    pattern: "cart",
                    defaults: new { controller = "Cart", action = "Index" }
                );
                #endregion Cart Controller



                #region Admin Controller
                // "~/admin/users/" : 'default' route-dan once yazilmalidir ki, "~/admin/users/" sxeminin default route-a uygun gelmesi sebebile default route ile ilk qarwilawaraq default route iwlemesin.
                endpoints.MapControllerRoute
                (
                    name: "adminUserList",
                    pattern: "admin/users",
                    defaults: new { controller = "Admin", action = "UserList" }
                );



                // "~/admin/users/create"
                endpoints.MapControllerRoute
                (
                    name: "adminUserCreate",
                    pattern: "admin/users/create",
                    defaults: new { controller = "Admin", action = "UserCreate" }
                );



                // "~/admin/user/id"
                endpoints.MapControllerRoute
                (
                    name: "adminUserEdit",
                    pattern: "admin/user/{id?}",
                    defaults: new { controller = "Admin", action = "UserEdit" }
                );



                // "~/admin/roles/" : 'default' route-dan once yazilmalidir ki, "~/admin/roles/" sxeminin default route-a uygun gelmesi sebebile default route ile ilk qarwilawaraq default route iwlemesin.
                endpoints.MapControllerRoute
                (
                    name: "adminRoleList",
                    pattern: "admin/roles",
                    defaults: new { controller = "Admin", action = "RoleList" }
                );



                // "~/admin/roles/create"
                endpoints.MapControllerRoute
                (
                    name: "adminRoleCreate",
                    pattern: "admin/roles/create",
                    defaults: new { controller = "Admin", action = "RoleCreate" }
                );



                // "~/admin/role/id"
                endpoints.MapControllerRoute
                (
                    name: "adminRoleEdit",
                    pattern: "admin/roles/{id?}",
                    defaults: new { controller = "Admin", action = "RoleEdit" }
                );



                // "~/admin/products/" : 'default' route-dan once yazilmalidir ki, "~/admin/products/" sxeminin default route-a uygun gelmesi sebebile default route ile ilk qarwilawaraq default route iwlemesin.
                endpoints.MapControllerRoute
                (
                    name: "adminProductList",
                    pattern: "admin/products",
                    defaults: new { controller = "Admin", action = "ProductList" }
                );



                // "~/admin/products/create/"
                endpoints.MapControllerRoute
                (
                    name: "adminProductCreate",
                    pattern: "admin/products/create",
                    defaults: new { controller = "Admin", action = "CreateProduct" }
                );



                // "~/admin/products/"
                // "~/admin/products/id?"
                endpoints.MapControllerRoute
                (
                    name: "adminProductEdit",
                    pattern: "admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "EditProduct" }
                );



                // "~/admin/categories/" : 'default' route-dan once yazilmalidir ki, "~/admin/categories/" sxeminin default route-a uygun gelmesi sebebile default route ile ilk qarwilawaraq default route iwlemesin.
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
                #endregion Admin Controller



                #region Shop Controller
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
                    pattern: "products/{id?}", /* 'id' : Route-un optional olan bu hissesi produktlarini elde edeceyimiz kateqoriyanin ID-sini temsil edir, route-dan ID yaxalanmasa butun mehsullar siralanacaq. 'Categories' ViewComponentinde sol menyu elementlerini render etdirdik ve dedik ki hemin bu soldaki kateqoriyalardan 'All Categories'-e kliklense '~/products/' Route-u iwlesin, diger hansi menyu elementi kliklense hemin kateqoriyanin ID-sini yaz Route-a ve belece route olacaq "/products/id" ve yene eyni qaydada bu route iwleyecek.  */
                    defaults: new { controller = "Shop", action = "List" }
                );
                #endregion Shop Controller



                #region Default Routes
                // "~/home/index/"
                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );

                endpoints.MapRazorPages();
                #endregion Default Routes
            });
        }
    }
}