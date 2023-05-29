using Microsoft.AspNetCore.Identity;
using eShopApp.WebUI.Identity.Entities;

namespace eShopApp.WebUI.Identity
{
    /// <summary>
    /// Proyekt sifirdan ayaga qaldirilanda DB bow olacaq, lakin biz gerek en azi 1 admin icazeli user DB-miza elave edek ki menecment sehifelerine giriw icazesi olan azi 1 user olmuw olsun. Bu sinif ozunde app ayaga qaldirilan vaxti yaradilmasi lazim olan User ve User Rollarini saxlayir.
    /// </summary>
    public static class SeedIdentityDatabase
    {
        /// <summary>
        /// App ayaga qaldirilan vaxti DB-da yaradilmasi lazim olan User ve User Rollarini yaradir.
        /// </summary>
        /// <param name="_configuration">Yaradilacaq userlerin melumatlari external/xarici bir konfiqurasiya faylindadirsa, hemin melumatlar hansi <see href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-7.0">IConfiguration</see> orneyi komeyile oxunsun?</param>
        public static async Task SeedIdentityDatasToDB(this IHost host, IConfiguration _configuration = null)
        {
            /* '_configuration' uygun obyektin referansini saxlayirsa, demeli melumatlar external/xarici bir konfiqurasiya faylindan oxunacaq: */
            if(_configuration != null)
            {
                string firstName      = _configuration["PredefinedUsers:Admin1_Details:FirstName"];
                string lastName       = _configuration["PredefinedUsers:Admin1_Details:LastName"];
                string userName       = _configuration["PredefinedUsers:Admin1_Details:UserName"];
                string email          = _configuration["PredefinedUsers:Admin1_Details:Email"];
                bool   emailConfirmed = _configuration.GetValue<bool>("PredefinedUsers:Admin1_Details:EmailConfirmed");
                string password       = _configuration["PredefinedUsers:Admin1_Details:FirstName"];
                string roleName       = _configuration["PredefinedUsers:Admin1_Details:FirstName"];

                /* 'Scoped' olan servisin scope/Request daxilinde cemi 1 orneyinin teleb olunmasini qarantiyaya almaq ucun yeni bir scope yaradiriq: */
                using(IServiceScope scope = host.Services.CreateScope())
                {
                    /* IoC Container-a 'AddIdentity()' terefinden 'Scoped' olaraq yerlewdirilmiw menecer sinifleri instancelarini IoC Container-dan teleb edirem: */
                    using(UserManager<AppUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>())
                    using(RoleManager<AppRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>())
                    {
                        /* Ilk once yaradacagim userin hazirda DB-da movcud olub-olmadigini yoxlayiram: */
                        if(await _userManager.FindByNameAsync(userName) == null)
                        {
                            /* Yaratmaq istediyim rol hazirda DB-da movcud deyilse: */
                            if(!await _roleManager.RoleExistsAsync(roleName))
                            {
                                /* Ilk once rol yaradiram: */
                                await _roleManager.CreateAsync(new AppRole() { Name = roleName });
                            }

                            /* Yaradacagim useri hazirlayiram: */
                            AppUser user = new AppUser()
                            {
                                FirstName      = firstName,
                                LastName       = lastName,
                                UserName       = userName,
                                Email          = email,
                                EmailConfirmed = emailConfirmed, /* Burada bir bawa 'true'-da vermek olardi ki, predefined yaratdigim hemin bu userler tesdiqlenmiw bir wekilde yaradilsin */
                            };

                            /* Yeni bir user yaradiram: */
                            IdentityResult result = await _userManager.CreateAsync(user, password);

                            /* Eger user ugurla yaradilsa: */
                            if(result.Succeeded)
                            {
                                if(await _roleManager.RoleExistsAsync(roleName))
                                {
                                    /* Useri elave edirem 'Admin' roluna: */
                                    await _userManager.AddToRoleAsync(user, roleName);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                /* Lazim olsa burada Hard Code ederek user melumatlarini qeyd edib ve dalinca eyni wekilde useri ve rolu yarada bilerem */
            }
        }
    }
}