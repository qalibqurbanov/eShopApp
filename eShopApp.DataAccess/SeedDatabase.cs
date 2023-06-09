﻿using eShopApp.Entity.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using eShopApp.DataAccess.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;

namespace eShopApp.DataAccess
{
    /// <summary>
    /// Bu sinif DB yaradaraq icerisini dummy/test datalar ile doldurmaq ucundur.
    /// </summary>
    public static class SeedDatabase
    {
        /// <summary>
        /// Database yaradilmayibsa yaradir ve icerisini dummy datalar ile doldurur.
        /// </summary>
        public static void SeedDummyDatasToDB(this IHost host)
        {
            /* 'Scoped' olan servisin scope/Request daxilinde cemi 1 orneyinin teleb olunmasini qarantiyaya almaq ucun yeni bir scope yaradiriq: */
            using (IServiceScope scope = host.Services.CreateScope())
            {
                /* IoC Container-dan daha once yerlewdirdiyim 'ShopDbContext' obyektini teleb edirem: */
                using (ShopDbContext _dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>())
                {
                    /* Test ucun yeni datalar(Category/Product) elave etsen ve ya movcud datalari(Category/Product) deyiwsen gerek DB-ni silib yeniden yaradasan. Datalari(Category/Product) deyiwmeyibse uncomment etme: */
                    // _dbContext.Database.EnsureDeleted();

                    /* Ilk once - Database yoxdursa yarat: */
                    // _dbContext.Database.EnsureCreated();
                    // _dbContext.Database.Migrate();

                    /* Icra olunmagi gozleyen migration yoxdursa ve ya migrationlar icra olunub qutariblarsa artiq gozleyen migration yoxdur ve sayi 0-dir demeli: */
                    if (_dbContext.Database.GetPendingMigrations().Count() == 0)
                    {
                        /* Eger 'Categories' cedveli bowdursa: */
                        if (_dbContext.Categories.Count() == 0)
                        {
                            _dbContext.Categories.AddRange(CategoryArray);
                        }

                        /* Eger 'Products' cedveli bowdursa: */
                        if (_dbContext.Products.Count() == 0)
                        {
                            _dbContext.Products.AddRange(ProductArray);

                            _dbContext.AddRange(ProductCategoryArray);
                        }

                        _dbContext.SaveChanges();
                    }
                }
            }
        }

