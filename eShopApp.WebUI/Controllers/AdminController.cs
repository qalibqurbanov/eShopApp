using AutoMapper;
using eShopApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using eShopApp.Entity.Entities;
using eShopApp.WebUI.Models.ViewModels;
using eShopApp.WebUI.Extensions.Helpers;
using Microsoft.AspNetCore.Authorization;
using eShopApp.Business.Services.Abstract;
using eShopApp.WebUI.Extensions.Serialization;

namespace eShopApp.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public AdminController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment, IMapper mapper)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._environment = environment;
            this._mapper = mapper;
        }

        #region Mehsul/Kateqoriya uzerinde reallawdirilmiw emeliyyat barede usere sehifenin ust hissesinde gostereceyimiz mesaji set eden komekci mini metod.
        private void CreateInformationalMessage(string AlertMessage, AlertMessage.AlertType AlertType)
        {
            TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
            {
                alertMessage = AlertMessage,
                alertType = AlertType
            });
        }
        #endregion Mehsul/Kateqoriya uzerinde reallawdirilmiw emeliyyat barede usere sehifenin ust hissesinde gostereceyimiz mesaji set eden komekci mini metod.

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
    }
}