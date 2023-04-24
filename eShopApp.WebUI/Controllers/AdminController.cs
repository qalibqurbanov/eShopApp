﻿using System.Net;
using AutoMapper;
using eShopApp.Business.Services.Abstract;
using eShopApp.Entity.Entities;
using eShopApp.WebUI.Extensions.Helpers;
using eShopApp.WebUI.Extensions.Serialization;
using eShopApp.WebUI.Models;
using eShopApp.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eShopApp.WebUI.Controllers
{
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
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct([FromForm] ProductViewModel productVM, [FromForm] IFormFile imageFile)
        {
            Product product = _mapper.Map<Product>(productVM);
            if (imageFile != null)
            {
                /* Faylin ozunu yerlewdirirem 'Web Root (wwwroot)' papkasina: */
                imageFile.MoveFormFile(_environment.WebRootPath, out string imageName);

                /* Daha sonra ise hemin faylin adini qeyd edirem DB-ya: */
                product.ProductImageName = imageName;
            }

            _productService.Create(product);

            TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
            {
                alertMessage = $"You have successfully created a new product with name: \"{product.ProductName}\"!",
                alertType = AlertMessage.AlertType.success
            });

            return RedirectToAction(nameof(ProductList)); /* Yeni bir mehsul elave olunduqdan sonra admin qalmasin mehsul elave etme sehifesinde, onun yerine admini yonlendirek mehsul siyahisi sehifesine */
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
            ViewBag.Categories = _categoryService.GetAll();

            return View(productVM);
        }

        [HttpPost]
        public IActionResult EditProduct([FromForm]ProductViewModel productVM, [FromForm]int[] catIDs, [FromForm] IFormFile imageFile)
            /* 'catIDs' - yaranacaq olan 'Switch' kontrollarinin 'name'-i esasinda yaxalanacaq 'value'-lari */
            /* 'imageFile' - form-dan post olunan wekli yaxalayacaq, diqqet etmek lazimdir ki: "input type="file" :olan inputun 'name'-i ile buradaki parametrin adi eyni olsun (ki Model Binding dogru iwlesin) */
        {
            Product product = _productService.GetByID(productVM.ProductID);

            if(product == null)
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

            _productService.Update(product, catIDs);

            TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
            {
                alertMessage = $"You have successfully edited a product with name: \"{product.ProductName}\"!",
                alertType = AlertMessage.AlertType.info
            });

            return RedirectToAction(nameof(ProductList)); /* Mehsul editlendikden sonra admin qalmasin mehsul editleme sehifesinde, onun yerine admini yonlendirek mehsul siyahisi sehifesine */
        }

        [HttpPost]
        public IActionResult DeleteProduct([FromForm] int prodID)
        {
            Product product = _productService.GetByID(prodID);

            if(product != null)
            {
                _productService.Delete(product);

                TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
                {
                    alertMessage = $"You have successfully deleted a product with name: \"{product.ProductName}\"!",
                    alertType = AlertMessage.AlertType.danger
                });
            }

            return RedirectToAction(nameof(ProductList)); /* Mehsul silindikden sonra tezeden Redirect edirik admini hazirda oldugu 'ProductList' sehifesine, yeni bir baxima sehifeni refresh etdirmiw olduq */
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
            Category category = _mapper.Map<Category>(categoryVM);

            _categoryService.Create(category);

            TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
            {
                alertMessage = $"You have successfully created a new category with name: \"{category.CategoryName}\"!",
                alertType = AlertMessage.AlertType.success
            });

            return RedirectToAction(nameof(CategoryList)); /* Yeni bir kateqoriya elave olunduqdan sonra admin qalmasin kateqoriya elave etme sehifesinde, onun yerine admini yonlendirek kateqoriya siyahisi sehifesine */
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
            Category category = _categoryService.GetByID(categoryVM.CategoryID);

            if (category == null)
            {
                return NotFound();
            }

            category = _mapper.Map<Category>(categoryVM);

            _categoryService.Update(category);

            TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
            {
                alertMessage = $"You have successfully edited a category with name: \"{category.CategoryName}\"!",
                alertType = AlertMessage.AlertType.info
            });

            return RedirectToAction(nameof(CategoryList)); /* Kateqoriya editlendikden sonra admin qalmasin kateqoriya editleme sehifesinde, onun yerine admini yonlendirek kateqoriya siyahisi sehifesine */
        }

        [HttpPost]
        public IActionResult DeleteCategory([FromForm] int catID)
        {
            Category category = _categoryService.GetByID(catID);

            if (category != null)
            {
                _categoryService.Delete(category);

                TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
                {
                    alertMessage = $"You have successfully deleted a category with name: \"{category.CategoryName}\"!",
                    alertType = AlertMessage.AlertType.danger
                });
            }

            return RedirectToAction(nameof(CategoryList)); /* Kateqoriya silindikden sonra tezeden Redirect edirik admini hazirda oldugu 'CategoryList' sehifesine, yeni bir baxima sehifeni refresh etdirmiw olduq */
        }
        #endregion Category Management
    }
}