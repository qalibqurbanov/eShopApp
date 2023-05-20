using AutoMapper;
using eShopApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using eShopApp.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using eShopApp.WebUI.Identity.Enums;
using eShopApp.WebUI.Identity.Entities;
using eShopApp.WebUI.Models.ViewModels;
using eShopApp.WebUI.Extensions.Helpers;
using Microsoft.AspNetCore.Authorization;
using eShopApp.Business.Services.Abstract;
using eShopApp.WebUI.Extensions.Serialization;
using eShopApp.WebUI.Models.IdentityModels.Role;
using eShopApp.WebUI.Models.IdentityModels.User;

namespace eShopApp.WebUI.Controllers
{
    [Authorize(Roles = nameof(IdentityRoles.Admin))]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AdminController(
            IProductService productService, 
            ICategoryService categoryService, 
            IWebHostEnvironment environment, 
            IMapper mapper,
            RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._environment = environment;
            this._mapper = mapper;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        #region Admin icazesi teleb olunan reallawdirilmiw emeliyyat barede usere sehifenin ust hissesinde gostereceyimiz mesaji set eden komekci mini metod.
        private void CreateInformationalMessage(string AlertMessage, AlertMessage.AlertType AlertType)
        {
            TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
            {
                alertMessage = AlertMessage,
                alertType = AlertType
            });
        }
        #endregion Admin icazesi teleb olunan reallawdirilmiw emeliyyat barede usere sehifenin ust hissesinde gostereceyimiz mesaji set eden komekci mini metod.

