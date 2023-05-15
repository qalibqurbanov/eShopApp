using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eShopApp.WebUI.Identity.Entities;
using eShopApp.Business.Services.Abstract;
using eShopApp.Business.Services.Concrete;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete;
using eShopApp.WebUI.Identity.DatabaseContext;
using eShopApp.WebUI.Identity.Services.Abstract;
using eShopApp.WebUI.Identity.Services.Concrete;

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
            serviceCollection.AddDbContext<ShopDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("ShopSqlConnectionString1")); });
            serviceCollection.AddDbContext<ShopIdentityContext>(options => { options.UseSqlServer(configuration.GetConnectionString("ShopSqlConnectionString2_Identity")); });

            serviceCollection.AddIdentity<AppUser, AppRole>(options =>
            {
                // options.Password.RequiredLength = 12; /* Kodun uzunlugu minimum 12 simvoldan ibaret olsun */
                options.Password.RequireNonAlphanumeric = false; /* True: Alfanumerik(A-Z, 0-9) elementlerden elave simvollarda daxil edilmelidir koda. False: simvollar elave edilmesede olar. */
                options.Password.RequireLowercase = false; /* Kicik herf daxil edilme mecburiyyetini deaktiv edirik */
                options.Password.RequireUppercase = false; /* Boyuk herf daxil edilme mecburiyyetini deaktiv edirik */
                options.Password.RequireDigit = true; /* True: deyerek 0-9 arasi mutleq reqem daxil olunmali oldugunu deyirik */

                options.User.RequireUniqueEmail = true; /* Daxil edilen her email unikal olmalidir, yeni, artiq sistemde olan maili yaza bilmeyeceyik */
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_"; /* Username hansi simvollardan ibaret ola biler */

                options.Lockout.AllowedForNewUsers = false; /* Userin profili Lock olsun? Etrafli: "https://stackoverflow.com/a/51963403/13249741" */
                options.Lockout.MaxFailedAccessAttempts = 5; /* Nece ugursuz login olma cehdinden sonra userin profili lock edilsin? (cehd sayi gosterdiyim 5-e catsa 'shouldLockout' true-dursa userin profili lock edilecek. Daha sonra 'AccessFailedCount' sutunu 0-lanacaq) */
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20); /* Userin profili ne qeder muddetlik lock edilsin? */

                options.SignIn.RequireConfirmedEmail = false; /* User sign-in ola bilmek ucun mutleq mailini tesdiqlemiw olmalidir */
                options.SignIn.RequireConfirmedPhoneNumber = false; /* User sign-in ola bilmek ucun telefonu tesdiqlemeyi mecbur deyil */
            })
                .AddEntityFrameworkStores<ShopIdentityContext>()
                .AddDefaultTokenProviders(); /* Default gelen 3 Token Providerin 3-nude elave edir Identity sistemimize. Bu Tokenler account operasiyalari (unudulmuw passwordun resetlenmesi veya email deyiwmek) ve two-factor autentifikasiya zamani iwledilir. Yeni, wifre yenileme meqsedile hedef maile atdigimiz password resetleme linkine elave edilmiw olacaq generasiya edilmiw bu Token.
                            + "DataProtectorTokenProvider": Email tesdiqi/deyiwdirilmesi veya unudulmuw Passwordun yenilenmesi ve s. ucun Token generasiya edir: 'DataProtector' :sinifinin ve mueyyen kriptoqrafiya alqoritmlerinin komeyile. Hemin bu xususi alqoritmle wifrelenmiw Token, userin mailine gelen "Token elave edilmiw password resetleme linki"-ne basib servere "password resetleme" ile bagli sorgu gonderdiyi zaman server terefde dewifre edilir bu Token.
	                        + "EmailTokenProvider" ve "PhoneNumberTokenProvider": Her iki provider "TotpSecurityStampBasedTokenProvider" sinifinden miras alib ve 'Time-based One-time Password Algorithm (TOTP)' protokoluyla/prinsipiyle iwleyirler, bu protokol user-friendly ve eyni zamanda qisa Tokenler generasiya etmek ucundur hansiki daha sonra SMS olaraq veya email olaraq gondermek olar.
                    > Yaxwi bes 'Token' nedir? : Appin usere verdiyi, bawqa sozle, user ucun generasiya etdiyi xususi alqoritmle wifrelenmiw simvol toplulugudur, daha sonra bu Tokeni userden geri alir ve belece Identity sistemi tesdiqlemiw olurki, user heqiqetende hedefimizde olan userdir ve etmek istediyi emeliyyata(mail tesdiqleme, unudulmuw kodu berpa etmek ve s.) icaze verilsin. Token ferqli yerlerde iwlede bilerik, meselen: Tokeni, hedef user haqqinda nese bir weyi tesdiqlemek istediyimiz zaman iwledirik: "Telefon heqiqetende hazirki usere aiddirmi?", "Mail heqiqetende usere aiddirmi?" :dediyim kimi Tokeni ferqli yerlerde iwlede bilerik, "slack.com" saytinda meselen nece istifade edilib: User profiline telefonda giriw ede bilmesi ucun "magic sign-in link" generasiya etme meqsedile iwledilib Token. Bu neticeye gelirik ki: Tokenin guvenli olmagi ucun wifrelenmiw halda gizli qalmalidir, cunki Tokenler ele kecirildiyi an tehlukesizlik problemleri yarana bilir. Bu sebeble, generasiya edilmiw Tokene kecerlilik muddeti, bawqa sozle, omur qoymaq veya birbawa tek istifadelik etmek lazimdir + ki, Token ferqli adamlarin/userlerin eline kecse nese ede bilmesin. ASP Core-da built-in gelen Identity API, bize yuxarida vurguladigim 3 'Token Provider'-leri teqdim edir, hansiki o providerlerin komeyile Token yarada bilerik. */

            /* 'ConfigureApplicationCookie()' metodunun komeyile, yaradilacaq olan Cookie-mizin('Cookie based authentication' ucun) bezi ozelliklerini deyiwek: */
            serviceCollection.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/account/signin");
                options.LogoutPath = new PathString("/account/signout");
                options.AccessDeniedPath = new PathString("/account/accessdenied");

                options.ExpireTimeSpan = TimeSpan.FromMinutes(2); /* Identity API ucun yaranacaq olan bu 'Cookie'-nin omrunun ne qeder olacagini bildirir */
                options.SlidingExpiration = true; /* Cookie-nin omrunu uzatmaq isteyirikse iwledirik. Hazirki Cookie-nin omrune qalmiw vaxtin('ExpireTimeSpan') yarisindan coxu kecende, eger user her hansi bir request(seyfenin yenilenmesi ve s.) gonderse servere bu zaman 'ExpireTimeSpan' vaxti sifirlanacaq, belece emin olunsun ki User aktivdir ve belece user profiline login olmuw veziyyetde qalsin. */

                /* Cookie esasli Autentifikasiya ucun yaradilacaq olan Cookie-ni konfiqurasiya edirik: */
                options.Cookie = new CookieBuilder
                {
                    /* Yaranacaq Cookie-nin adi: */
                    Name = ".eShopApp_idCookie",

                    /* Client-side diller(JS ve s.) vasitesile Cookie-ye mudaxilenin qarwisini aliriq. JS-in "allCookies = document.cookie;" kodu ile Cookieleri elde etmek ve s. olur brauzerden */
                    HttpOnly = false,

                    /* Diger saytlar terefinden bizim saytimizin Cookielerinin iwledile bilib-bilinmeyeceyini konfiqurasiya edir, bawqa sozle - brauzer terefinden, Cookie Requeste elave edilsin(attach) ya edilmesin yoxlamaq meqsedile iwledilir. Aldigi deyerler:
                            - Lax : Bu moddada eyni wekilde ya user terefinden bir-bawa 'sayt.com' deyilib girilsin(Request gonderilsin) gerek, yada sayt daxilinde seyfeler arasinda kecid edilsin, yada ki, bawqa bir domainde saytimizin linkine basilaraq saytimiza kecid edilsin. Kenar saytlar saytimizi iFrame icerisinde gosterdiyi anlarda Cookie Requeste attach olunmayacaq. */
                    SameSite = SameSiteMode.Lax,

                    /* Cookie 'HTTPS' uzerinden elcatilan olacaq, yeni, yalniz 'HTTPS' requestlercun elcatilan olacaq Cookie-miz: */
                    SecurePolicy = CookieSecurePolicy.Always
                };
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
        public static void AddCustomServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();

            serviceCollection.AddScoped<IProductService, ProductManager>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();

            serviceCollection.AddScoped<IEmailSender, HotmailEmailSender>(serviceProvider => new HotmailEmailSender
            (
                /* IoC Containerden 'IEmailSender' teleb olunsa geriye icerisi default olaraq awagidaki datalarla dolu olan 'HotmailEmailSender' orneyi dondururuk: */
                HostAddress: configuration["HotmailSender:Host"],
                Port:        configuration.GetValue<ushort>("HotmailSender:Port"),
                EnableSSL:   configuration.GetValue<bool>("HotmailSender:EnableSSL"),
                DevEmail:    configuration["HotmailSender:Email"],
                DevPassword: configuration["HotmailSender:Password"]
            ));
        }
    }
}