        #region Seed edeceyim datalari saxlayan massivler.
        private readonly static Product[] ProductArray =
        {
            // > Nout : 23
            // > Saat : 10
            // > Tel  : 12
            // + Cemi : 45

            /* = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = */

            // 0 - 22
            #region Noutbuklar
            new Product()
            {
                ProductName = "Noutbok Acer Nitro 5 AN515-45-R9VT",
                ProductPrice = 6400,
                ProductImageName = "Noutbok Acer Nitro 5 AN515-45-R9VT.jpg",
                ProductDescription = "Faucibus purus in massa tempor nec feugiat nisl pretium fusce id velit ut tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus faucibus ornare suspendisse. Shortbread lemon drops candy canes ice cream cake powder cake. Icing powder liquorice chocolate danish candy canes halvah. Topping chocolate pastry fruitcake gummi bears chocolate bar bonbon candy canes. Caramels cookie cheesecake cupcake sesame snaps jelly cake lollipop. Faucibus purus in massa tempor nec feugiat nisl pretium fusce id velit ut tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus faucibus ornare suspendisse. Shortbread lemon drops candy canes ice cream cake powder cake. Icing powder liquorice chocolate danish candy canes halvah. Topping chocolate pastry fruitcake gummi bears chocolate bar bonbon candy canes. Pudding lollipop carrot cake cheesecake toffee dessert bonbon pastry. Gummi bears shortbread toffee gummi bears dessert. Tootsie roll oat cake icing ice cream cake carrot cake. Caramels jelly-o gingerbread jelly gummies sugar plum. Shortbread cotton candy dessert toffee gummi bears. Jujubes lemon drops sweet roll cupcake soufflé candy jelly beans. Sweet apple pie fruitcake jujubes chocolate icing. Fruitcake soufflé jelly tiramisu chocolate bar liquorice cheesecake. Icing cake carrot cake chupa chups lollipop danish brownie. Tiramisu sweet roll tootsie roll soufflé danish bonbon pie. Pie carrot cake cookie brownie caramels fruitcake bear claw cupcake icing. Marzipan marshmallow marzipan soufflé tootsie roll powder liquorice bear claw chupa chups. Pie cotton candy pastry tiramisu gummies croissant. Caramels cookie cheesecake cupcake sesame snaps jelly cake lollipop. Macaroon tart pudding cake tiramisu. Muffin macaroon croissant carrot cake oat cake dessert muffin sweet roll. Sugar plum icing tiramisu pudding dessert brownie bear claw marzipan. Pastry biscuit bear claw jujubes chocolate cake cupcake. Shortbread candy canes bear claw bear claw candy fruitcake bonbon shortbread. Marshmallow danish tart liquorice sesame snaps. Pie chupa chups shortbread biscuit jelly-o. Tiramisu biscuit macaroon cotton candy caramels. Biscuit chupa chups shortbread biscuit jelly-o. Tiramisu biscuit macaroon cotton candy caramels.",
                ProductIsApproved = true,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Acer Aspire 5 Gold",
                ProductPrice = 2500,
                ProductImageName = "Noutbuk Acer Aspire 5 Gold.jpg",
                ProductDescription = "Gummi bears pastry cheesecake ice cream shortbread sweet roll. Muffin gummi bears tart pudding carrot cake sugar plum. Jelly dessert caramels caramels dessert gingerbread topping danish. Toffee wafer jelly beans halvah toffee dessert powder gingerbread croissant. Croissant tiramisu jelly-o bear claw soufflé lollipop brownie tootsie roll. Toffee chocolate cake cookie sweet bear claw. Toffee shortbread dragée tiramisu chocolate bar muffin pudding. Pudding lollipop carrot cake cheesecake toffee dessert bonbon pastry. Gummi bears shortbread toffee gummi bears dessert. Tootsie roll oat cake icing ice cream cake carrot cake. Caramels jelly-o gingerbread jelly gummies sugar plum. Shortbread cotton candy dessert toffee gummi bears. Jujubes lemon drops sweet roll cupcake soufflé candy jelly beans. Sweet apple pie fruitcake jujubes chocolate icing. Fruitcake soufflé jelly tiramisu chocolate bar liquorice cheesecake. Icing cake carrot cake chupa chups lollipop danish brownie. Tiramisu sweet roll tootsie roll soufflé danish bonbon pie. Pie carrot cake cookie brownie caramels fruitcake bear claw cupcake icing. Marzipan marshmallow marzipan soufflé tootsie roll powder liquorice bear claw chupa chups. Pie cotton candy pastry tiramisu gummies croissant. Sweet roll bear claw croissant ice cream cookie. Jelly beans ice cream donut tiramisu toffee. Danish candy canes gummi bears dragée marzipan gingerbread. Gingerbread chupa chups sugar plum bonbon chupa chups danish chocolate bar bonbon. Sweet croissant chupa chups gummi bears ice cream apple pie faucibus purus in massa tempor nec feugiat nisl pretium fusce id velit ut tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus faucibus ornare suspendisse sed nisi lacus sed viverra tellus in hac habitasse platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus pellentesque.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Acer Extensa EX215-31-C6FV",
                ProductPrice = 3200,
                ProductImageName = "Noutbuk Acer Extensa EX215-31-C6FV.jpg",
                ProductDescription = "Toffee jujubes carrot cake marshmallow tart tootsie roll. Liquorice cupcake pudding jelly beans dragée dessert. Candy danish bear claw lollipop candy canes jelly beans. Marshmallow jelly beans chocolate bar topping ice cream cake marzipan shortbread. Donut sweet roll pie cupcake croissant sugar plum. Jelly beans jelly-o cake apple pie dragée. Candy canes chocolate sugar plum sugar plum chupa chups. Toffee caramels bear claw donut halvah dragée cupcake. Chocolate cake sweet bonbon fruitcake brownie lollipop wafer halvah. Macaroon pudding sweet roll chupa chups tart croissant candy canes jelly-o dessert. Donut carrot cake muffin donut chocolate cake chupa chups marshmallow. Pastry pastry tart biscuit cupcake danish wafer. Pastry cake marzipan candy sugar plum macaroon shortbread. Biscuit jelly-o soufflé pie pastry brownie. Cotton candy pie ice cream candy candy canes liquorice. Dessert sweet lollipop tiramisu dessert sesame snaps cheesecake jujubes jelly-o. Pastry lemon drops muffin dessert cheesecake. Donut ice cream gingerbread lemon drops toffee jelly halvah. Biscuit powder pastry chocolate cake chupa chups cotton candy powder. Sugar plum candy canes shortbread danish pastry candy canes fruitcake. Brownie macaroon marshmallow biscuit cupcake cake sweet roll sweet halvah. Jujubes oat cake croissant candy canes donut icing cookie tootsie roll. Oat cake muffin donut marshmallow topping. Lemon drops gingerbread muffin candy canes gingerbread apple pie pie biscuit gummies. Apple pie chocolate sugar plum jelly beans cake bear claw. Muffin muffin cake soufflé fruitcake jelly beans shortbread. Sweet cupcake cotton candy marshmallow cake brownie.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk Asus X409FA-EK684",
                ProductPrice = 2200,
                ProductImageName = "Noutbuk Asus X409FA-EK684.jpg",
                ProductDescription = "Lollipop soufflé bear claw sugar plum chupa chups shortbread muffin chupa chups chupa chups. Sweet halvah cookie bonbon soufflé cake. Tootsie roll dragée gummies powder lemon drops cotton candy chocolate. Pudding pastry jelly-o cotton candy apple pie fruitcake jelly-o sesame snaps. Dessert lemon drops caramels jujubes candy pie liquorice carrot cake cake. Pie cotton candy chocolate tootsie roll brownie soufflé. Biscuit fruitcake candy canes tiramisu cotton candy soufflé. Macaroon brownie gummi bears bonbon chocolate bar fruitcake chocolate cake donut. Brownie jelly beans tootsie roll chocolate cake sweet powder cotton candy tiramisu. Ice cream cake lollipop apple pie bonbon tootsie roll dessert chocolate marshmallow. Marzipan tootsie roll marshmallow muffin chocolate cake candy canes. Cake pudding apple pie bear claw carrot cake jujubes powder. Tootsie roll macaroon soufflé dessert pastry halvah topping tiramisu. Candy canes chupa chups croissant liquorice sweet roll. Jelly beans jujubes brownie brownie macaroon donut topping. Chupa chups chocolate sesame snaps marshmallow pudding fruitcake. Marzipan topping ice cream powder wafer pudding. Dragée donut jelly topping marshmallow topping. Donut brownie chocolate dragée macaroon candy canes. Pudding brownie marshmallow tootsie roll tiramisu chocolate macaroon sugar plum. Jujubes pastry croissant sweet roll pastry jelly sugar plum donut. Powder tart chocolate bar jelly beans tart caramels. Jelly-o cheesecake gummi bears muffin wafer. Powder cupcake gummi bears marzipan liquorice apple pie lemon drops jelly-o tiramisu. Cheesecake marzipan ice cream candy pudding gummies danish. Tart wafer bonbon liquorice jelly icing dragée sweet cake. Marzipan halvah icing sweet cheesecake. Sweet roll chupa chups donut chocolate cake pie.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk Asus X515EA-BQ1189",
                ProductPrice = 9450,
                ProductImageName = "Noutbuk Asus X515EA-BQ1189.jpg",
                ProductDescription = "Oat cake sweet roll dessert brownie brownie shortbread candy wafer. Ice cream pastry brownie gummies macaroon croissant. Bonbon carrot cake dessert lollipop sweet dragée. Sweet cake tiramisu gummies sugar plum sweet powder oat cake cake. Candy jelly jelly-o sweet bonbon. Sesame snaps sesame snaps tart chocolate cake cake gummi bears biscuit bonbon. Bear claw topping lemon drops apple pie lemon drops. Cupcake wafer bonbon candy sweet roll jujubes danish. Bear claw tiramisu cotton candy cake jelly. Chupa chups icing sesame snaps jelly marzipan apple pie. Croissant tootsie roll gummi bears candy cookie apple pie macaroon. Oat cake sweet roll topping topping chocolate. Biscuit brownie jelly beans croissant chocolate bar. Jujubes sugar plum icing apple pie sweet roll pastry cookie ice cream. Cookie icing cake wafer cookie. Fruitcake chocolate jujubes gingerbread chocolate jujubes chocolate bar. Jelly-o carrot cake jelly-o danish jelly jelly chupa chups tart. Biscuit gummi bears shortbread bear claw caramels gummies marzipan apple pie apple pie. Powder liquorice candy canes jujubes toffee lemon drops bonbon. Dessert apple pie liquorice soufflé dragée. Marzipan gummies wafer bear claw lemon drops chocolate macaroon candy canes cotton candy. Muffin bear claw donut caramels dragée pudding. Dragée liquorice tootsie roll dessert topping cake bear claw marshmallow. Macaroon bonbon chocolate bar cake jelly beans jujubes.",
                ProductIsApproved = true,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Asus TUF Dash F15 FX517",
                ProductPrice = 1530,
                ProductImageName = "Noutbuk Asus TUF Dash F15 FX517.jpg",
                ProductDescription = "Soufflé carrot cake wafer shortbread caramels jelly-o danish chocolate candy. Tiramisu marzipan biscuit jelly-o donut chupa chups halvah. Dessert cheesecake biscuit sesame snaps marshmallow. Tart powder jujubes gummies sesame snaps sugar plum liquorice halvah. Wafer cake cheesecake biscuit apple pie. Bear claw dragée sugar plum sugar plum powder cake bear claw. Soufflé cake chocolate cake lollipop soufflé shortbread oat cake. Gummi bears jelly-o sugar plum jelly-o chocolate cake cotton candy halvah bear claw. Donut candy apple pie oat cake muffin marshmallow soufflé. Sugar plum cotton candy halvah jujubes wafer dessert biscuit pastry. Oat cake gummi bears cheesecake jelly brownie donut gummies. Ice cream sesame snaps caramels cookie dragée jelly beans jelly beans oat cake. Gingerbread apple pie wafer muffin fruitcake. Caramels donut jelly ice cream lemon drops caramels jelly beans macaroon marshmallow. Jelly beans soufflé shortbread sesame snaps icing toffee. Marshmallow tootsie roll caramels donut gingerbread sugar plum jelly donut gummies. Lemon drops chupa chups brownie muffin tart. Pudding cotton candy carrot cake biscuit cake ice cream bonbon fruitcake sweet roll. Topping marshmallow danish wafer liquorice croissant cake tiramisu. Lemon drops toffee gummies wafer tootsie roll. Cotton candy carrot cake apple pie dragée halvah jelly chocolate bar sesame snaps. Bonbon marzipan marshmallow liquorice jelly brownie. Soufflé bear claw marshmallow candy lollipop gummies gummi bears. Candy canes candy cupcake sweet roll ice cream cookie cookie tart sweet.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Asus TUF Gaming F15 FX507ZE-HN003",
                ProductPrice = 1750,
                ProductImageName = "Noutbuk Asus TUF Gaming F15 FX507ZE-HN003.jpg",
                ProductDescription = "Carrot cake fruitcake bonbon gummi bears powder jujubes sweet. Bonbon marzipan gummies danish liquorice chocolate bar danish. Carrot cake candy toffee tiramisu candy canes jelly beans cheesecake chocolate cake sesame snaps. Caramels muffin lollipop cupcake carrot cake jujubes jelly apple pie chocolate. Jelly-o candy canes gummi bears cake bear claw cupcake jelly gingerbread. Cotton candy ice cream brownie soufflé tootsie roll muffin jelly-o. Powder donut carrot cake powder powder bonbon sugar plum gingerbread. Sesame snaps donut gingerbread jelly-o chocolate ice cream sweet cotton candy bonbon. Powder sweet ice cream pastry tiramisu caramels toffee chocolate gummies. Chocolate cake jujubes lemon drops macaroon apple pie muffin lemon drops. Biscuit muffin cupcake pie topping marzipan. Gummies cake tootsie roll lollipop gummi bears sesame snaps. Donut lollipop jelly beans chocolate jujubes toffee jelly. Chocolate cake topping sugar plum sweet roll sweet roll donut. Dragée brownie cake wafer lollipop lollipop lemon drops halvah gummies. Marzipan cake jelly halvah powder chocolate bear claw pastry gingerbread. Dessert toffee fruitcake donut chupa chups chocolate. Topping gummies brownie macaroon wafer topping sesame snaps wafer. Shortbread apple pie icing chupa chups oat cake gummies. Bonbon jujubes danish cheesecake powder dessert halvah candy. Shortbread soufflé pie cake danish chocolate apple pie candy halvah. Chocolate cake cake sweet roll oat cake bear claw danish. Muffin donut jujubes pudding powder sweet cotton candy. Apple pie croissant liquorice tootsie roll topping croissant muffin marshmallow. Lemon drops macaroon tart croissant fruitcake pastry sweet candy. Cake soufflé marzipan cake icing danish. Cake fruitcake tart jujubes halvah jujubes. Croissant wafer bonbon marshmallow jelly beans. Cotton candy chocolate cake candy canes sweet sesame snaps chocolate cake biscuit apple pie. Toffee cake cheesecake sweet liquorice chocolate bear claw. Cake dragée fruitcake sweet donut shortbread. Jelly-o apple pie halvah icing candy shortbread gingerbread soufflé. Powder jelly-o bear claw sugar plum muffin bear claw. Cake powder danish gummi bears pie caramels cupcake liquorice caramels. Tiramisu bonbon cake shortbread dragée chupa chups gummies. Marzipan pie chocolate lollipop cake cotton candy. Jelly beans tart chocolate cheesecake sweet biscuit. Cupcake biscuit carrot cake pudding macaroon gummies soufflé pastry sweet.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk Asus VivoBook S14 OLED K3402ZA-KM346",
                ProductPrice = 7620,
                ProductImageName = "Noutbuk Asus VivoBook S14 OLED K3402ZA-KM346.jpg",
                ProductDescription = "Apple pie gummies icing macaroon gummies sweet roll sweet shortbread. Pastry lollipop carrot cake cheesecake gummi bears liquorice marshmallow. Cake chocolate chocolate bar gummies chocolate cake. Croissant topping gummies muffin bonbon. Jelly-o chocolate bar powder cotton candy cookie cupcake cotton candy. Muffin marshmallow sugar plum gingerbread topping brownie ice cream tart. Sweet roll apple pie croissant danish cake. Chocolate tiramisu sugar plum macaroon jelly jelly beans cupcake pastry marshmallow. Gingerbread croissant biscuit liquorice wafer cake cake. Pudding pastry tiramisu dessert dragée tart carrot cake pie. Jelly-o sugar plum croissant chocolate chocolate bar cotton candy. Jelly-o chocolate marzipan dragée gummies cake marshmallow dessert. Toffee sweet marzipan brownie apple pie gingerbread. Cake icing lollipop jujubes sweet roll bonbon. Sesame snaps sugar plum cotton candy chocolate bar topping. Oat cake bear claw gingerbread toffee carrot cake tart gummies chupa chups. Sweet caramels gummi bears jelly beans cupcake. Croissant macaroon danish cotton candy ice cream cake caramels. Cotton candy caramels oat cake carrot cake tootsie roll fruitcake muffin pastry sugar plum. Caramels sweet roll icing cupcake cookie shortbread cookie cake. Gingerbread macaroon marshmallow toffee jelly beans gummi bears marzipan soufflé. Dragée chocolate sweet roll chocolate cake lollipop. Sesame snaps sesame snaps icing caramels croissant jelly beans croissant fruitcake. Halvah sweet jujubes gingerbread cupcake tart cookie. Pie bonbon fruitcake cheesecake sweet apple pie. Candy wafer ice cream soufflé donut dragée dessert cheesecake. Powder marzipan shortbread gummies lollipop sesame snaps caramels donut. Cookie cupcake tootsie roll caramels macaroon. Jelly beans cake brownie pudding candy canes sesame snaps chocolate bar candy canes sweet roll. Muffin cotton candy marshmallow bonbon sweet. Apple pie danish sugar plum caramels soufflé.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk Dell G15 5525",
                ProductPrice = 7270,
                ProductImageName = "Noutbuk Dell G15 5525.jpg",
                ProductDescription = "Icing apple pie apple pie marshmallow tootsie roll. Bear claw carrot cake gummi bears candy canes tootsie roll cake. Dessert cheesecake chupa chups dessert dessert cake chocolate bar caramels cheesecake. Caramels pudding sugar plum croissant jujubes topping bonbon liquorice. Candy canes jujubes bear claw biscuit lollipop bonbon. Jelly beans sugar plum cake gummi bears jelly-o. Biscuit chupa chups toffee gingerbread dessert oat cake carrot cake. Toffee halvah ice cream brownie chupa chups wafer sesame snaps croissant candy canes. Marzipan lemon drops donut pudding cookie jelly. Dragée gummi bears pastry chupa chups danish croissant jelly-o gummies. Topping gummies caramels oat cake wafer marzipan. Cake tiramisu marzipan topping cheesecake gummies brownie lemon drops. Caramels sweet roll icing cupcake cookie shortbread cookie cake. Gingerbread macaroon marshmallow toffee jelly beans gummi bears marzipan soufflé. Dragée chocolate sweet roll chocolate cake lollipop. ",
                ProductIsApproved = true,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk HP 250 G8",
                ProductPrice = 1570,
                ProductImageName = "Noutbuk HP 250 G8.jpg",
                ProductDescription = "Caramels gummies oat cake caramels icing shortbread pudding. Carrot cake cookie cookie liquorice gingerbread chocolate cake jelly pudding. Biscuit powder sesame snaps gummies sugar plum pastry gummi bears sugar plum dessert. Lollipop shortbread soufflé ice cream gummi bears apple pie icing. Marshmallow croissant dessert pie sweet roll. Toffee dragée cupcake cookie chocolate brownie candy canes. Tart sugar plum icing halvah muffin carrot cake. Marzipan jujubes lemon drops oat cake candy lemon drops lemon drops bonbon pie. Caramels jelly croissant cake cookie chocolate bar biscuit sesame snaps toffee. Marzipan ice cream candy bear claw chupa chups tiramisu. Shortbread brownie donut tootsie roll lollipop danish dragée cake marshmallow. Oat cake icing liquorice candy canes ice cream gummi bears chocolate cake pudding. Chocolate cake cake dessert dessert croissant gingerbread candy donut lollipop. Liquorice biscuit cotton candy gingerbread chocolate bar sugar plum apple pie gummies. Pie powder sweet roll candy canes marzipan chocolate. Chocolate cake sugar plum sesame snaps biscuit oat cake chupa chups cotton candy brownie pie. Jelly-o powder tart muffin gummies lollipop.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk HP 250 G9",
                ProductPrice = 7220,
                ProductImageName = "Noutbuk HP 250 G9.jpg",
                ProductDescription = "Marzipan jujubes lemon drops oat cake candy lemon drops lemon drops bonbon pie. Caramels jelly croissant cake cookie chocolate bar biscuit sesame snaps toffee. Marzipan ice cream candy bear claw chupa chups tiramisu. Shortbread brownie donut tootsie roll lollipop danish dragée cake marshmallow. Oat cake icing liquorice candy canes ice cream gummi bears chocolate cake pudding. Chocolate cake cake dessert dessert croissant gingerbread candy donut lollipop. Liquorice biscuit cotton candy gingerbread chocolate bar sugar plum apple pie gummies. Pie powder sweet roll candy canes marzipan chocolate. Chocolate cake sugar plum sesame snaps biscuit oat cake chupa chups cotton candy brownie pie. Jelly-o powder tart muffin gummies lollipop. Dragée gummi bears cupcake ice cream ice cream chocolate tiramisu biscuit brownie. Fruitcake shortbread soufflé chocolate jelly-o. Pudding gummies gummies cookie chocolate cake. Tootsie roll sweet roll powder apple pie macaroon cookie. Wafer cookie chupa chups cheesecake danish soufflé pastry. Marshmallow biscuit toffee tart jelly. Cheesecake marzipan danish candy canes jelly candy canes gummies chocolate tart. Cookie chocolate tiramisu cheesecake cotton candy topping gummi bears jelly-o danish. Croissant wafer danish gingerbread ice cream liquorice sesame snaps sugar plum shortbread. Ice cream macaroon icing brownie powder. Muffin cake powder chocolate bar apple pie chocolate bar. Oat cake jelly wafer chocolate halvah topping gummies pastry. Bonbon marshmallow jujubes pastry oat cake gingerbread muffin danish. Candy canes powder lemon drops bonbon marzipan chupa chups. Biscuit tiramisu cake cake pie gingerbread. Toffee tart tootsie roll soufflé cheesecake chocolate cake. Gummi bears biscuit jujubes pudding cake candy canes. Sweet lollipop muffin apple pie chocolate. Bear claw cake apple pie bonbon marshmallow macaroon. Shortbread cheesecake pie chocolate cupcake dessert candy brownie tiramisu. Chocolate cake tootsie roll gingerbread icing cheesecake. Muffin caramels muffin tart jelly-o. Soufflé brownie croissant tiramisu caramels. Ice cream candy sugar plum cotton candy shortbread icing lemon drops tart caramels. Wafer donut dessert tiramisu macaroon lemon drops brownie. Gummi bears candy canes sweet roll brownie topping.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk HP Pavilion X360 14-DW1010WM",
                ProductPrice = 4690,
                ProductImageName = "Noutbuk HP Pavilion X360 14-DW1010WM.jpg",
                ProductDescription = "Chocolate cake tootsie roll gingerbread icing cheesecake. Muffin caramels muffin tart jelly-o. Soufflé brownie croissant tiramisu caramels. Ice cream candy sugar plum cotton candy shortbread icing lemon drops tart caramels. Wafer donut dessert tiramisu macaroon lemon drops brownie. Gummi bears candy canes sweet roll brownie topping. Bear claw sesame snaps liquorice oat cake pastry apple pie oat cake shortbread. Jelly-o dessert dessert jelly cake candy canes gingerbread. Pudding candy canes pastry pudding bonbon cookie croissant caramels. Muffin jelly-o liquorice tiramisu cake cake chupa chups. Tart biscuit cake lollipop lollipop sesame snaps oat cake oat cake. Topping candy canes muffin fruitcake oat cake ice cream soufflé dragée brownie. Donut biscuit danish biscuit carrot cake. Cookie pie tiramisu cake marzipan cotton candy wafer jujubes oat cake. Pastry cupcake halvah pastry biscuit wafer sweet liquorice. Chocolate topping fruitcake muffin bonbon. Carrot cake carrot cake sesame snaps chocolate bar fruitcake gummi bears apple pie biscuit topping. Shortbread wafer tart gummi bears shortbread sesame snaps jelly lollipop. Oat cake toffee fruitcake lemon drops ice cream pastry pie halvah chocolate. Donut halvah croissant shortbread cotton candy marshmallow apple pie. Sugar plum donut jelly apple pie macaroon brownie macaroon gingerbread pie. Biscuit gummi bears macaroon sugar plum icing muffin liquorice. Cookie jelly beans chocolate bar jujubes pie icing. Halvah chupa chups chocolate cake donut brownie. Cupcake chocolate jujubes sweet lollipop pudding. Bear claw cookie jelly biscuit gummi bears. Sugar plum halvah carrot cake macaroon dessert icing candy canes lollipop. Candy canes tiramisu jujubes jelly-o lemon drops dragée marzipan lollipop. Pie jelly beans candy chocolate cake biscuit candy tootsie roll pastry powder. Cheesecake gummies cake candy canes cake pudding. Jujubes gummies icing icing bonbon bear claw cupcake donut. Pastry donut jelly lemon drops lemon drops brownie halvah jelly. Toffee donut muffin bear claw pie danish jelly.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk HP Victus 15-fa0032dx",
                ProductPrice = 7530,
                ProductImageName = "Noutbuk HP Victus 15-fa0032dx.jpg",
                ProductDescription = "Dragée chocolate bar caramels carrot cake chupa chups. Cookie shortbread oat cake sweet macaroon. Pastry biscuit sweet chocolate gummies pie. Jelly dragée cake fruitcake powder. Jujubes marzipan croissant pie chupa chups dessert tiramisu sweet roll. Bonbon sweet cupcake dragée jelly-o oat cake icing. Wafer shortbread cupcake cheesecake tootsie roll croissant tootsie roll macaroon jelly-o. Cake ice cream gingerbread shortbread chocolate bar pudding gingerbread dessert. Carrot cake brownie dragée cake wafer oat cake caramels fruitcake. Apple pie gummies fruitcake jelly-o cupcake icing. Candy cheesecake candy canes dragée fruitcake apple pie fruitcake jelly. Candy canes marshmallow jelly pie biscuit cake candy candy canes. Cake halvah gummi bears icing cake cookie dessert. Fruitcake bear claw cookie marzipan dessert biscuit chupa chups topping shortbread. Biscuit biscuit tart brownie jelly-o. Macaroon apple pie donut caramels jelly-o. Brownie gummi bears marzipan tootsie roll tart pie biscuit. Soufflé lemon drops sweet muffin marzipan pastry pudding powder. Sugar plum apple pie candy canes cotton candy powder ice cream. Cheesecake wafer macaroon chocolate cake candy. Lemon drops cake muffin oat cake muffin. Jelly-o soufflé apple pie brownie bear claw halvah cheesecake shortbread chocolate bar. Dragée shortbread halvah dessert jujubes. Jelly beans sesame snaps tiramisu jujubes chocolate cake brownie. Topping pudding icing gummi bears jelly chocolate topping.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk HP Victus 16-D0039UA",
                ProductPrice = 2960,
                ProductImageName = "Noutbuk HP Victus 16-D0039UA.jpg",
                ProductDescription = "Cheesecake wafer macaroon chocolate cake candy. Lemon drops cake muffin oat cake muffin. Jelly-o soufflé apple pie brownie bear claw halvah cheesecake shortbread chocolate bar. Dragée shortbread halvah dessert jujubes. Jelly beans sesame snaps tiramisu jujubes chocolate cake brownie. Topping pudding icing gummi bears jelly chocolate topping. Gingerbread soufflé donut sweet roll candy canes sweet roll brownie tootsie roll jelly-o. Lollipop tiramisu bear claw ice cream cake. Halvah jelly-o croissant toffee macaroon cake pie. Oat cake oat cake jelly beans bear claw caramels cotton candy. Sesame snaps croissant pie muffin marzipan. Candy canes soufflé powder bonbon bonbon gingerbread brownie carrot cake. Chocolate cake dessert tiramisu chupa chups chocolate bar donut caramels candy canes chocolate. Lemon drops brownie cake pie cookie bear claw dragée caramels biscuit. Pudding tootsie roll bonbon bonbon lemon drops. Gummi bears apple pie jujubes tiramisu caramels jelly beans danish tiramisu. Gingerbread gingerbread dessert sweet roll pudding tiramisu cupcake lollipop. Marzipan lemon drops jujubes sweet chocolate chocolate cake sugar plum. Soufflé carrot cake powder powder cupcake lollipop liquorice danish muffin.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo IdeaPad 1 14ADA05",
                ProductPrice = 6200,
                ProductImageName = "Noutbuk Lenovo IdeaPad 1 14ADA05.jpg",
                ProductDescription = "Soufflé carrot cake powder powder cupcake lollipop liquorice danish muffin. Carrot cake tart chupa chups topping pastry candy. Toffee chupa chups marzipan halvah macaroon pastry sweet. Danish dragée cotton candy chupa chups chupa chups candy canes tootsie roll jelly beans tart. Brownie pastry cupcake lemon drops tart cheesecake cheesecake gummies gummies. Carrot cake candy canes chocolate lemon drops macaroon chocolate gummi bears oat cake. Cookie tootsie roll biscuit brownie soufflé toffee macaroon. Cotton candy bonbon pastry pudding muffin jelly beans biscuit chupa chups jelly-o. Liquorice shortbread fruitcake carrot cake tiramisu chocolate. Caramels oat cake pie sweet roll muffin tootsie roll chocolate cotton candy. Carrot cake lemon drops caramels biscuit cookie fruitcake. Marshmallow gummies chocolate pudding sugar plum marzipan tootsie roll jelly candy canes. Donut tart candy canes sweet sesame snaps. Chocolate cake jelly-o cotton candy chocolate bar pudding toffee toffee caramels candy canes. Gummies tootsie roll donut powder gummi bears. Chocolate cake jelly-o lemon drops cotton candy marshmallow fruitcake. Fruitcake pudding jujubes chocolate cake liquorice carrot cake croissant cookie dragée. Carrot cake cheesecake caramels tiramisu lollipop lemon drops jelly-o. Pudding tootsie roll fruitcake jelly-o pudding tootsie roll marzipan chupa chups sesame snaps. Sweet roll marshmallow toffee chocolate jelly beans candy canes. Gummies sweet sweet roll cheesecake liquorice marzipan gingerbread pastry. Tart bonbon dessert cake croissant shortbread brownie cake tart. Lemon drops tart biscuit gingerbread cookie shortbread. Dragée dessert chocolate topping cotton candy. Dragée chocolate cake icing jelly beans tiramisu chocolate cake bear claw sweet roll. Tiramisu bonbon fruitcake chupa chups sweet roll.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo IdeaPad 3 14ITL6",
                ProductPrice = 3150,
                ProductImageName = "Noutbuk Lenovo IdeaPad 3 14ITL6.jpg",
                ProductDescription = "Carrot cake candy canes chocolate lemon drops macaroon chocolate gummi bears oat cake. Cookie tootsie roll biscuit brownie soufflé toffee macaroon. Cotton candy bonbon pastry pudding muffin jelly beans biscuit chupa chups jelly-o. Liquorice shortbread fruitcake carrot cake tiramisu chocolate. Caramels oat cake pie sweet roll muffin tootsie roll chocolate cotton candy. Carrot cake lemon drops caramels biscuit cookie fruitcake. Marshmallow gummies chocolate pudding sugar plum marzipan tootsie roll jelly candy canes. Donut tart candy canes sweet sesame snaps. Chocolate cake jelly-o cotton candy chocolate bar pudding toffee toffee caramels candy canes. Gummies tootsie roll donut powder gummi bears. Chocolate cake jelly-o lemon drops cotton candy marshmallow fruitcake. Fruitcake pudding jujubes chocolate cake liquorice carrot cake croissant cookie dragée. Carrot cake candy canes chocolate lemon drops macaroon chocolate gummi bears oat cake. Cookie tootsie roll biscuit brownie soufflé toffee macaroon. Cotton candy bonbon pastry pudding muffin jelly beans biscuit chupa chups jelly-o. Liquorice shortbread fruitcake carrot cake tiramisu chocolate. Caramels oat cake pie sweet roll muffin tootsie roll chocolate cotton candy. Carrot cake lemon drops caramels biscuit cookie fruitcake. Marshmallow gummies chocolate pudding sugar plum marzipan tootsie roll jelly candy canes. Donut tart candy canes sweet sesame snaps. Chocolate cake jelly-o cotton candy chocolate bar pudding toffee toffee caramels candy canes. Gummies tootsie roll donut powder gummi bears. Chocolate cake jelly-o lemon drops cotton candy marshmallow fruitcake. Fruitcake pudding jujubes chocolate cake liquorice carrot cake croissant cookie dragée. Carrot cake candy canes chocolate lemon drops macaroon chocolate gummi bears oat cake. Cookie tootsie roll biscuit brownie soufflé toffee macaroon. Cotton candy bonbon pastry pudding muffin jelly beans biscuit chupa chups jelly-o. Liquorice shortbread fruitcake carrot cake tiramisu chocolate. Caramels oat cake pie sweet roll muffin tootsie roll chocolate cotton candy. Carrot cake lemon drops caramels biscuit cookie fruitcake. Marshmallow gummies chocolate pudding sugar plum marzipan tootsie roll jelly candy canes. Donut tart candy canes sweet sesame snaps. Chocolate cake jelly-o cotton candy chocolate bar pudding toffee toffee caramels candy canes.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo IdeaPad Gaming 3 15IMH05",
                ProductPrice = 7420,
                ProductImageName = "Noutbuk Lenovo IdeaPad Gaming 3 15IMH05.jpg",
                ProductDescription = "Carrot cake cheesecake caramels tiramisu lollipop lemon drops jelly-o. Pudding tootsie roll fruitcake jelly-o pudding tootsie roll marzipan chupa chups sesame snaps. Sweet roll marshmallow toffee chocolate jelly beans candy canes. Gummies sweet sweet roll cheesecake liquorice marzipan gingerbread pastry. Tart bonbon dessert cake croissant shortbread brownie cake tart. Lemon drops tart biscuit gingerbread cookie shortbread. Dragée dessert chocolate topping cotton candy. Dragée chocolate cake icing jelly beans tiramisu chocolate cake bear claw sweet roll. Tiramisu bonbon fruitcake chupa chups sweet roll. Apple pie gingerbread sweet roll dragée candy canes chocolate cake bear claw icing cotton candy. Oat cake lollipop jelly beans jelly beans muffin. Chocolate cake cake cake bonbon cupcake tootsie roll candy canes lemon drops. Fruitcake icing apple pie toffee marzipan. Macaroon oat cake dragée sweet tiramisu brownie chupa chups. Gingerbread muffin cake brownie sesame snaps jujubes bear claw. Chupa chups chupa chups pastry brownie marshmallow. Jujubes jujubes apple pie candy canes jelly beans cookie cheesecake marzipan cake. Apple pie jelly-o dessert chupa chups biscuit sweet roll. Dragée sweet roll cookie caramels biscuit marzipan cake. Jelly-o topping muffin cake lollipop lollipop danish apple pie. Cupcake sesame snaps cupcake jelly cotton candy jelly-o cheesecake jelly-o oat cake. Fruitcake marshmallow carrot cake gingerbread carrot cake. Sweet pastry oat cake pastry carrot cake.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo IdeaPad Gaming 3i 15IAH7",
                ProductPrice = 6340,
                ProductImageName = "Noutbuk Lenovo IdeaPad Gaming 3i 15IAH7.jpg",
                ProductDescription = "Jelly-o topping muffin cake lollipop lollipop danish apple pie. Cupcake sesame snaps cupcake jelly cotton candy jelly-o cheesecake jelly-o oat cake. Fruitcake marshmallow carrot cake gingerbread carrot cake. Sweet pastry oat cake pastry carrot cake. Jelly beans halvah halvah bonbon dessert powder biscuit candy cupcake. Lollipop cake gingerbread liquorice cupcake tart pudding danish fruitcake. Cookie liquorice gingerbread dragée jelly cheesecake tart toffee. Chocolate jelly chocolate bar pudding jelly beans jelly topping chocolate cake lemon drops. Biscuit cupcake tiramisu macaroon dragée. Lollipop cake ice cream marzipan gingerbread liquorice chocolate chocolate candy. Sweet roll gummies bonbon bonbon gummi bears cookie jujubes. Carrot cake marshmallow gummies powder powder cheesecake sweet wafer. Wafer pastry caramels powder jujubes candy. Macaroon dessert jelly pastry soufflé. Cake bonbon lollipop biscuit wafer. Dragée jelly-o cotton candy chocolate bar tiramisu. Jujubes chupa chups pastry sweet roll icing cheesecake.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo Legion 5 Pro 16ACH6H",
                ProductPrice = 3420,
                ProductImageName = "Noutbuk Lenovo Legion 5 Pro 16ACH6H.jpg",
                ProductDescription = "Faucibus purus in massa tempor nec feugiat nisl pretium fusce id velit ut tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus faucibus ornare suspendisse. Purus in massa tempor nec feugiat nisl pretium fusce id velit ut tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus faucibus ornare suspendisse. Shortbread lemon drops candy canes ice cream cake powder cake. Icing powder liquorice chocolate danish candy canes halvah. Topping chocolate pastry fruitcake gummi bears chocolate bar bonbon candy canes. Caramels cookie cheesecake cupcake sesame snaps jelly cake lollipop. Macaroon tart pudding cake tiramisu. Muffin macaroon croissant carrot cake oat cake dessert muffin sweet roll. Sugar plum icing tiramisu pudding dessert brownie bear claw marzipan. Pastry biscuit bear claw jujubes chocolate cake cupcake. Shortbread candy canes bear claw bear claw candy fruitcake bonbon shortbread. Marshmallow danish tart liquorice sesame snaps. Pie chupa chups shortbread biscuit jelly-o. Tiramisu biscuit macaroon cotton candy caramels. Biscuit sweet roll brownie bonbon tootsie roll. Sed nisi lacus sed viverra tellus in hac habitasse platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus. Shortbread lemon drops candy canes ice cream cake powder cake. Icing powder liquorice chocolate danish candy canes halvah. Topping chocolate pastry fruitcake gummi bears chocolate bar bonbon candy canes. Caramels cookie cheesecake cupcake sesame snaps jelly cake lollipop. Macaroon tart pudding cake tiramisu. Muffin macaroon croissant carrot cake oat cake dessert muffin sweet roll. Sugar plum icing tiramisu pudding dessert brownie bear claw marzipan. Pastry biscuit bear claw jujubes chocolate cake cupcake. Shortbread candy canes bear claw bear claw candy fruitcake bonbon shortbread. Marshmallow danish tart liquorice sesame snaps. Pie chupa chups shortbread biscuit jelly-o. Tiramisu biscuit macaroon cotton candy caramels. Biscuit sweet roll brownie bonbon tootsie roll. Sed nisi lacus sed viverra tellus in hac habitasse platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo ThinkPad X1 Carbon G10",
                ProductPrice = 7320,
                ProductImageName = "Noutbuk Lenovo ThinkPad X1 Carbon G10.jpg",
                ProductDescription = "Chocolate cake tiramisu cake bear claw gingerbread. Cookie jelly ice cream toffee candy canes liquorice biscuit. Chocolate cake toffee bear claw muffin gingerbread tiramisu soufflé marshmallow toffee. Apple pie muffin chupa chups dragée macaroon carrot cake powder. Shortbread oat cake apple pie marzipan tootsie roll. Macaroon toffee macaroon cake cake marshmallow lemon drops brownie. Topping chupa chups lemon drops cupcake pudding biscuit tart. Chocolate cake tiramisu cake bear claw gingerbread. Cookie jelly ice cream toffee candy canes liquorice biscuit. Chocolate cake toffee bear claw muffin gingerbread tiramisu soufflé marshmallow toffee. Apple pie muffin chupa chups dragée macaroon carrot cake powder. Shortbread oat cake apple pie marzipan tootsie roll. Macaroon toffee macaroon cake cake marshmallow lemon drops brownie. Topping chupa chups lemon drops cupcake pudding biscuit tart. Halvah sesame snaps chocolate bar cupcake ice cream fruitcake. Pie lollipop macaroon caramels chocolate liquorice chocolate cake cotton candy. Brownie croissant jelly liquorice shortbread ice cream. Dessert gingerbread sweet sweet roll sweet. Lollipop gummies bear claw cheesecake shortbread croissant chupa chups. Soufflé chocolate toffee jelly chupa chups halvah gingerbread. Liquorice liquorice lollipop jelly fruitcake chocolate muffin bear claw. Halvah sesame snaps chocolate bar cupcake ice cream fruitcake. Pie lollipop macaroon caramels chocolate liquorice chocolate cake cotton candy. Brownie croissant jelly liquorice shortbread ice cream. Dessert gingerbread sweet sweet roll sweet. Lollipop gummies bear claw cheesecake shortbread croissant chupa chups. Soufflé chocolate toffee jelly chupa chups halvah gingerbread. Liquorice liquorice lollipop jelly fruitcake chocolate muffin bear claw.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo V15 G1 IML",
                ProductPrice = 3240,
                ProductImageName = "Noutbuk Lenovo V15 G1 IML.jpg",
                ProductDescription = "Caramels gummies sugar plum chocolate bar jelly beans bear claw shortbread macaroon marshmallow. Muffin shortbread chupa chups candy canes oat cake apple pie sweet jelly lollipop. Lemon drops tiramisu jelly beans marzipan chocolate cake jelly cotton candy. Marzipan candy muffin cheesecake powder marshmallow gummi bears cotton candy. Croissant oat cake chocolate cake sweet jelly-o cupcake chocolate cheesecake. Ice cream chupa chups tootsie roll dessert pudding marzipan. Danish chocolate sweet roll danish gummies marzipan cake carrot cake ice cream. Caramels gummies sugar plum chocolate bar jelly beans bear claw shortbread macaroon marshmallow. Muffin shortbread chupa chups candy canes oat cake apple pie sweet jelly lollipop. Lemon drops tiramisu jelly beans marzipan chocolate cake jelly cotton candy. Marzipan candy muffin cheesecake powder marshmallow gummi bears cotton candy. Soufflé ice cream marzipan cheesecake cupcake apple pie pie chocolate. Croissant jelly soufflé chocolate jelly macaroon sesame snaps carrot cake sesame snaps. Cookie dessert icing sesame snaps danish jelly beans macaroon cotton candy. Cotton candy pudding fruitcake dessert marshmallow sweet pastry lollipop. Jelly-o cupcake sweet shortbread apple pie cookie pie apple pie chupa chups. Soufflé ice cream marzipan cheesecake cupcake apple pie pie chocolate. Croissant jelly soufflé chocolate jelly macaroon sesame snaps carrot cake sesame snaps. Cookie dessert icing sesame snaps danish jelly beans macaroon cotton candy. Cotton candy pudding fruitcake dessert marshmallow sweet pastry lollipop. Croissant oat cake chocolate cake sweet jelly-o cupcake chocolate cheesecake. Ice cream chupa chups tootsie roll dessert pudding marzipan. Danish chocolate sweet roll danish gummies marzipan cake carrot cake ice cream. Jelly-o cupcake sweet shortbread apple pie cookie pie apple pie chupa chups. Soufflé ice cream marzipan cheesecake cupcake apple pie pie chocolate. Croissant jelly soufflé chocolate jelly macaroon sesame snaps carrot cake sesame snaps. Cookie dessert icing sesame snaps danish jelly beans macaroon cotton candy. Cotton candy pudding fruitcake dessert marshmallow sweet pastry lollipop. Jelly-o cupcake sweet shortbread apple pie cookie pie apple pie chupa chups.",
                ProductIsApproved = true,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk Lenovo V15-IGL",
                ProductPrice = 6130,
                ProductImageName = "Noutbuk Lenovo V15-IGL.jpg",
                ProductDescription = "Croissant dessert chocolate cake tiramisu jujubes shortbread danish. Candy tootsie roll marshmallow sesame snaps shortbread. Gummi bears tart soufflé cake cheesecake jelly beans tootsie roll. Chocolate bar cake croissant candy danish liquorice lemon drops cake. Sweet roll marshmallow gummi bears wafer sugar plum caramels. Wafer candy canes chocolate bar powder tart apple pie sesame snaps jelly. Candy canes marshmallow donut lemon drops chupa chups sweet roll muffin sugar plum biscuit. Croissant dessert chocolate cake tiramisu jujubes shortbread danish. Candy tootsie roll marshmallow sesame snaps shortbread. Gummi bears tart soufflé cake cheesecake jelly beans tootsie roll. Chocolate bar cake croissant candy danish liquorice lemon drops cake. Sweet roll marshmallow gummi bears wafer sugar plum caramels. Wafer candy canes chocolate bar powder tart apple pie sesame snaps jelly. Candy canes marshmallow donut lemon drops chupa chups sweet roll muffin sugar plum biscuit. Brownie chocolate cake gummies marshmallow powder. Biscuit dessert chupa chups bonbon candy canes chupa chups chocolate cake chocolate cake. Bear claw candy jelly beans sugar plum biscuit gummi bears pie jelly-o. Jelly-o lollipop sugar plum chupa chups danish. Croissant shortbread sugar plum marzipan tiramisu pastry pie pudding. Caramels marshmallow cookie sweet chocolate cake chocolate cake croissant topping marzipan. Chocolate tart cupcake tiramisu lollipop. Brownie chocolate cake gummies marshmallow powder. Biscuit dessert chupa chups bonbon candy canes chupa chups chocolate cake chocolate cake. Bear claw candy jelly beans sugar plum biscuit gummi bears pie jelly-o. Jelly-o lollipop sugar plum chupa chups danish. Croissant shortbread sugar plum marzipan tiramisu pastry pie pudding. Caramels marshmallow cookie sweet chocolate cake chocolate cake croissant topping marzipan. Chocolate tart cupcake tiramisu lollipop.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Noutbuk MSI GF63 11SC-693US",
                ProductPrice = 7420,
                ProductImageName = "Noutbuk MSI GF63 11SC-693US.jpg",
                ProductDescription = "Jelly-o halvah sweet danish sweet lemon drops croissant. Tootsie roll marzipan tootsie roll gingerbread cookie. Gummi bears ice cream brownie icing brownie. Halvah jelly-o sweet roll pie muffin pastry lemon drops lemon drops. Wafer biscuit bonbon pastry danish brownie toffee. Candy canes apple pie chupa chups dragée dragée fruitcake jujubes danish macaroon. Marshmallow cupcake jelly apple pie dragée apple pie gummies dessert bonbon. Wafer candy tart halvah gummies. Marshmallow fruitcake tiramisu sweet jelly fruitcake marzipan marshmallow gummi bears. Jelly-o dragée jujubes pudding carrot cake dessert tart. Marshmallow dessert cookie cookie gingerbread. Sugar plum dragée wafer chupa chups jelly. Tiramisu apple pie jelly icing dessert wafer cake icing liquorice. Cupcake tiramisu chupa chups tiramisu liquorice brownie pudding sesame snaps biscuit. Brownie sweet roll macaroon cake muffin. Chocolate pudding cake jujubes tart shortbread chocolate cake bear claw cupcake. Croissant tiramisu marshmallow caramels pudding. Tart pudding sweet dragée tootsie roll cupcake dessert chocolate bar. Dragée oat cake gingerbread bonbon tart jujubes. Jelly-o caramels sweet roll jelly-o marzipan. Liquorice cake lemon drops cotton candy jelly-o carrot cake. Chupa chups jelly beans liquorice shortbread biscuit apple pie. Pudding cotton candy cupcake chupa chups icing. Dessert marshmallow candy canes cheesecake sesame snaps jelly-o ice cream. Tiramisu apple pie shortbread cupcake dragée candy ice cream jelly carrot cake. Cheesecake marshmallow liquorice bonbon jelly.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            #endregion Noutbuklar

            /* = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = */

            #region Saatlar
            new Product()
            {
                ProductName = "Ağıllı saat Borofone BD2 Black",
                ProductPrice = 6130,
                ProductImageName = "Saat Ağıllı saat Borofone BD2 Black.jpg",
                ProductDescription = "Carrot cake ice cream cupcake donut sesame snaps. Cake danish croissant chocolate bar sweet roll fruitcake sesame snaps jujubes. Tiramisu gummies lollipop sweet apple pie ice cream topping. Oat cake cupcake halvah chupa chups shortbread. Bonbon ice cream candy cupcake cookie caramels jelly beans. Jujubes fruitcake chupa chups dessert dragée toffee gingerbread. Dessert lemon drops icing tootsie roll jujubes. Carrot cake ice cream cupcake donut sesame snaps. Cake danish croissant chocolate bar sweet roll fruitcake sesame snaps jujubes. Tiramisu gummies lollipop sweet apple pie ice cream topping. Oat cake cupcake halvah chupa chups shortbread. Bonbon ice cream candy cupcake cookie caramels jelly beans. Jujubes fruitcake chupa chups dessert dragée toffee gingerbread. Dessert lemon drops icing tootsie roll jujubes. Chupa chups candy toffee biscuit macaroon ice cream jelly-o sesame snaps sweet. Toffee sesame snaps gingerbread shortbread macaroon shortbread gingerbread. Marzipan muffin biscuit fruitcake chocolate bar cheesecake. Marzipan pastry halvah macaroon cupcake cake candy canes. Icing cotton candy apple pie chocolate bar cake topping gummies. Liquorice ice cream gummies sweet roll chocolate bar biscuit jujubes pudding bonbon. Chupa chups candy toffee biscuit macaroon ice cream jelly-o sesame snaps sweet. Toffee sesame snaps gingerbread shortbread macaroon shortbread gingerbread. Marzipan muffin biscuit fruitcake chocolate bar cheesecake. Marzipan pastry halvah macaroon cupcake cake candy canes. Icing cotton candy apple pie chocolate bar cake topping gummies. Liquorice ice cream gummies sweet roll chocolate bar biscuit jujubes pudding bonbon.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Ağıllı saat Hoco Y8 Rose Gold",
                ProductPrice = 7330,
                ProductImageName = "Saat Ağıllı saat Hoco Y8 Rose Gold.jpg",
                ProductDescription = "Pudding ice cream jelly fruitcake chocolate bar ice cream croissant jelly beans jujubes. Lollipop chocolate gingerbread wafer sugar plum sweet. Marshmallow macaroon gummies jelly beans biscuit wafer chocolate bar caramels. Candy bear claw caramels sweet roll toffee wafer. Icing sugar plum soufflé cake muffin pastry pudding. Chupa chups muffin croissant tootsie roll cotton candy wafer liquorice. Chocolate cake tiramisu toffee liquorice lemon drops pie muffin fruitcake jelly. Pudding ice cream jelly fruitcake chocolate bar ice cream croissant jelly beans jujubes. Lollipop chocolate gingerbread wafer sugar plum sweet. Marshmallow macaroon gummies jelly beans biscuit wafer chocolate bar caramels. Candy bear claw caramels sweet roll toffee wafer. Icing sugar plum soufflé cake muffin pastry pudding. Chupa chups muffin croissant tootsie roll cotton candy wafer liquorice. Chocolate cake tiramisu toffee liquorice lemon drops pie muffin fruitcake jelly. Chocolate bar cotton candy dragée croissant chocolate bar. Donut jujubes powder sweet roll tart jelly cupcake pudding dessert. Ice cream sugar plum sweet roll tiramisu jelly beans halvah cake. Cookie dragée topping dessert icing. Fruitcake cake carrot cake candy canes bonbon. Chocolate bar cotton candy dragée croissant chocolate bar. Donut jujubes powder sweet roll tart jelly cupcake pudding dessert. Ice cream sugar plum sweet roll tiramisu jelly beans halvah cake. Cookie dragée topping dessert icing. Fruitcake cake carrot cake candy canes bonbon.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Ağıllı saat Smart Watch HW37 Black",
                ProductPrice = 9220,
                ProductImageName = "Saat Ağıllı saat Smart Watch HW37 Black.jpg",
                ProductDescription = "Dragée icing cookie tiramisu macaroon. Tart sweet roll candy canes tart fruitcake bear claw cotton candy. Sesame snaps bear claw tart soufflé caramels cake powder. Pudding cookie liquorice jelly-o cotton candy sesame snaps marzipan. Tart sweet roll pie muffin marzipan chupa chups. Tootsie roll sweet lemon drops cheesecake donut pie. Wafer donut sesame snaps soufflé cake powder dessert brownie apple pie. Dessert pastry gummies candy canes caramels marzipan. Cookie bonbon tart marzipan lollipop chocolate bar sweet candy canes. Powder apple pie icing cookie dessert brownie. Dragée icing cookie tiramisu macaroon. Tart sweet roll candy canes tart fruitcake bear claw cotton candy. Sesame snaps bear claw tart soufflé caramels cake powder. Pudding cookie liquorice jelly-o cotton candy sesame snaps marzipan. Tart sweet roll pie muffin marzipan chupa chups. Tootsie roll sweet lemon drops cheesecake donut pie. Wafer donut sesame snaps soufflé cake powder dessert brownie apple pie. Dessert pastry gummies candy canes caramels marzipan. Cookie bonbon tart marzipan lollipop chocolate bar sweet candy canes. Powder apple pie icing cookie dessert brownie. Wafer wafer candy canes gummi bears jelly-o. Gingerbread macaroon jujubes lemon drops cookie gummi bears. Jelly beans wafer cake fruitcake sugar plum oat cake topping. Pastry cake bear claw chocolate cake ice cream tootsie roll candy oat cake. Wafer wafer candy canes gummi bears jelly-o. Gingerbread macaroon jujubes lemon drops cookie gummi bears. Jelly beans wafer cake fruitcake sugar plum oat cake topping. Pastry cake bear claw chocolate cake ice cream tootsie roll candy oat cake.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Masaüstü saat-zəngli saat 'Telefon', 18.5x12.5x19 sm, gümüşü",
                ProductPrice = 6160,
                ProductImageName = "Saat Masaüstü saat-zəngli saat 'Telefon', 18.5x12.5x19 sm, gümüşü, məhsul çeşiddə.jpg",
                ProductDescription = "Macaroon shortbread wafer ice cream cotton candy liquorice cupcake marzipan lemon drops. Wafer wafer cotton candy pastry cotton candy macaroon. Dessert cotton candy jujubes jelly dragée. Sweet tiramisu jelly beans chocolate marzipan donut marzipan. Macaroon shortbread wafer ice cream cotton candy liquorice cupcake marzipan lemon drops. Wafer wafer cotton candy pastry cotton candy macaroon. Dessert cotton candy jujubes jelly dragée. Sweet tiramisu jelly beans chocolate marzipan donut marzipan. Danish liquorice brownie ice cream chocolate marzipan jelly beans biscuit icing. Marshmallow gummi bears gummi bears gingerbread cake candy cake. Lollipop gummies chocolate tiramisu dragée wafer. Candy jujubes chocolate bar cake bear claw. Pudding danish cupcake dessert tiramisu jelly apple pie lemon drops. Chocolate cake bonbon carrot cake soufflé jelly-o toffee dragée biscuit. Marshmallow cake donut candy chocolate bar. Donut gingerbread cake jelly brownie sweet roll. Muffin sweet shortbread candy canes fruitcake cheesecake jelly halvah gingerbread. Macaroon shortbread wafer ice cream cotton candy liquorice cupcake marzipan lemon drops. Wafer wafer cotton candy pastry cotton candy macaroon. Dessert cotton candy jujubes jelly dragée. Sweet tiramisu jelly beans chocolate marzipan donut marzipan. Danish liquorice brownie ice cream chocolate marzipan jelly beans biscuit icing. Marshmallow gummi bears gummi bears gingerbread cake candy cake. Lollipop gummies chocolate tiramisu dragée wafer. Candy jujubes chocolate bar cake bear claw. Pudding danish cupcake dessert tiramisu jelly apple pie lemon drops. Chocolate cake bonbon carrot cake soufflé jelly-o toffee dragée biscuit. Marshmallow cake donut candy chocolate bar. Donut gingerbread cake jelly brownie sweet roll. Muffin sweet shortbread candy canes fruitcake cheesecake jelly halvah gingerbread. Danish liquorice brownie ice cream chocolate marzipan jelly beans biscuit icing. Marshmallow gummi bears gummi bears gingerbread cake candy cake. Lollipop gummies chocolate tiramisu dragée wafer. Candy jujubes chocolate bar cake bear claw. Pudding danish cupcake dessert tiramisu jelly apple pie lemon drops. Chocolate cake bonbon carrot cake soufflé jelly-o toffee dragée biscuit. Marshmallow cake donut candy chocolate bar. Donut gingerbread cake jelly brownie sweet roll. Muffin sweet shortbread candy canes fruitcake cheesecake jelly halvah gingerbread.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Masaüstü saat-zəngli saat 'Yer', 22x6x11 sm, gümüşü",
                ProductPrice = 3260,
                ProductImageName = "Saat Masaüstü saat-zəngli saat 'Yer', 22x6x11 sm, gümüşü.jpg",
                ProductDescription = "Caramels danish bear claw jelly beans jujubes topping bear claw tootsie roll lollipop. Jelly chocolate lollipop ice cream tootsie roll chocolate cake sesame snaps brownie dragée. Bear claw cake gummi bears toffee chocolate caramels. Pie cotton candy pastry halvah topping cheesecake. Pastry chocolate lollipop croissant toffee wafer donut sweet. Dessert tiramisu danish jelly-o carrot cake candy canes croissant sweet roll. Caramels danish bear claw jelly beans jujubes topping bear claw tootsie roll lollipop. Jelly chocolate lollipop ice cream tootsie roll chocolate cake sesame snaps brownie dragée. Bear claw cake gummi bears toffee chocolate caramels. Pie cotton candy pastry halvah topping cheesecake. Pastry chocolate lollipop croissant toffee wafer donut sweet. Dessert tiramisu danish jelly-o carrot cake candy canes croissant sweet roll. Caramels sesame snaps toffee liquorice topping cheesecake cupcake icing biscuit. Chocolate cake marzipan oat cake lollipop toffee carrot cake marzipan oat cake cake. Carrot cake pudding liquorice tart sugar plum cotton candy. Candy canes sesame snaps brownie cake tootsie roll. Jujubes jelly pudding lollipop cotton candy croissant cake tart. Gummies chocolate cake sesame snaps halvah sugar plum macaroon sweet tootsie roll dessert. Candy canes icing icing tootsie roll cake jelly macaroon lemon drops chupa chups. Caramels danish bear claw jelly beans jujubes topping bear claw tootsie roll lollipop. Jelly chocolate lollipop ice cream tootsie roll chocolate cake sesame snaps brownie dragée. Bear claw cake gummi bears toffee chocolate caramels. Pie cotton candy pastry halvah topping cheesecake. Pastry chocolate lollipop croissant toffee wafer donut sweet. Dessert tiramisu danish jelly-o carrot cake candy canes croissant sweet roll. Caramels sesame snaps toffee. Caramels sesame snaps toffee liquorice topping cheesecake cupcake icing biscuit. Chocolate cake marzipan oat cake lollipop toffee carrot cake marzipan oat cake cake. Carrot cake pudding liquorice tart sugar plum cotton candy. Candy canes sesame snaps brownie cake tootsie roll. Jujubes jelly pudding lollipop cotton candy croissant cake tart. Gummies chocolate cake sesame snaps halvah sugar plum macaroon sweet tootsie roll dessert. Candy canes icing icing tootsie roll cake jelly macaroon lemon drops chupa chups.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Rəqəmsal saat-zəngli saat, termometrli, ağ",
                ProductPrice = 6720,
                ProductImageName = "Saat Rəqəmsal saat-zəngli saat, termometrli, ağ.jpg",
                ProductDescription = "Marshmallow jelly beans cotton candy jujubes oat cake toffee chupa chups chocolate tart. Sesame snaps biscuit gummies pudding oat cake gummi bears. Sweet chocolate shortbread macaroon candy canes pastry dragée apple pie gummies. Gingerbread macaroon chupa chups oat cake marshmallow caramels tootsie roll sugar plum. Marshmallow jelly beans cotton candy jujubes oat cake toffee chupa chups chocolate tart. Sesame snaps biscuit gummies pudding oat cake gummi bears. Sweet chocolate shortbread macaroon candy canes pastry dragée apple pie gummies. Gingerbread macaroon chupa chups oat cake marshmallow caramels tootsie roll sugar plum. Candy canes gingerbread cake caramels tootsie roll powder icing chupa chups wafer. Shortbread jelly beans ice cream cookie caramels pie croissant candy canes dessert. Lollipop dragée jujubes pastry sweet lollipop pie marshmallow. Biscuit fruitcake pie soufflé marshmallow dragée donut. Oat cake cake pastry bonbon toffee cotton candy. Dessert macaroon oat cake chupa chups sweet oat cake. Gummies sweet roll chocolate bar cake powder macaroon caramels. Sweet roll chupa chups bonbon chocolate bar cake chocolate bar cupcake icing. Gummi bears carrot cake toffee cake jujubes. Muffin croissant cupcake sweet pastry dragée. Candy canes gingerbread cake caramels tootsie roll powder icing chupa chups wafer. Shortbread jelly beans ice cream cookie caramels pie croissant candy canes dessert. Lollipop dragée jujubes pastry sweet lollipop pie marshmallow. Biscuit fruitcake pie soufflé marshmallow dragée donut. Oat cake cake pastry bonbon toffee cotton candy. Dessert macaroon oat cake chupa chups sweet oat cake. Gummies sweet roll chocolate bar cake powder macaroon caramels. Sweet roll chupa chups bonbon chocolate bar cake chocolate bar cupcake icing. Gummi bears carrot cake toffee cake jujubes. Muffin croissant cupcake sweet pastry dragée.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Saat-zəngli saat JYSK Sven, 5х8х14 sm, qara",
                ProductPrice = 5730,
                ProductImageName = "Saat-zəngli saat JYSK Sven, 5х8х14 sm, qara.jpg",
                ProductDescription = "Cotton candy marshmallow cookie toffee bear claw chocolate bar tart powder. Pie tart donut croissant soufflé sugar plum. Cake toffee apple pie oat cake sesame snaps powder pastry gingerbread. Caramels cake chocolate cake chocolate cake tiramisu gingerbread soufflé. Halvah toffee marshmallow fruitcake jelly oat cake. Pastry cake cake pastry chocolate bar. Cotton candy marshmallow cookie toffee bear claw chocolate bar tart powder. Pie tart donut croissant soufflé sugar plum. Cake toffee apple pie oat cake sesame snaps powder pastry gingerbread. Caramels cake chocolate cake chocolate cake tiramisu gingerbread soufflé. Halvah toffee marshmallow fruitcake jelly oat cake. Pastry cake cake pastry chocolate bar. Gummies dragée apple pie tootsie roll gummi bears halvah toffee marshmallow cotton candy. Chocolate cake lollipop icing wafer sesame snaps sweet roll shortbread caramels. Ice cream candy chocolate bar marshmallow apple pie donut cookie pie sweet roll. Wafer liquorice ice cream sesame snaps chocolate cake lemon drops cotton candy marzipan jelly beans. Cake tart bonbon chocolate bar sesame snaps sesame snaps. Liquorice chocolate bar biscuit tart apple pie. Carrot cake soufflé chocolate bar jelly croissant powder sweet icing. Gummies dragée apple pie tootsie roll gummi bears halvah toffee marshmallow cotton candy. Chocolate cake lollipop icing wafer sesame snaps sweet roll shortbread caramels. Ice cream candy chocolate bar marshmallow apple pie donut cookie pie sweet roll. Wafer liquorice ice cream sesame snaps chocolate cake lemon drops cotton candy marzipan jelly beans. Cake tart bonbon chocolate bar sesame snaps sesame snaps. Liquorice chocolate bar biscuit tart apple pie. Carrot cake soufflé chocolate bar jelly croissant powder sweet icing.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Smart saat DT No.1 DT7 Mini Silver",
                ProductPrice = 8020,
                ProductImageName = "Saat Smart saat DT No.1 DT7 Mini Silver.jpg",
                ProductDescription = "Powder pastry shortbread ice cream shortbread cake marshmallow cake. Chocolate cake lollipop icing jelly beans pastry topping muffin. Jelly-o jelly beans soufflé apple pie muffin chocolate bar cotton candy topping marshmallow. Oat cake pudding chupa chups dessert shortbread. Powder pastry shortbread ice cream shortbread cake marshmallow cake. Chocolate cake lollipop icing jelly beans pastry topping muffin. Jelly-o jelly beans soufflé apple pie muffin chocolate bar cotton candy topping marshmallow. Oat cake pudding chupa chups dessert shortbread. Toffee toffee croissant bear claw tart chupa chups. Chocolate cake candy cake caramels pie sugar plum sweet wafer. Sweet apple pie pie bear claw soufflé. Gummi bears chocolate cake toffee marshmallow danish. Carrot cake macaroon cookie sesame snaps pie. Muffin cookie marzipan tart tootsie roll cotton candy chocolate bar pastry cheesecake. Liquorice fruitcake powder liquorice jujubes macaroon lemon drops gingerbread chocolate. Caramels cake dragée bear claw soufflé. Caramels carrot cake donut toffee pudding caramels tart. Tiramisu lemon drops oat cake chocolate shortbread. Toffee toffee croissant bear claw tart chupa chups. Chocolate cake candy cake caramels pie sugar plum sweet wafer. Sweet apple pie pie bear claw soufflé. Gummi bears chocolate cake toffee marshmallow danish. Carrot cake macaroon cookie sesame snaps pie. Muffin cookie marzipan tart tootsie roll cotton candy chocolate bar pastry cheesecake. Liquorice fruitcake powder liquorice jujubes macaroon lemon drops gingerbread chocolate. Caramels cake dragée bear claw soufflé. Caramels carrot cake donut toffee pudding caramels tart. Tiramisu lemon drops oat cake chocolate shortbread.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Smart saat Haylou GST",
                ProductPrice = 7810,
                ProductImageName = "Saat Smart saat Haylou GST.jpg",
                ProductDescription = "Liquorice cookie halvah cake cake chocolate cake icing. Brownie gummies candy canes soufflé dragée shortbread. Wafer icing gingerbread tiramisu dessert chocolate. Cookie powder croissant brownie toffee chocolate cake. Jelly-o soufflé sesame snaps pastry donut pastry topping. Gummi bears halvah fruitcake marzipan donut. Bonbon chocolate fruitcake biscuit jelly beans. Wafer dragée caramels marzipan powder caramels chocolate bar chocolate cake biscuit. Liquorice cookie halvah cake cake chocolate cake icing. Brownie gummies candy canes soufflé dragée shortbread. Wafer icing gingerbread tiramisu dessert chocolate. Cookie powder croissant brownie toffee chocolate cake. Jelly-o soufflé sesame snaps pastry donut pastry topping. Gummi bears halvah fruitcake marzipan donut. Bonbon chocolate fruitcake biscuit jelly beans. Wafer dragée caramels marzipan powder caramels chocolate bar chocolate cake biscuit. Cake candy canes icing chocolate apple pie gummies. Shortbread chocolate pastry wafer tootsie roll toffee fruitcake pastry. Sugar plum brownie gingerbread gummies jelly-o lollipop cake chocolate bar ice cream. Dragée bonbon lemon drops fruitcake caramels brownie candy canes gummies chocolate cake. Cookie sweet croissant oat cake carrot cake chocolate danish. Liquorice cookie halvah cake cake chocolate cake icing. Brownie gummies candy canes soufflé dragée shortbread. Wafer icing gingerbread tiramisu dessert chocolate. Cookie powder croissant brownie toffee chocolate cake. Jelly-o soufflé sesame snaps pastry donut pastry topping. Gummi bears halvah fruitcake marzipan donut. Bonbon chocolate fruitcake biscuit jelly beans. Wafer dragée caramels marzipan powder caramels chocolate bar chocolate cake biscuit. Cake candy canes icing chocolate apple pie gummies. Shortbread chocolate pastry wafer tootsie roll toffee fruitcake pastry. Sugar plum brownie gingerbread gummies jelly-o lollipop cake chocolate bar ice cream. Dragée bonbon lemon drops fruitcake caramels brownie candy canes gummies chocolate cake. Cookie sweet croissant oat cake carrot cake chocolate danish. Cake candy canes icing chocolate apple pie gummies. Shortbread chocolate pastry wafer tootsie roll toffee fruitcake pastry. Sugar plum brownie gingerbread gummies jelly-o lollipop cake chocolate bar ice cream. Dragée bonbon lemon drops fruitcake caramels brownie candy canes gummies chocolate cake. Cookie sweet croissant oat cake carrot cake chocolate danish.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Smart-saat Smart Watch GS7 Pro Max Black",
                ProductPrice = 7640,
                ProductImageName = "Saat Smart-saat Smart Watch GS7 Pro Max Black.jpg",
                ProductDescription = "Cupcake tart topping muffin dessert ice cream sugar plum. Croissant apple pie jelly beans muffin ice cream cotton candy bonbon. Halvah topping caramels danish dragée dessert. Danish wafer pie jelly beans donut carrot cake ice cream sugar plum. Apple pie dragée candy tiramisu donut chupa chups marzipan. Sugar plum candy canes caramels candy canes biscuit muffin. Chocolate bar jelly-o toffee donut pudding powder pudding cake sesame snaps. Cupcake tart topping muffin dessert ice cream sugar plum. Croissant apple pie jelly beans muffin ice cream cotton candy bonbon. Halvah topping caramels danish dragée dessert. Danish wafer pie jelly beans donut carrot cake ice cream sugar plum. Apple pie dragée candy tiramisu donut chupa chups marzipan. Sugar plum candy canes caramels candy canes biscuit muffin. Chocolate bar jelly-o toffee donut pudding powder pudding cake sesame snaps. Toffee caramels marshmallow liquorice bear claw donut halvah tootsie roll. Topping cake croissant chupa chups brownie tootsie roll oat cake sugar plum cheesecake. Gummi bears chupa chups powder cake tart. Topping biscuit dragée cotton candy cake. Tiramisu cake chocolate cake sesame snaps soufflé apple pie candy. Fruitcake croissant chocolate pie biscuit lollipop chupa chups sesame snaps macaroon. Cupcake tart topping muffin dessert ice cream sugar plum. Croissant apple pie jelly beans muffin ice cream cotton candy bonbon. Halvah topping caramels danish dragée dessert. Danish wafer pie jelly beans donut carrot cake ice cream sugar plum. Apple pie dragée candy tiramisu donut chupa chups marzipan. Sugar plum candy canes caramels candy canes biscuit muffin. Chocolate bar jelly-o toffee donut pudding powder pudding cake sesame snaps. Toffee caramels marshmallow liquorice bear claw donut halvah tootsie roll. Topping cake croissant chupa chups brownie tootsie roll oat cake sugar plum cheesecake. Gummi bears chupa chups powder cake tart. Topping biscuit dragée cotton candy cake. Tiramisu cake chocolate cake sesame snaps soufflé apple pie candy. Toffee caramels marshmallow liquorice bear claw donut halvah tootsie roll. Topping cake croissant chupa chups brownie tootsie roll oat cake sugar plum cheesecake. Gummi bears chupa chups powder cake tart. Topping biscuit dragée cotton candy cake. Tiramisu cake chocolate cake sesame snaps soufflé apple pie candy. Fruitcake croissant chocolate pie biscuit lollipop chupa chups sesame snaps macaroon.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            #endregion Saatlar

            /* = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = */

            #region Telefonlar
            new Product()
            {
                ProductName = "Smartfon Apple iPhone 13 4GB/128GB Midnight",
                ProductPrice = 4890,
                ProductImageName = "Smartfon Apple iPhone 13 4GB,128GB Midnight.jpg",
                ProductDescription = "Chupa chups fruitcake oat cake pie pastry sugar plum pie wafer liquorice. Cupcake liquorice cheesecake tiramisu gummies jelly caramels donut pie. Sweet sweet sesame snaps chocolate caramels icing shortbread lemon drops sesame snaps. Jelly-o carrot cake pudding halvah jelly soufflé tiramisu pudding soufflé. Macaroon jelly-o candy canes tart liquorice tiramisu cake liquorice. Jelly beans soufflé tart wafer gummies. Chupa chups fruitcake oat cake pie pastry sugar plum pie wafer liquorice. Cupcake liquorice cheesecake tiramisu gummies jelly caramels donut pie. Sweet sweet sesame snaps chocolate caramels icing shortbread lemon drops sesame snaps. Jelly-o carrot cake pudding halvah jelly soufflé tiramisu pudding soufflé. Macaroon jelly-o candy canes tart liquorice tiramisu cake liquorice. Jelly beans soufflé tart wafer gummies. Chocolate bar lollipop sesame snaps danish carrot cake chocolate. Chocolate cake fruitcake sugar plum biscuit donut cupcake sweet muffin. Chupa chups icing muffin wafer sesame snaps sesame snaps. Jelly beans wafer chocolate cupcake chocolate bar chocolate muffin topping sesame snaps. Cake cupcake jelly-o marshmallow cookie. Macaroon powder macaroon muffin lollipop shortbread marzipan. Chupa chups fruitcake oat cake pie pastry sugar plum pie wafer liquorice. Cupcake liquorice cheesecake tiramisu gummies jelly caramels donut pie. Sweet sweet sesame snaps chocolate caramels icing shortbread lemon drops sesame snaps. Jelly-o carrot cake pudding halvah jelly soufflé tiramisu pudding soufflé. Macaroon jelly-o candy canes tart liquorice tiramisu cake liquorice. Jelly beans soufflé tart wafer gummies. Chocolate bar lollipop sesame snaps danish carrot cake chocolate. Chocolate cake fruitcake sugar plum biscuit donut cupcake sweet muffin. Chupa chups icing muffin wafer sesame snaps sesame snaps. Jelly beans wafer chocolate cupcake chocolate bar chocolate muffin topping sesame snaps. Cake cupcake jelly-o marshmallow cookie. Macaroon powder macaroon muffin lollipop shortbread marzipan. Chocolate bar lollipop sesame snaps danish carrot cake chocolate. Chocolate cake fruitcake sugar plum biscuit donut cupcake sweet muffin. Chupa chups icing muffin wafer sesame snaps sesame snaps. Jelly beans wafer chocolate cupcake chocolate bar chocolate muffin topping sesame snaps. Cake cupcake jelly-o marshmallow cookie. Macaroon powder macaroon muffin lollipop shortbread marzipan.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Smartfon Honor X9a 6GB/128GB Green",
                ProductPrice = 1360,
                ProductImageName = "Smartfon Honor X9a 6GB,128GB Green.jpg",
                ProductDescription = "Muffin sesame snaps pastry donut tootsie roll. Shortbread jelly beans jelly beans chupa chups halvah macaroon liquorice. Chupa chups lollipop cake sweet roll marshmallow wafer. Danish gummi bears topping candy bear claw biscuit wafer sweet. Muffin sesame snaps pastry donut tootsie roll. Shortbread jelly beans jelly beans chupa chups halvah macaroon liquorice. Chupa chups lollipop cake sweet roll marshmallow wafer. Danish gummi bears topping candy bear claw biscuit wafer sweet. Cupcake donut macaroon tootsie roll candy liquorice. Marshmallow chupa chups jelly bear claw gummies. Tiramisu fruitcake tiramisu candy powder halvah sesame snaps marzipan. Halvah soufflé chocolate marzipan cake ice cream biscuit. Wafer soufflé cake sweet jelly-o gingerbread bonbon dessert gummi bears. Powder macaroon sugar plum icing topping sugar plum sugar plum jujubes. Croissant chocolate apple pie dessert chocolate cake. Pudding chocolate bar lemon drops cotton candy carrot cake sweet chocolate. Cookie sesame snaps chocolate bar lemon drops jelly jelly beans. Shortbread sweet cupcake chocolate cake caramels marzipan cake. Cupcake donut macaroon tootsie roll candy liquorice. Marshmallow chupa chups jelly bear claw gummies. Tiramisu fruitcake tiramisu candy powder halvah sesame snaps marzipan. Halvah soufflé chocolate marzipan cake ice cream biscuit. Wafer soufflé cake sweet jelly-o gingerbread bonbon dessert gummi bears. Powder macaroon sugar plum icing topping sugar plum sugar plum jujubes. Croissant chocolate apple pie dessert chocolate cake. Pudding chocolate bar lemon drops cotton candy carrot cake sweet chocolate. Cookie sesame snaps chocolate bar lemon drops jelly jelly beans. Shortbread sweet cupcake chocolate cake caramels marzipan cake.",
                ProductIsApproved = true,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Smartfon Xiaomi Redmi 10A 2GB/32GB Sky Blue",
                ProductPrice = 7300,
                ProductImageName = "Smartfon Xiaomi Redmi 10A 2GB,32GB Sky Blue.jpg",
                ProductDescription = "Rollipop croissant soufflé cookie jelly-o cotton candy carrot cake pudding. Gummi bears pudding sweet roll bear claw cupcake oat cake cake carrot cake caramels. Cupcake lollipop croissant soufflé cookie jelly-o cotton candy carrot cake pudding. Gummi bears pudding sweet roll bear claw cupcake oat cake cake carrot cake caramels. Ice cream gingerbread tiramisu pudding cupcake chupa chups. Liquorice halvah chupa chups lollipop carrot cake chocolate. Wafer bear claw powder muffin wafer candy candy canes. Gummies candy canes dessert sweet roll tart caramels. Chocolate cake gingerbread pastry carrot cake brownie pudding. Toffee sweet lemon drops wafer topping cake shortbread. Donut jelly gummies brownie icing candy toffee candy. Fruitcake carrot cake gingerbread carrot cake cheesecake jelly-o chupa chups chocolate lemon drops. Sesame snaps marzipan wafer sweet jujubes. Apple pie icing jelly beans jelly oat cake gummi bears chocolate sugar plum. Ice cream gingerbread tiramisu pudding cupcake chupa chups. Liquorice halvah chupa chups lollipop carrot cake chocolate. Wafer bear claw powder muffin wafer candy candy canes. Gummies candy canes dessert sweet roll tart caramels. Chocolate cake gingerbread pastry carrot cake brownie pudding. Toffee sweet lemon drops wafer topping cake shortbread. Donut jelly gummies brownie icing candy toffee candy. Fruitcake carrot cake gingerbread carrot cake cheesecake jelly-o chupa chups chocolate lemon drops. Sesame snaps marzipan wafer sweet jujubes. Apple pie icing jelly beans jelly oat cake gummi bears chocolate sugar plum.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Smartfon Xiaomi Redmi 10C 4GB/128GB Graphite Gray",
                ProductPrice = 6490,
                ProductImageName = "Smartfon Xiaomi Redmi 10C 4GB,128GB Graphite Gray.jpg",
                ProductDescription = "Sweet roll carrot cake bonbon lollipop lollipop marshmallow. Powder tiramisu cake chocolate cake caramels jelly beans muffin. Liquorice carrot cake halvah cotton candy marzipan ice cream danish. Sweet brownie topping chocolate cake cookie cake. Cookie gingerbread bonbon topping ice cream chupa chups. Sweet carrot cake lemon drops dragée cotton candy. Soufflé macaroon caramels croissant cotton candy sweet roll. Gummies jelly-o topping tiramisu croissant pudding shortbread wafer. Sweet roll carrot cake bonbon lollipop lollipop marshmallow. Powder tiramisu cake chocolate cake caramels jelly beans muffin. Liquorice carrot cake halvah cotton candy marzipan ice cream danish. Sweet brownie topping chocolate cake cookie cake. Cookie gingerbread bonbon topping ice cream chupa chups. Sweet carrot cake lemon drops dragée cotton candy. Soufflé macaroon caramels croissant cotton candy sweet roll. Gummies jelly-o topping tiramisu croissant pudding shortbread wafer. Sweet roll brownie caramels bonbon gingerbread pie. Croissant chocolate sweet roll gummies macaroon. Fruitcake apple pie dragée icing candy canes chocolate cake lemon drops jujubes toffee. Cake tiramisu cupcake pudding biscuit dragée toffee bear claw chocolate. Jujubes cotton candy gummies tart toffee toffee. Dessert topping toffee dragée pastry. Sweet roll brownie caramels bonbon gingerbread pie. Croissant chocolate sweet roll gummies macaroon. Fruitcake apple pie dragée icing candy canes chocolate cake lemon drops jujubes toffee. Cake tiramisu cupcake pudding biscuit dragée toffee bear claw chocolate. Jujubes cotton candy gummies tart toffee toffee. Dessert topping toffee dragée pastry.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Smartfon Xiaomi Redmi A1 Plus 2GB/32GB Black",
                ProductPrice = 3630,
                ProductImageName = "Smartfon Xiaomi Redmi A1 Plus 2GB,32GB Black.jpg",
                ProductDescription = "Muffin dessert chocolate bar tart cake. Liquorice croissant danish dessert tart brownie pastry cake brownie. Marzipan lollipop croissant chocolate bar toffee. Lollipop jelly cupcake wafer cookie oat cake. Chocolate gummi bears chocolate cake lemon drops fruitcake. Tootsie roll carrot cake liquorice chocolate bar tiramisu. Jelly jelly-o shortbread tart candy tart. Icing cupcake marzipan danish chocolate bear claw macaroon. Jelly liquorice bonbon candy canes bear claw gummi bears muffin fruitcake marzipan. Powder jujubes lemon drops jujubes icing. Jelly-o ice cream liquorice cake fruitcake wafer jujubes. Sugar plum cookie dessert topping chocolate lollipop. Cotton candy ice cream chocolate croissant sweet tootsie roll marshmallow candy sweet roll. Muffin dessert chocolate bar tart cake. Liquorice croissant danish dessert tart brownie pastry cake brownie. Marzipan lollipop croissant chocolate bar toffee. Lollipop jelly cupcake wafer cookie oat cake. Chocolate gummi bears chocolate cake lemon drops fruitcake. Tootsie roll carrot cake liquorice chocolate bar tiramisu. Jelly jelly-o shortbread tart candy tart. Icing cupcake marzipan danish chocolate bear claw macaroon. Jelly liquorice bonbon candy canes bear claw gummi bears muffin fruitcake marzipan. Powder jujubes lemon drops jujubes icing. Jelly-o ice cream liquorice cake fruitcake wafer jujubes. Sugar plum cookie dessert topping chocolate lollipop. Cotton candy ice cream chocolate croissant sweet tootsie roll marshmallow candy sweet roll. Muffin dessert chocolate bar tart cake. Liquorice croissant danish dessert tart brownie pastry cake brownie. Marzipan lollipop croissant chocolate bar toffee. Lollipop jelly cupcake wafer cookie oat cake. Chocolate gummi bears chocolate cake lemon drops fruitcake. Tootsie roll carrot cake liquorice chocolate bar tiramisu. Jelly jelly-o shortbread tart candy tart. Icing cupcake marzipan danish chocolate bear claw macaroon. Jelly liquorice bonbon candy canes bear claw gummi bears muffin fruitcake marzipan. Powder jujubes lemon drops jujubes icing. Jelly-o ice cream liquorice cake fruitcake wafer jujubes. Sugar plum cookie dessert topping chocolate lollipop. Cotton candy ice cream chocolate croissant sweet tootsie roll marshmallow candy sweet roll.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Smartfon Xiaomi Redmi Note 10 Pro 8 GB/128GB Glacier Blue",
                ProductPrice = 1340,
                ProductImageName = "Smartfon Xiaomi Redmi Note 10 Pro 8 GB,128GB Glacier Blue.jpg",
                ProductDescription = "Tootsie roll lemon drops caramels candy canes icing. Apple pie jelly cake cupcake fruitcake croissant lemon drops pastry cake. Pudding liquorice pastry bear claw biscuit pie toffee. Chocolate cake candy brownie liquorice macaroon liquorice chocolate. Halvah candy canes sweet halvah apple pie tart. Wafer tootsie roll donut cupcake toffee donut. Dragée marzipan tart lemon drops sweet sweet caramels. Carrot cake cheesecake chocolate pudding pie brownie icing. Shortbread wafer sugar plum carrot cake donut donut powder wafer carrot cake. Pie bear claw cheesecake bonbon cotton candy pie pastry cupcake. Sugar plum pie liquorice cake jelly beans ice cream toffee cupcake macaroon. Donut jelly danish oat cake powder lollipop candy canes. Tootsie roll lemon drops caramels candy canes icing. Apple pie jelly cake cupcake fruitcake croissant lemon drops pastry cake. Pudding liquorice pastry bear claw biscuit pie toffee. Chocolate cake candy brownie liquorice macaroon liquorice chocolate. Halvah candy canes sweet halvah apple pie tart. Wafer tootsie roll donut cupcake toffee donut. Dragée marzipan tart lemon drops sweet sweet caramels. Carrot cake cheesecake chocolate pudding pie brownie icing. Shortbread wafer sugar plum carrot cake donut donut powder wafer carrot cake. Pie bear claw cheesecake bonbon cotton candy pie pastry cupcake. Sugar plum pie liquorice cake jelly beans ice cream toffee cupcake macaroon. Donut jelly danish oat cake powder lollipop candy canes. Tootsie roll lemon drops caramels candy canes icing. Apple pie jelly cake cupcake fruitcake croissant lemon drops pastry cake. Pudding liquorice pastry bear claw biscuit pie toffee. Chocolate cake candy brownie liquorice macaroon liquorice chocolate. Halvah candy canes sweet halvah apple pie tart. Wafer tootsie roll donut cupcake toffee donut. Dragée marzipan tart lemon drops sweet sweet caramels. Carrot cake cheesecake chocolate pudding pie brownie icing. Shortbread wafer sugar plum carrot cake donut donut powder wafer carrot cake. Pie bear claw cheesecake bonbon cotton candy pie pastry cupcake. Sugar plum pie liquorice cake jelly beans ice cream toffee cupcake macaroon. Donut jelly danish oat cake powder lollipop candy canes.",
                ProductIsApproved = true,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Smartfon Xiaomi Redmi Note 11s 6GB/128GB Graphite Gray",
                ProductPrice = 2270,
                ProductImageName = "Smartfon Xiaomi Redmi Note 11s 6GB,128GB Graphite Gray.jpg",
                ProductDescription = "Cake cake pie gummies jujubes marshmallow shortbread bear claw pastry. Pastry tootsie roll dessert gummi bears bear claw pastry. Donut fruitcake apple pie soufflé cheesecake candy. Halvah lemon drops ice cream marshmallow cheesecake jelly beans. Jelly caramels chupa chups dessert bear claw sweet bonbon chocolate cake cupcake. Wafer sweet chocolate candy sugar plum. Chocolate bar sweet roll shortbread chupa chups chocolate bar cookie bonbon macaroon. Tiramisu dragée icing sugar plum liquorice sweet. Fruitcake wafer jujubes cupcake chocolate. Donut apple pie croissant marshmallow cupcake tiramisu candy canes macaroon. Dessert pastry dragée jelly biscuit carrot cake candy. Croissant pastry lemon drops pastry gummi bears gingerbread cotton candy. Cake tootsie roll liquorice apple pie bear claw cotton candy gummi bears. Cake cake pie gummies jujubes marshmallow shortbread bear claw pastry. Pastry tootsie roll dessert gummi bears bear claw pastry. Donut fruitcake apple pie soufflé cheesecake candy. Halvah lemon drops ice cream marshmallow cheesecake jelly beans. Jelly caramels chupa chups dessert bear claw sweet bonbon chocolate cake cupcake. Wafer sweet chocolate candy sugar plum. Chocolate bar sweet roll shortbread chupa chups chocolate bar cookie bonbon macaroon. Tiramisu dragée icing sugar plum liquorice sweet. Fruitcake wafer jujubes cupcake chocolate. Donut apple pie croissant marshmallow cupcake tiramisu candy canes macaroon. Dessert pastry dragée jelly biscuit carrot cake candy. Croissant pastry lemon drops pastry gummi bears gingerbread cotton candy. Cake tootsie roll liquorice apple pie bear claw cotton candy gummi bears. Cake cake pie gummies jujubes marshmallow shortbread bear claw pastry. Pastry tootsie roll dessert gummi bears bear claw pastry. Donut fruitcake apple pie soufflé cheesecake candy. Halvah lemon drops ice cream marshmallow cheesecake jelly beans. Jelly caramels chupa chups dessert bear claw sweet bonbon chocolate cake cupcake. Wafer sweet chocolate candy sugar plum. Chocolate bar sweet roll shortbread chupa chups chocolate bar cookie bonbon macaroon. Tiramisu dragée icing sugar plum liquorice sweet. Fruitcake wafer jujubes cupcake chocolate. Donut apple pie croissant marshmallow cupcake tiramisu candy canes macaroon. Dessert pastry dragée jelly biscuit carrot cake candy. Croissant pastry lemon drops pastry gummi bears gingerbread cotton candy.",
                ProductIsApproved = false,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Smartfon Xiaomi Redmi Note 11s 6GB/128GB Twilight Blue",
                ProductPrice = 3260,
                ProductImageName = "Smartfon Xiaomi Redmi Note 11s 6GB,128GB Twilight Blue.jpg",
                ProductDescription = "Jelly beans chocolate tiramisu candy canes donut gummies gummies. Candy shortbread cake lemon drops chocolate cake apple pie croissant toffee icing. Lemon drops candy pie sweet roll candy toffee wafer jujubes fruitcake. Tiramisu sweet roll bear claw cotton candy oat cake tiramisu pie chocolate. Lollipop bonbon gummies cake halvah. Apple pie jujubes muffin candy dragée candy pastry. Powder pudding macaroon marshmallow cupcake tart cheesecake chocolate. Caramels cotton candy wafer dragée jelly beans wafer powder. Candy bear claw toffee sugar plum pastry icing cookie soufflé. Liquorice carrot cake icing cheesecake toffee. Tootsie roll bonbon carrot cake lemon drops dessert. Marshmallow apple pie cupcake biscuit halvah sesame snaps fruitcake. Brownie bear claw chocolate cake soufflé jelly beans. Cookie cake cheesecake tootsie roll tiramisu pudding. Jelly beans chocolate tiramisu candy canes donut gummies gummies. Candy shortbread cake lemon drops chocolate cake apple pie croissant toffee icing. Lemon drops candy pie sweet roll candy toffee wafer jujubes fruitcake. Tiramisu sweet roll bear claw cotton candy oat cake tiramisu pie chocolate. Lollipop bonbon gummies cake halvah. Apple pie jujubes muffin candy dragée candy pastry. Powder pudding macaroon marshmallow cupcake tart cheesecake chocolate. Caramels cotton candy wafer dragée jelly beans wafer powder. Candy bear claw toffee sugar plum pastry icing cookie soufflé. Liquorice carrot cake icing cheesecake toffee. Tootsie roll bonbon carrot cake lemon drops dessert. Marshmallow apple pie cupcake biscuit halvah sesame snaps fruitcake. Brownie bear claw chocolate cake soufflé jelly beans. Cookie cake cheesecake tootsie roll tiramisu pudding.",
                ProductIsApproved = true,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Smartfon Samsung Galaxy A13 3GB/32GB Black",
                ProductPrice = 6310,
                ProductImageName = "Smartfon Samsung Galaxy A13 3GB,32GB Black.jpg",
                ProductDescription = "Chocolate ice cream cookie lemon drops liquorice cookie topping pie. Cake donut marzipan halvah gummi bears. Sugar plum tiramisu wafer carrot cake chocolate chocolate cake. Pudding powder chocolate cake sweet roll gingerbread sweet chupa chups apple pie. Cotton candy bonbon gingerbread cookie bear claw. Bear claw toffee macaroon cheesecake chocolate icing cupcake pastry. Cake marshmallow cookie gingerbread wafer candy chupa chups sweet. Sweet tiramisu danish chupa chups gummies. Donut sweet roll pastry gummi bears caramels icing croissant toffee. Tart ice cream cupcake cake candy canes marshmallow chocolate bar pie sesame snaps. Dragée gummies chupa chups gummi bears chocolate bar jelly beans dragée cake. Ice cream lemon drops croissant gingerbread macaroon jelly beans. Chocolate ice cream cookie lemon drops liquorice cookie topping pie. Cake donut marzipan halvah gummi bears. Sugar plum tiramisu wafer carrot cake chocolate chocolate cake. Pudding powder chocolate cake sweet roll gingerbread sweet chupa chups apple pie. Cotton candy bonbon gingerbread cookie bear claw. Bear claw toffee macaroon cheesecake chocolate icing cupcake pastry. Cake marshmallow cookie gingerbread wafer candy chupa chups sweet. Sweet tiramisu danish chupa chups gummies. Donut sweet roll pastry gummi bears caramels icing croissant toffee. Tart ice cream cupcake cake candy canes marshmallow chocolate bar pie sesame snaps. Dragée gummies chupa chups gummi bears chocolate bar jelly beans dragée cake. Ice cream lemon drops croissant gingerbread macaroon jelly beans. Chocolate ice cream cookie lemon drops liquorice cookie topping pie. Cake donut marzipan halvah gummi bears. Sugar plum tiramisu wafer carrot cake chocolate chocolate cake. Pudding powder chocolate cake sweet roll gingerbread sweet chupa chups apple pie. Cotton candy bonbon gingerbread cookie bear claw. Bear claw toffee macaroon cheesecake chocolate icing cupcake pastry. Cake marshmallow cookie gingerbread wafer candy chupa chups sweet. Sweet tiramisu danish chupa chups gummies. Donut sweet roll pastry gummi bears caramels icing croissant toffee. Tart ice cream cupcake cake candy canes marshmallow chocolate bar pie sesame snaps. Dragée gummies chupa chups gummi bears chocolate bar jelly beans dragée cake. Ice cream lemon drops croissant gingerbread macaroon jelly beans.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Smartfon Samsung Galaxy A13 4GB/64GB Blue",
                ProductPrice = 1350,
                ProductImageName = "Smartfon Samsung Galaxy A13 4GB,64GB Blue.jpg",
                ProductDescription = "Cotton candy croissant brownie croissant pudding pie fruitcake. Marzipan dragée bear claw donut gummies sesame snaps bear claw. Jelly pudding sesame snaps croissant fruitcake gummi bears jujubes gummies cotton candy. Jelly tootsie roll cookie candy canes biscuit chocolate cake. Jelly-o tootsie roll gummi bears lemon drops sugar plum soufflé. Cookie shortbread carrot cake lollipop brownie jelly-o bear claw sweet. Chocolate cake marzipan dragée cheesecake fruitcake bear claw jelly-o. Cookie cake liquorice chocolate cake cake. Dragée sesame snaps chupa chups tootsie roll cotton candy. Pastry cake cake tootsie roll sweet gummi bears. Jujubes halvah marshmallow cupcake pie shortbread oat cake croissant. Sweet roll sesame snaps chupa chups sweet roll toffee. Chupa chups tart halvah pie pie sweet roll soufflé. Bonbon cupcake biscuit sweet sugar plum pastry. Cotton candy croissant brownie croissant pudding pie fruitcake. Marzipan dragée bear claw donut gummies sesame snaps bear claw. Jelly pudding sesame snaps croissant fruitcake gummi bears jujubes gummies cotton candy. Jelly tootsie roll cookie candy canes biscuit chocolate cake. Jelly-o tootsie roll gummi bears lemon drops sugar plum soufflé. Cookie shortbread carrot cake lollipop brownie jelly-o bear claw sweet. Chocolate cake marzipan dragée cheesecake fruitcake bear claw jelly-o. Cookie cake liquorice chocolate cake cake. Dragée sesame snaps chupa chups tootsie roll cotton candy. Pastry cake cake tootsie roll sweet gummi bears. Jujubes halvah marshmallow cupcake pie shortbread oat cake croissant. Sweet roll sesame snaps chupa chups sweet roll toffee. Chupa chups tart halvah pie pie sweet roll soufflé. Bonbon cupcake biscuit sweet sugar plum pastry.",
                ProductIsApproved = true,
                ProductIsHome = true
            },
            new Product()
            {
                ProductName = "Smartfon Samsung Galaxy A53 5G 8GB/256GB Black",
                ProductPrice = 4310,
                ProductImageName = "Smartfon Samsung Galaxy A53 5G 8GB,256GB Black.jpg",
                ProductDescription = "Lollipop halvah lemon drops gingerbread soufflé liquorice tootsie roll tiramisu pastry. Jelly tiramisu dragée gummi bears pudding jelly jelly-o sweet roll. Toffee candy canes bear claw candy oat cake danish jelly beans. Jujubes pudding croissant soufflé jelly-o powder wafer. Chocolate jelly-o dessert tart pie apple pie chocolate. Jujubes caramels tart cake liquorice. Cheesecake soufflé sugar plum muffin chocolate lemon drops soufflé. Wafer fruitcake powder marshmallow dessert brownie candy jelly candy. Bonbon liquorice candy lollipop lemon drops tootsie roll cotton candy. Chocolate bar jelly-o gingerbread caramels tart. Dragée caramels halvah lollipop topping cookie. Apple pie liquorice liquorice wafer sweet roll pudding. Tootsie roll soufflé bonbon muffin soufflé sweet donut. Lollipop halvah lemon drops gingerbread soufflé liquorice tootsie roll tiramisu pastry. Jelly tiramisu dragée gummi bears pudding jelly jelly-o sweet roll. Toffee candy canes bear claw candy oat cake danish jelly beans. Jujubes pudding croissant soufflé jelly-o powder wafer. Chocolate jelly-o dessert tart pie apple pie chocolate. Jujubes caramels tart cake liquorice. Cheesecake soufflé sugar plum muffin chocolate lemon drops soufflé. Wafer fruitcake powder marshmallow dessert brownie candy jelly candy. Bonbon liquorice candy lollipop lemon drops tootsie roll cotton candy. Chocolate bar jelly-o gingerbread caramels tart. Dragée caramels halvah lollipop topping cookie. Apple pie liquorice liquorice wafer sweet roll pudding. Tootsie roll soufflé bonbon muffin soufflé sweet donut. Pudding lollipop carrot cake cheesecake toffee dessert bonbon pastry. Gummi bears shortbread toffee gummi bears dessert. Tootsie roll oat cake icing ice cream cake carrot cake. Caramels jelly-o gingerbread jelly gummies sugar plum. Shortbread cotton candy dessert toffee gummi bears. Jujubes lemon drops sweet roll cupcake soufflé candy jelly beans. Sweet apple pie fruitcake jujubes chocolate icing. Fruitcake soufflé jelly tiramisu chocolate bar liquorice cheesecake. Icing cake carrot cake chupa chups lollipop danish brownie.",
                ProductIsApproved = false,
                ProductIsHome = false
            },
            new Product()
            {
                ProductName = "Smartfon Samsung Galaxy A73 8GB/256GB Gray",
                ProductPrice = 6760,
                ProductImageName = "Smartfon Samsung Galaxy A73 8GB,256GB Gray.jpg",
                ProductDescription = "Pudding lollipop carrot cake cheesecake toffee dessert bonbon pastry. Gummi bears shortbread toffee gummi bears dessert. Tootsie roll oat cake icing ice cream cake carrot cake. Caramels jelly-o gingerbread jelly gummies sugar plum. Shortbread cotton candy dessert toffee gummi bears. Jujubes lemon drops sweet roll cupcake soufflé candy jelly beans. Sweet apple pie fruitcake jujubes chocolate icing. Fruitcake soufflé jelly tiramisu chocolate bar liquorice cheesecake. Icing cake carrot cake chupa chups lollipop danish brownie. Tiramisu sweet roll tootsie roll soufflé danish bonbon pie. Pie carrot cake cookie brownie caramels fruitcake bear claw cupcake icing. Marzipan marshmallow marzipan soufflé tootsie roll powder liquorice bear claw chupa chups. Pie cotton candy pastry tiramisu gummies croissant. Pudding lollipop carrot cake cheesecake toffee dessert bonbon pastry. Gummi bears shortbread toffee gummi bears dessert. Tootsie roll oat cake icing ice cream cake carrot cake. Caramels jelly-o gingerbread jelly gummies sugar plum. Shortbread cotton candy dessert toffee gummi bears. Jujubes lemon drops sweet roll cupcake soufflé candy jelly beans. Sweet apple pie fruitcake jujubes chocolate icing. Fruitcake soufflé jelly tiramisu chocolate bar liquorice cheesecake. Icing cake carrot cake chupa chups lollipop danish brownie. Tiramisu sweet roll tootsie roll soufflé danish bonbon pie. Pie carrot cake cookie brownie caramels fruitcake bear claw cupcake icing. Marzipan marshmallow marzipan soufflé tootsie roll powder liquorice bear claw chupa chups. Pie cotton candy pastry tiramisu gummies croissant. Pudding lollipop carrot cake cheesecake toffee dessert bonbon pastry. Gummi bears shortbread toffee gummi bears dessert. Tootsie roll oat cake icing ice cream cake carrot cake. Caramels jelly-o gingerbread jelly gummies sugar plum. Shortbread cotton candy dessert toffee gummi bears. Jujubes lemon drops sweet roll cupcake soufflé candy jelly beans. Sweet apple pie fruitcake jujubes chocolate icing. Fruitcake soufflé jelly tiramisu chocolate bar liquorice cheesecake. Icing cake carrot cake chupa chups lollipop danish brownie. Tiramisu sweet roll tootsie roll soufflé danish bonbon pie. Pie carrot cake cookie brownie caramels fruitcake bear claw cupcake icing. Marzipan marshmallow marzipan soufflé tootsie roll powder liquorice bear claw chupa chups. Pie cotton candy pastry tiramisu gummies croissant.",
                ProductIsApproved = true,
                ProductIsHome = true
            }
            #endregion Telefonlar

            /* = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = + = */
        };

        private readonly static Category[] CategoryArray =
        {
            new Category() { CategoryName = "Noutbuklar" },      // id = 0
            new Category() { CategoryName = "Saatlar" },         // id = 1
            new Category() { CategoryName = "Mobil telefonlar" } // id = 2
        };

        private readonly static ProductCategory[] ProductCategoryArray =
        {
            #region Noutbuklar
            new ProductCategory() {Product = ProductArray[0],  Category = CategoryArray[0]}, /* 0 ID-li Product olsun 0 ID-li Category-de */
            new ProductCategory() {Product = ProductArray[1],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[2],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[3],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[4],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[5],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[6],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[7],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[8],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[9],  Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[10], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[11], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[12], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[13], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[14], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[15], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[16], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[17], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[18], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[19], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[20], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[21], Category = CategoryArray[0]},
            new ProductCategory() {Product = ProductArray[22], Category = CategoryArray[0]},
            #endregion Noutbuklar

            #region Saatlar
            new ProductCategory() {Product = ProductArray[23], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[24], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[25], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[26], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[27], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[28], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[29], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[30], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[31], Category = CategoryArray[1]},
            new ProductCategory() {Product = ProductArray[32], Category = CategoryArray[1]},
            #endregion Saatlar

            #region Telefonlar
            new ProductCategory() {Product = ProductArray[33], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[34], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[35], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[36], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[37], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[38], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[39], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[40], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[41], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[42], Category = CategoryArray[2]},
            new ProductCategory() {Product = ProductArray[43], Category = CategoryArray[2]}
            #endregion Telefonlar
        };
        #endregion Seed edeceyim datalari saxlayan massivler.
    }
}