        #region Product Management
        [HttpGet]
        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            /* User mehsul yaratma sehifesine girende gerek DB-dan kateqoriyalari elde ederek View-ya gonderim, cunki hemin kateqoriyalar esasinda checkboxlar yaradacam. Burada yaradacagim Checkboxlar ucun hansi kateqoriyalari iwledeceyimi set edirem, yeni burada checkboxlari hansi kateqoriyalar esasinda yaradacagimi paketleyib gonderirem 'CreateProduct' sehifesine. Qisaca, DB-daki butun kateqoriyalari View-ya dawiyacam ve hemin bu kateqoriyalari View-da checkbox olaraq yaradacam (isteseydim 'ViewBag.Categories'-i 'ProductViewModel' sinifi icerisinde bir kolleksiya olaraq yarada ve o curde dawiya bilerdim): */
            ViewData["Categories"] = _categoryService.GetAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct([FromForm] ProductViewModel productVM, [FromForm] int[] catIDs, [FromForm] IFormFile imageFile)
        {
            if(ModelState.IsValid)
            {
                Product product = _mapper.Map<Product>(productVM);
                if (imageFile != null)
                {
                    /* Faylin ozunu yerlewdirirem 'Web Root (wwwroot)' papkasina: */
                    imageFile.MoveFormFile(_environment.WebRootPath, out string imageName);

                    /* Daha sonra ise hemin faylin adini qeyd edirem DB-ya: */
                    product.ProductImageName = imageName;
                }

                bool resultOfProductCreateOperation = _productService.Create(product, catIDs);

                if(resultOfProductCreateOperation == true)
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"You have successfully created a new product with name: \"{product.ProductName}\"!",
                        AlertType: AlertMessage.AlertType.success
                    );

                    return RedirectToAction(nameof(ProductList)); /* Yeni bir mehsul elave olunduqdan sonra admin qalmasin mehsul elave etme sehifesinde, onun yerine admini yonlendirek mehsul siyahisi sehifesine */
                }
                else
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"{_productService.ErrorMessage}",
                        AlertType: AlertMessage.AlertType.danger
                    );

                    /* Modelin propertyleri biznes qatinda 'IValidator' komeyile tetbiq etdiyim validasiyadan kece bilmese useri gonderirem mehsul yaratma sehifesine: */
                    ViewData["Categories"] = _categoryService.GetAll();
                    return View(productVM);
                }
            }
            else
            {
                /* Eger inputplara/modele gelen data validasiyadan kece bilmese demeli xeta var ve bu zaman useri qaytariram mehsul yaratma sehifesine ve hemin sehifede hemde DB-dan kateqoriyalari elde ederek hemin kateqoriyalarin her biri ucun Checkbox yaradiram. Burada yaradacagim Checkboxlar ucun hansi kateqoriyalari iwledeceyimi set edirem, yeni burada checkboxlari hansi kateqoriyalar esasinda yaradacagimi paketleyib gonderirem 'CreateProduct' sehifesine. Qisaca, DB-daki butun kateqoriyalari View-ya dawiyacam ve hemin bu kateqoriyalari View-da checkbox olaraq yaradacam (isteseydim 'ViewBag.Categories'-i 'ProductViewModel' sinifi icerisinde bir kolleksiya olaraq yarada ve o curde dawiya bilerdim): */
                ViewData["Categories"] = _categoryService.GetAll();

                /* Eger inputplara/modele gelen data validasiyadan kece bilmese demeli xeta var ve bu zaman useri qaytariram mehsul yaratma sehifesine ve xetali daxil etmiw oldugu modelide gonderirem hemin sehifeye. Sehifede ise inputlar bu modele 'asp-for' ile bindlenmiw oldugu ucun (ve belece 'asp-for' 'id,name ve s.'-dan bawqa input ucun 'value=@Model.PropertyName' atributunuda generasiya edecek deye) problemsiz bir wekilde uygun inputlarda userin daxil etmiw oldugu melumatlar(productVM) yazilacaq ve xetali inputlarin yanindada ('asp-validation-for' sayesinde) uygun xeta mesaji: */
                return View(productVM);
            }
        }

        [HttpGet]
        public IActionResult EditProduct([FromRoute] int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = _productService.GetByIdWithCategories((int)id);

            if(product == null)
            {
                return NotFound();
            }

            ProductViewModel productVM = _mapper.Map<ProductViewModel>(product);

            /* ViewModel icerisine mehsulun aid oldugu kateqoriyalarida yerlewdirirem */
            productVM.SelectedCategories = product.ProductCategories.Select(prod => prod.Category).ToList();

            /* DB-daki butun kateqoriyalari View-ya dawiyacam ve hemin bu kateqoriyalari checkbox olaraq yaradacam (isteseydim 'ProductViewModel' icerisinde bir kolleksiya olaraq yarada ve o curde dawiya bilerdim): */
            ViewData["Categories"] = _categoryService.GetAll();

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct([FromForm]ProductViewModel productVM, [FromForm]int[] catIDs, [FromForm] IFormFile imageFile)
            /* 'catIDs' - yaranacaq olan 'Switch' kontrollarinin 'name'-i esasinda yaxalanacaq 'value'-lari */
            /* 'imageFile' - form-dan post olunan wekli yaxalayacaq, diqqet etmek lazimdir ki: "input type="file" :olan inputun 'name'-i ile buradaki parametrin adi eyni olsun (ki Model Binding dogru iwlesin) */
        {
            if (ModelState.IsValid)
            {
                Product product = _productService.GetByID(productVM.ProductID);

                if (product == null)
                {
                    return NotFound();
                }

                product = _mapper.Map<Product>(productVM);
                if (imageFile != null)
                {
                    /* Faylin ozunu yerlewdirirem 'Web Root (wwwroot)' papkasina: */
                    imageFile.MoveFormFile(_environment.WebRootPath, out string imageName);

                    /* Daha sonra ise hemin faylin adini qeyd edirem DB-ya: */
                    product.ProductImageName = imageName;
                }

                bool resultOfProductUpdateOperation = _productService.Update(product, catIDs);

                if (resultOfProductUpdateOperation == true)
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"You have successfully edited a product with name: \"{product.ProductName}\"!",
                        AlertType: AlertMessage.AlertType.info
                    );

                    return RedirectToAction(nameof(ProductList)); /* Mehsul editlendikden sonra admin qalmasin mehsul editleme sehifesinde, onun yerine admini yonlendirek mehsul siyahisi sehifesine */
                }
                else
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"{_productService.ErrorMessage}",
                        AlertType: AlertMessage.AlertType.danger
                    );

                    /* Modelin propertyleri biznes qatinda 'IValidator' komeyile tetbiq etdiyim validasiyadan kece bilmese useri gonderirem mehsul yaratma sehifesine: */
                    return View(productVM);
                }
            }
            else
            {
                /* Eger inputplara/modele gelen data validasiyadan kece bilmese demeli xeta var ve bu zaman useri qaytariram mehsul editleme sehifesine ve xetali daxil etmiw oldugu modelide gonderirem hemin sehifeye. Sehifede ise inputlar bu modele 'asp-for' ile bindlenmiw oldugu ucun (ve belece 'asp-for' 'id,name ve s.'-dan bawqa input ucun 'value=@Model.PropertyName' atributunuda generasiya edecek deye) problemsiz bir wekilde uygun inputlarda userin daxil etmiw oldugu melumatlar(productVM) yazilacaq ve xetali inputlarin yanindada ('asp-validation-for' sayesinde) uygun xeta mesaji: */
                return View(productVM);
            }
            
        }

        [HttpPost]
        public IActionResult DeleteProduct([FromForm] int prodID)
        /* Mehsul siyahisi sehifesinde product-un 'Delete'-ne basilan kimi 'name'-i 'prodID' olan hidden inputun 'value'-sunu POST edirik 'DeleteProduct'-a ve burada eyni adli parametr komeyile yaxalayiriq. */
        {
            Product product = _productService.GetByID(prodID);

            if (product != null)
            {
                _productService.Delete(product);

                CreateInformationalMessage
                (
                    AlertMessage: $"You have successfully deleted a product with name: \"{product.ProductName}\"!",
                    AlertType: AlertMessage.AlertType.danger
                );

                return RedirectToAction(nameof(ProductList)); /* Mehsul silindikden sonra tezeden Redirect edirik admini hazirda oldugu 'ProductList' sehifesine, yeni bir baxima sehifeni refresh etdirmiw olduq */
            }
            else
            {
                CreateInformationalMessage
                (
                    AlertMessage: "The data you are trying to delete was not found in our database!",
                    AlertType: AlertMessage.AlertType.danger
                );

                return RedirectToAction(nameof(ProductList)); /* Silinmeli mehsul tapilmadisa admini tezeden Redirect edirik hazirda oldugu 'ProductList' sehifesine, yeni bir baxima sehifeni refresh etdirmiw olduq */
            }
        }

        [HttpPost] /* 'EditCategory' sehifesinde hazirda editlemeye caliwdigim kateqoriyaya("~/admin/category/{id}") aid olan her hansi bir mehsulun 'Delete'-ne basilsa iwleyecek: */
        public IActionResult DeleteProductFromCategory([FromForm] int prodID, [FromForm] int catID) /* 'EditCategory' sehifesindeki hidden inputlardan yaxalanacaqlar */
        {
            _categoryService.DeleteProductFromCategory(prodID, catID);

            return Redirect($"/admin/category/{catID}"); /* Kateqoriyadan her hansi mehsul silinse: hemin bu kateqoriya editleme sehifesine redirect et ve ya bawqa sozle - hazirki kateqoriya editleme sehifesini refresh et - dedik */
        }
        #endregion Product Management

        #region Category Management
        [HttpGet]
        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory([FromForm] CategoryViewModel categoryVM)
        {
            if(ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(categoryVM);

                bool resultOfCategoryCreateOperation = _categoryService.Create(category);

                if (resultOfCategoryCreateOperation == true)
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"You have successfully created a new category with name: \"{category.CategoryName}\"!",
                        AlertType: AlertMessage.AlertType.success
                    );

                    return RedirectToAction(nameof(CategoryList)); /* Yeni bir kateqoriya elave olunduqdan sonra admin qalmasin kateqoriya elave etme sehifesinde, onun yerine admini yonlendirek kateqoriya siyahisi sehifesine */
                }
                else
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"{_categoryService.ErrorMessage}",
                        AlertType: AlertMessage.AlertType.danger
                    );

                    /* Modelin propertyleri biznes qatinda 'IValidator' komeyile tetbiq etdiyim validasiyadan kece bilmese useri gonderirem kateqoriya yaratma sehifesine: */
                    return View(categoryVM);
                }
            }
            else
            {
                /* Eger inputplara/modele gelen data validasiyadan kece bilmese demeli xeta var ve bu zaman useri qaytariram kateqoriya yaratma sehifesine ve xetali daxil etmiw oldugu modelide gonderirem hemin sehifeye. Sehifede ise inputlar bu modele 'asp-for' ile bindlenmiw oldugu ucun (ve belece 'asp-for' 'id,name ve s.'-dan bawqa input ucun 'value=@Model.PropertyName' atributunuda generasiya edecek deye) problemsiz bir wekilde uygun inputlarda userin daxil etmiw oldugu melumatlar(categoryVM) yazilacaq ve xetali inputlarin yanindada ('asp-validation-for' sayesinde) uygun xeta mesaji: */
                return View(categoryVM);
            }
        }

        [HttpGet]
        public IActionResult EditCategory([FromRoute] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = _categoryService.GetByIdWithProducts((int)id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel categoryVM = _mapper.Map<CategoryViewModel>(category);

            /* Burada elave nese bir sorgu(hansi kateqoriyadaki mehsullari elde etmek isteyirem) yazmiyacam, cunki 'GetByIdWithProducts()' metodu geriye verdiyimiz ID-ye sahib kateqoriyani ve hemin kateqoriyaya aid mehsullari saxlayan bir obyekt dondururdu. Bu sebeble, bir bawa navigation property ile 'Product'-lari tek-tek secerek 'List' daxilinde elde edirem: */
            categoryVM.Products = category.ProductCategories.Select(prodCat => prodCat.Product).ToList(); /* Artiq ViewModelde hem kateqoriya hemde hemin kateqoriyaya aid mehsullar haqqinda melumatlar var */

            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory([FromForm] CategoryViewModel categoryVM)
        {
            if(ModelState.IsValid)
            {
                Category category = _categoryService.GetByID(categoryVM.CategoryID);

                if (category == null)
                {
                    return NotFound();
                }

                category = _mapper.Map<Category>(categoryVM);

                bool resultOfCategoryUpdateOperation = _categoryService.Update(category);

                if (resultOfCategoryUpdateOperation == true)
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"You have successfully edited a category with name: \"{category.CategoryName}\"!",
                        AlertType: AlertMessage.AlertType.info
                    );

                    return RedirectToAction(nameof(CategoryList)); /* Kateqoriya editlendikden sonra admin qalmasin kateqoriya editleme sehifesinde, onun yerine admini yonlendirek kateqoriya siyahisi sehifesine */
                }
                else
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: $"{_categoryService.ErrorMessage}",
                        AlertType: AlertMessage.AlertType.danger
                    );

                    /* Modelin propertyleri biznes qatinda 'IValidator' komeyile tetbiq etdiyim validasiyadan kece bilmese useri gonderirem kateqoriya yaratma sehifesine: */
                    return View(categoryVM);
                }
            }
            else
            {
                /* Eger inputplara/modele gelen data validasiyadan kece bilmese demeli xeta var ve bu zaman useri qaytariram kateqoriya editleme sehifesine ve xetali daxil etmiw oldugu modelide ('EditProduct' sehifesinde hidden inputlardan elde etdiyim hazirda editlemeye caliwdigim kateqoriyaya aid mehsullar ile birlikde) gonderirem hemin sehifeye (mehsullari niye gonderirem? - kateqoriya editleme sehifesinde hemde editlemeye caliwdigim kateqoriyaya aid mehsullarida siralayiram, bu sebeble viewya yanliw daxil edilmiw form datalari ile yanawi hemde hemin kateqoriyaya aid olan 'Product'-larida gondermeliyem). Sehifede ise inputlar bu modele 'asp-for' ile bindlenmiw oldugu ucun (ve belece 'asp-for' 'id,name ve s.'-dan bawqa input ucun 'value=@Model.PropertyName' atributunuda generasiya edecek deye) problemsiz bir wekilde uygun inputlarda userin daxil etmiw oldugu melumatlar(categoryVM) yazilacaq ve xetali inputlarin yanindada ('asp-validation-for' sayesinde) uygun xeta mesaji: */
                return View(categoryVM);
            }
        }

        [HttpPost]
        public IActionResult DeleteCategory([FromForm] int catID)
        {
            Category category = _categoryService.GetByID(catID);

            if (category != null)
            {
                _categoryService.Delete(category);

                CreateInformationalMessage
                (
                    AlertMessage: $"You have successfully deleted a category with name: \"{category.CategoryName}\"!",
                    AlertType: AlertMessage.AlertType.danger
                );
            }

            return RedirectToAction(nameof(CategoryList)); /* Kateqoriya silindikden sonra tezeden Redirect edirik admini hazirda oldugu 'CategoryList' sehifesine, yeni bir baxima sehifeni refresh etdirmiw olduq */
        }
        #endregion Category Management
    
        #region Role Management
        [HttpGet]
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> RoleCreate([FromForm] RoleModel model)
        {
            /* Userin post etmiw oldugu datalar dogru/valid datalardir? */
            if(ModelState.IsValid)
            {
                /* User dogru datalar daxil edibse, yeni rol yaradiriq: */
                AppRole appRole = new AppRole() { Name = model.Name };
                IdentityResult result = await _roleManager.CreateAsync(appRole);

                if(result.Succeeded) /* Rol ugurla yaradilsa user redirect edirik rol siyahisi sehifesine: */
                {
                    return RedirectToAction(nameof(RoleList));
                }
                else /* Rol yaradiliwi ugursuz neticelense baw vermiw xetalari elave edirik ModelState-e, hansiki daha sonra View terefde hemin bu xetalari yazdiracayiq sehifeye: */
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> RoleEdit([FromRoute] int? id)
        /* 'RoleList' sehifesinde editlenmesi istenen rolun sagindaki 'Edit'-e basilanda avtomatik bu actiona yonlendirilecek ve elave olaraq URL-e editlenmesi istenilen rolun 'id'-si yerlewdirilecek. */
        {
            /* Ilk once editlenmesi istenilen rolu DB-dan elde edek: */
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());

            /* Hazirki rolda olan/olmayan userleri saxlayacagim kolleksiyalari hazirlayiram: */
            List<AppUser> Members = new List<AppUser>();
            List<AppUser> NonMembers = new List<AppUser>();

            foreach(AppUser user in _userManager.Users)
            {
                /* DB-dan elde etdiyim user editlenmesi istenen rola sahibdirmi? */
                bool result = await _userManager.IsInRoleAsync(user, role.Name);

                /* Istediyim rolda olan userleri bir qrupa('Members'), istediyim rolda olmayan userleri ise bir bawqa qrupa('NonMembers') elave edirem: */
                if(result) Members.Add(user);
                else NonMembers.Add(user);
            }

            RoleDetails roleDetails = new RoleDetails()
            {
                Role = role,
                Members = Members,
                NonMembers = NonMembers
            };

            return View(roleDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleEdit([FromForm] RoleEditModel model)
        /* Rol editleme sehifesinden edilmiw post requestlerini qarwilayacaq. Modelin uzvleri olan:
            - RoleID          : 'RoleEdit' sehifesindeki name-i 'RoleID' olan hidden inputlarin value-sundan yaxalanacaq.
            - RoleName        : 'RoleEdit' sehifesindeki name-i 'RoleName' olan hidden inputlarin value-sundan yaxalanacaq.
            - IDsToAddRole    : 'RoleEdit' sehifesindeki name-i 'IDsToAddRole' checkboxlarin value-sundan yaxalanacaq.
            - IDsToRemoveRole : 'RoleEdit' sehifesindeki name-i 'IDsToRemoveRole' checkboxlarin value-sundan yaxalanacaq. */
        {
            /* Rol editleme sehifesinden post edilmiw modelde hec bir problem yoxdursa: */
            if(ModelState.IsValid)
            {
                /* Rola elave edilmek istenen User ID-lerini tek-tek elde edirem: */
                foreach(int userID in model.IDsToAddRole ?? Enumerable.Empty<int>()) /* 'IDsToAddRole' null olsa xeta alacaq idik, almayaq deye deyirik ki 'IDsToAddRole' nulldursa bow kolleksiya uzerinde dovr et, foreach ise dovr etmeye bawlayan zaman gorecek ki kolleksiyada dovr etmeye bir data yoxdur ve belece 'IDsToAddRole'-un null olmasi ile bagli xeta almayacayiq */
                {
                    /* Hazirda elde etmiw oldugum useri DB-da axtariram ve movcuddursa elde edirem: */
                    AppUser user = await _userManager.FindByIdAsync(userID.ToString());

                    /* Axtardigimiz user DB-da tapilsa: */
                    if(user != null)
                    {
                        /* user-i elave edirik ikinci parametrde gostermiw oldugumuz rola: */
                        IdentityResult result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        
                        /* Eger userin rola elave edilme emeliyyati ugursuz yekunlawsa bu barede ModelState-e baw vermiw xetanin aciqlamasini yazdiririq (ki, Viewya gonderek ve Viewda hemin bu xetani yazdiraq sehifeye): */
                        if(!result.Succeeded)
                        {
                            foreach(IdentityError error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            
                /* Roldan xaric edilmek istenen User ID-lerini tek-tek elde edirem: */
                foreach(int userID in model.IDsToRemoveRole ?? Enumerable.Empty<int>()) /* 'IDsToRemoveRole' null olsa xeta alacaq idik, almayaq deye deyirik ki 'IDsToRemoveRole' nulldursa bow kolleksiya uzerinde dovr et, foreach ise dovr etmeye bawlayan zaman gorecek ki kolleksiyada dovr etmeye bir data yoxdur ve belece 'IDsToRemoveRole'-un null olmasi ile bagli xeta almayacayiq */
                {
                    /* Hazirda elde etmiw oldugum useri DB-da axtariram ve movcuddursa elde edirem: */
                    AppUser user = await _userManager.FindByIdAsync(userID.ToString());

                    /* Axtardigimiz user DB-da tapilsa: */
                    if(user != null)
                    {
                        /* user-i ikinci parametrde gostermiw oldugum roldan xaric edirem: */
                        IdentityResult result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        
                        /* Useri roldan xaric etme emeliyyati ugursuz yekunlawsa bu barede ModelState-e baw vermiw xetanin aciqlamasini yazdiririq (ki, Viewya gonderek ve Viewda hemin bu xetani yazdiraq sehifeye): */
                        if(!result.Succeeded)
                        {
                            foreach(IdentityError error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            else return View();

            /* Userin rolu ugurla editlendise useri yonlendiririk 'RoleList' sehifesine: */
            return RedirectToAction(nameof(RoleList));
        }
        
        [HttpPost]
        public async Task<IActionResult> RoleDelete([FromForm] int roleID)
        /* Bu action 'RoleList' sehifesinde cedveldeki rollarin saginda olan 'Delete' knopkasina kliklenende iwleyecek. 'roleID' parametri 'RoleList' sehifesindeki eyni 'name'-li hidden inputdan yaxalanacaq. */
        {
            /* Ilk once uzerinde iw goreceyim rol DB-da movcuddursa elde edirem: */
            AppRole role = await _roleManager.FindByIdAsync(roleID.ToString());

            /* Rol DB-da movcuddursa silirem: */
            IdentityResult result = await _roleManager.DeleteAsync(role);

            if(result.Succeeded) /* Rol ugurla silinse */
            {
                CreateInformationalMessage
                (
                    AlertMessage: $"\"{role.Name}\" adli rol ugurla silindi.",
                    AlertType: AlertMessage.AlertType.success
                );
            }
            else /* Rolu silen zaman nese bir xeta baw verdise */
            {
                string errors = string.Empty;
                foreach(IdentityError error in result.Errors)
                {
                    errors += (error.Description + "\n");
                }
                
                CreateInformationalMessage
                (
                    AlertMessage: errors,
                    AlertType: AlertMessage.AlertType.danger
                );
            }

            /* Rol silindi-silinmedi istifadecini yonlendiririk role siyahisi sehifesine: */
            return RedirectToAction(nameof(RoleList));
        }
        #endregion Role Management

        #region User Management
        [HttpGet]
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit([FromRoute] int? id)
        /* 'UserList' sehifesinde editlenmesi istenen userin sagindaki 'Edit'-e basilanda avtomatik bu actiona yonlendirilecek ve elave olaraq URL-e editlenmesi istenilen userin 'id'-si yerlewdirilecek. */
        {
            /* Melumatlarinin editlenmesi istenilen useri DB-dan elde edirik: */
            AppUser user = await _userManager.FindByIdAsync(id.ToString());

            /* User movcuddursa: */
            if(user != null)
            {
                /* Userin aid oldugu rollari elde edirik: */
                List<string> RolesOfUser = await _userManager.GetRolesAsync(user) as List<string>;
                /* DB-daki butun Rollarin yalniz adini elde edirik: */
                List<string> AllAvailableRolesList = _roleManager.Roles.Select(role => role.Name).ToList();

                /* DB-daki movcud butun Rollari yerlewdirirem 'ViewBag'-a (ki, view-da userin hazirda hansi rollarda oldugu uzre qarwilawdirma ederek checkboxlari iwareledim): */
                ViewBag.AllAvailableRoles = AllAvailableRolesList;

                /* Userin melumatlarini gonderirem sehifeye: */
                return View(new UserEditModel()
                {
                    UserID         = user.Id,
                    FirstName      = user.FirstName,
                    LastName       = user.LastName,
                    UserName       = user.UserName,
                    Email          = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    RolesOfUser    = RolesOfUser
                });
            }

            /* Editlenmesi teleb olunan User qeydi DB-da tapilmasa istifadecini yonlendiririk 'UserList' sehifesine: */
            return RedirectToAction(nameof(UserList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit([FromForm] UserEditModel model, [FromForm] string[] selectedRoleNames)
        /* Userin melumatlari editlenib submit edilende hem model komeyile form elementlerinin deyerini yaxalayiram, hem de secilmiw rollari massiv komeyile yaxalayiram */
        {
            /* Eger form dogru doldurulubsa: */
            if(ModelState.IsValid)
            {
                /* Editlenmek istenen useri DB-da axtararaq elde edirik: */
                AppUser user = await _userManager.FindByIdAsync(model.UserID.ToString());

                /* Editlemek istenen user movcuddursa: */
                if(user != null)
                {
                    /* Userin melumatlarini yenileyirem: */
                    user = _mapper.Map<UserEditModel, AppUser>(model, user); /* Map-in movcud 'destination' alan overloadini iwledirem ki geriye ozunden bir obyekt yaradib dondurmesin(dondurse xeta alacam ki: "X email/ve s. sahib user artiq movcuddur" :buna sebeb odur ki, yeni 'AppUser' orneyi yaradildi hansiki yeni user ID-ye sahibdir ve belece 'UpdateAsync()' hemin bu user-i yeni bir user imiw kimi qebullanaraq DB-da yeni user yaratmaga caliwir, bu sebeble mappingi movcud obyektime tetbiq etmeliyem), movcud 'user' obyektine maplesin 'model'-i (bu yola alternativ ele bildiyimiz manual maplemeni etmekdir ya da bu cur duzgun overloadi secmeliyik mapping zamani). */
                    user.SecurityStamp = Guid.NewGuid().ToString(); /* FIX: "invalidoperationexception: User security stamp cannot be null." */

                    IdentityResult result = await _userManager.UpdateAsync(user);

                    if(result.Succeeded)
                    {
                        /* Userin aid oldugu rollari yenileyirem: */
                        List<string> RolesOfUser = await _userManager.GetRolesAsync(user) as List<string>; /* Userin sahib oldugu butun rollari elde edirem */
                        selectedRoleNames = selectedRoleNames ?? Array.Empty<string>(); /* Eger user hec bir rola aid edilmeyibse, bow string massivini menimsedirik */
                        await _userManager.AddToRolesAsync(user, selectedRoleNames.Except(RolesOfUser).ToArray<string>()); /* Usere, hazirda sahib oldugu rollar xaric olmaqla secilmiw diger rollari elave edirem */
                        await _userManager.RemoveFromRolesAsync(user, RolesOfUser.Except(selectedRoleNames).ToArray<string>()); /* Useri, hazirda secilmiw olan rollar xaric olmaqla diger rollardan cixariram */

                        /* Userin rollarini da ugurla yeniledikden sonra userin yonlendirileceyi sehifede gosterilmeyini istediyim mesaji set edirem: */
                        CreateInformationalMessage
                        (
                            AlertMessage: $"You have successfully edited details of user with username: \"{user.UserName}\"!",
                            AlertType: AlertMessage.AlertType.info
                        );

                        /* Userin melumatlari ugurla yenilendise istifadecini yonlendirirem 'UserList' sehifesine: */
                        return RedirectToAction(nameof(UserList));
                    }
                    else /* userin melumatlarini yenileyen zaman xeta baw verse, baw vermiw xetani ModelState-e elave edirik (ki, View-da gosterek usere) */
                    {
                        foreach(IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            /* Userin melumatlarinin editlenmesi zamani nese bir xeta(yanliw doldurulmuw form post edilse ve s.) baw verse gerek 'ViewBag.AllAvailableRoles' icerisini butun rollarla dolduraraq useri yeniden yanliw daxil etmiw oldugu forma yonlendirek, cunki 'UserEdit' sehifesinde checkboxlari 'ViewBag.AllAvailableRoles' esasinda yaradiram ve eger buradan hec bir rol gondermesem hemin o checkboxlarda yaranmayacaq ve netice olaraq natamam bir form yaratmiw olacam user ucun. Bu sebeble user yanliw melumatlara sahib form post etse useri daxil etmiw oldugu melumatlar ve appimdeki movcud butun rollar ile birlikde gonderirem yeniden user editleme sehifesine: */
            ViewBag.AllAvailableRoles = _roleManager.Roles.Select(role => role.Name).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult UserCreate()
        {
            /* DB-daki butun Rollarin yalniz adini elde edirik: */
            List<string> AllAvailableRolesList = _roleManager.Roles.Select(role => role.Name).ToList();

            /* DB-daki movcud butun Rollari yerlewdirirem 'ViewBag'-a (ki, view-da movcud her bir rol ucun checkbox yaradim): */
            ViewBag.AllAvailableRoles = AllAvailableRolesList; /* 'View'-da hemin bu 'AllAvailableRoles' komeyile rollarin checkboxlarini yaradacayiq */

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate([FromForm] UserCreateModel model, [FromForm] string[] selectedRoleNames)
        {
            /* User dogru bir form post edibse: */
            if(ModelState.IsValid)
            {
                /* Yaradacagimiz useri hazirlayiriq. Meqsedimize catmaq ucun userden yaxaladigimiz melumatlari saxlayan 'UserCreateModel' modeli icerisinden lazimi fieldleri export edirem: */
                AppUser user = _mapper.Map<AppUser>(model);
                /* User 'User Management'-den manual bir wekilde yaradilirsa avtomatik maili tesdiqlenmiw olsun: */
                user.EmailConfirmed = true;

                /* Ikinci parametrde verdiyimiz passworda sahib yeni bir user yaradiram: */
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                /* User ugurla yadildisa: */
                if(result.Succeeded)
                {
                    /* Useri elave edirik ikinci parametrde gosterdiyimiz rollara: */
                    await _userManager.AddToRolesAsync(user, selectedRoleNames);

                    /* Emeliyyatin ugurla yekunlawdigi barede sere gostereceyimiz mesaji set edirik: */
                    CreateInformationalMessage
                    (
                        AlertMessage: $"You have successfully created a new user with username: \"{user.UserName}\"!",
                        AlertType: AlertMessage.AlertType.success
                    );

                    /* Istifadecini yonlendiririk user siyahisi sehifesine, burada istifadecini ugurla yaratdigi user ile bagli tebrik mesaji qarwilayacaq: */
                    return RedirectToAction(nameof(UserList));
                }
                else /* User yaradilan vaxt nese bir xeta baw verse, baw vermiw xetalarin aciqlamalarini elave edirik ModelState-e (ki, View-da baw vermiw hemin bu xetalari usere bildirek:) */
                {
                    /* Xetalari ModelState-e elave edirik: */
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    /* Useri, user yaratma sehifesine qaytaracam deye gerek user yaratma sehifesindeki checkboxlarin hansi rollar esasinda yaradilacaginida gonderim hemin user yaratma sehifesine (ki checkboxlar ugurla yaradilsin, yoxsa hec bir checkbox yaradilmayacaq, cunki checkboxlarin hansi rollar esasinda yaradilacagini bildirmemiwem), yanliw daxil etmiw oldugu melumatlar ile birlikde: */
                    List<string> AllAvailableRolesList = _roleManager.Roles.Select(role => role.Name).ToList(); /* DB-daki butun Rollarin yalniz adini elde edirik */
                    ViewBag.AllAvailableRoles = AllAvailableRolesList; /* DB-daki movcud butun Rollari yerlewdirirem 'ViewBag'-a (ki, view-da movcud her bir rol ucun checkbox yaradim) */
                    return View(model);
                }
            }
            else /* Userin post etdiyi form-da problem varsa: */
            {
                /* Useri, user yaratma sehifesine qaytaracam deye gerek user yaratma sehifesindeki checkboxlarin hansi rollar esasinda yaradilacaginida gonderim hemin user yaratma sehifesine (ki checkboxlar ugurla yaradilsin, yoxsa hec bir checkbox yaradilmayacaq, cunki checkboxlarin hansi rollar esasinda yaradilacagini bildirmemiwem), yanliw daxil etmiw oldugu melumatlar ile birlikde: */
                List<string> AllAvailableRolesList = _roleManager.Roles.Select(role => role.Name).ToList(); /* DB-daki butun Rollarin yalniz adini elde edirik */
                ViewBag.AllAvailableRoles = AllAvailableRolesList; /* DB-daki movcud butun Rollari yerlewdirirem 'ViewBag'-a (ki, view-da movcud her bir rol ucun checkbox yaradim) */
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserDelete([FromForm] int? userID)
        /* Bu action 'UserList' sehifesinde cedveldeki userlerin saginda olan 'Delete' knopkasina kliklenende iwleyecek. 'userID' parametri 'UserList' sehifesindeki eyni 'name'-li hidden inputdan yaxalanacaq. */
        {
            /* Ilk once uzerinde iw goreceyim useri DB-da movcuddursa elde edirem: */
            AppUser user = await _userManager.FindByIdAsync(userID.ToString());

            /* User DB-da movcuddursa silirem: */
            IdentityResult result = await _userManager.DeleteAsync(user);

            if(result.Succeeded) /* User ugurla silinse */
            {
                /* Usere gostereceyim mesaji set edirem: */
                CreateInformationalMessage
                (
                    AlertMessage: $"\"{user.UserName}\" adli user ugurla silindi.",
                    AlertType: AlertMessage.AlertType.success
                );
            }
            else /* Useri silen zaman nese bir xeta baw verdise */
            {
                /* Usere gostereceyim mesajlari hazirlayiram: */
                string errors = string.Empty;
                foreach(IdentityError error in result.Errors)
                {
                    errors += (error.Description + "\n");
                }

                /* Usere gostereceyim mesaji set edirem: */
                CreateInformationalMessage
                (
                    AlertMessage: errors,
                    AlertType: AlertMessage.AlertType.danger
                );
            }

            /* User silindi-silinmedi istifadecini yonlendiririk role siyahisi sehifesine ve yonlendirildiyi hemin bu sehifede useri etdiyi emeliyyati ile elaqeli netice mesaji qarwilamiw olacaq: */
            return RedirectToAction(nameof(UserList));
        }
        #endregion User Management
    }
}