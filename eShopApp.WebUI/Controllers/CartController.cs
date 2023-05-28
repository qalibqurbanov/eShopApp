using eShopApp.Entity.Enums;
using System.Security.Claims;
using eShopApp.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using eShopApp.WebUI.Models.Cart;
using eShopApp.WebUI.Models.Order;
using Microsoft.AspNetCore.Authorization;
using eShopApp.Business.Services.Abstract;

namespace eShopApp.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService, IOrderService orderService)
        {
            this._cartService = cartService;
            this._orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            /* 'Cart (Sebet)' melumatlarini elde edeceyim userin ilk once ID-sini tapiram: */
            int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            /* ID-sini verdiyim userin Cart melumatlarini (+ Cart icerisindeki her bir mehsulu ve hemin mehsul haqqinda melumatlari) elde edirem */
            Cart cart = _cartService.GetCartByUserID(userID);


            /* Eger userin 'Cart (Sebet)'-inda nese bir wey varsa: */
            if(cart != null)
            {
                /* Sehifede gostermek istediyim 'Cart (Sebet)' melumatlarini gonderirem View-ya: */
                return View(new CartModel()
                {
                    CartID    = cart.ID,
                    CartItems = cart.CartItems.Select(cartItem => new CartItemModel() /* 'CartItems' adli kolleksiyadaki her bir elementi bir bawqa 'CartItemModel' obyekti icerisine yerlewdirirem */
                    {
                        CartItemID = cartItem.ID,
                        ProductID  = cartItem.ProductID,
                        Quantity   = cartItem.Quantity,
                        Name       = cartItem.Product.ProductName,     /* 'GetByUserID()' icerisinde 'Product'-i 'Include()' etmiwdim deye burada rahatca 'Product'-a kecid ederek uygun propertyni secerek iwlede bilirem */
                        Price      = cartItem.Product.ProductPrice,    /* 'GetByUserID()' icerisinde 'Product'-i 'Include()' etmiwdim deye burada rahatca 'Product'-a kecid ederek uygun propertyni secerek iwlede bilirem */
                        ImageName  = cartItem.Product.ProductImageName /* 'GetByUserID()' icerisinde 'Product'-i 'Include()' etmiwdim deye burada rahatca 'Product'-a kecid ederek uygun propertyni secerek iwlede bilirem */
                    }).ToList() /* Son olaraq yaradilmiw butun 'CartItemModel'-leri kolleksiyaya cevirerek menimsedirem 'CartModel' sinifinin 'CartItems' uzvune */
                });
            }
            else /* Userin 'Cart (Sebet)'-i bowdursa View-ya 'null' gonderirik */
            {
                return View(null);
            }
        }

        [HttpPost]
        public IActionResult AddToCart([FromForm] int productID, [FromForm] int productQuantity)
        {
            /* Ilk once hazirki userin ID-sini elde edirem ki, hansi userin 'Cart (Sebet)'-i uzerinde iw goreceyimi bildire bilim: */
            int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            /* ID-sini gosterdiyimiz userin 'Cart (Sebet)'-ne mehsul elave edirem: */
            _cartService.AddToCart(userID, productID, productQuantity);

            /* Mehsul userin 'Cart (Sebet)'-ne elave olunduqdan sonra useri yonlendirirem 'Cart (Sebet)' sehifesine: */
            return Redirect("/cart");
        }

        [HttpPost]
        public IActionResult DeleteFromCart([FromForm] int productID)
        {
            /* Ilk once hazirki userin ID-sini elde edirem ki, hansi userin 'Cart (Sebet)'-i uzerinde iw goreceyimi bildire bilim: */
            int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            /* Burada hansi ID-ye sahib userin sebetinden hansi mehsulu sileceyimi gosterirem: */
            _cartService.DeleteFromCart(userID, productID);

            /* Mehsulu userin sebetinden sildikden sonra useri yonlendirirem sebet sehifesine: */
            return Redirect("/cart");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            /* 'Cart (Sebet)' melumatlarini elde edeceyim userin ilk once ID-sini tapiram: */
            int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            /* ID-sini verdiyim userin Cart melumatlarini (+ Cart icerisindeki her bir mehsulu ve hemin mehsul haqqinda melumatlari) elde edirem */
            Cart cart = _cartService.GetCartByUserID(userID);

            /* Userin almaq istediyi mehsullari sebetden elde ederek saxlayiram bir obyekt icerisinde: */
            OrderDetailsModel orderModel = new OrderDetailsModel()
            {
                CartModel = new CartModel()
                {
                    CartID    = cart.ID,
                    CartItems = cart.CartItems.Select(cartItem => new CartItemModel() /* 'CartItems' adli kolleksiyadaki her bir elementi bir bawqa 'CartItemModel' obyekti icerisine yerlewdirirem */
                    {
                        CartItemID  = cartItem.ID,
                        ProductID   = cartItem.ProductID,
                        Quantity    = cartItem.Quantity,
                        Description = cartItem.Product.ProductDescription, /* 'GetByUserID()' icerisinde 'Product'-i 'Include()' etmiwdim deye burada rahatca 'Product'-a kecid ederek uygun propertyni secerek iwlede bilirem */
                        Name        = cartItem.Product.ProductName, /* 'GetByUserID()' icerisinde 'Product'-i 'Include()' etmiwdim deye burada rahatca 'Product'-a kecid ederek uygun propertyni secerek iwlede bilirem */
                        Price       = cartItem.Product.ProductPrice,    /* 'GetByUserID()' icerisinde 'Product'-i 'Include()' etmiwdim deye burada rahatca 'Product'-a kecid ederek uygun propertyni secerek iwlede bilirem */
                        ImageName   = cartItem.Product.ProductImageName /* 'GetByUserID()' icerisinde 'Product'-i 'Include()' etmiwdim deye burada rahatca 'Product'-a kecid ederek uygun propertyni secerek iwlede bilirem */
                    }).ToList() /* Son olaraq yaradilmiw butun 'CartItemModel'-leri kolleksiyaya cevirerek menimsedirem 'CartModel' sinifinin 'CartItems' uzvune */
                }
            };

            /* Userin almaq istediyi mehsullari gonderirem 'Checkout' sehifesine: */
            return View(orderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderDetailsModel model, [FromForm] string countryCode = null)
        {
            if(ModelState.IsValid && countryCode != null)
            {
                /* 'Cart (Sebet)' melumatlarini elde edeceyim userin ilk once ID-sini tapiram: */
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                /* Userin 'Cart (Sebet)'-ni elde edirem */
                Cart cart = _cartService.GetCartByUserID(userID);

                /* Model Binding zamani 'CartDetailsModel' tipli 'model' obyekti icerisinde olan 'CartModel' fieldi bow olacaq, burada 'CartModel' obyektini userin hazirki 'Cart (Sebet)' melumatlari ile doldururam: */
                model.CartModel = new CartModel()
                {
                    CartID = cart.ID,
                    CartItems = cart.CartItems.Select(cartItem => new CartItemModel()
                    {
                        CartItemID = cartItem.ID,
                        ProductID  = cartItem.ProductID,
                        Name       = cartItem.Product.ProductName,
                        Price      = (double)cartItem.Product.ProductPrice,
                        ImageName  = cartItem.Product.ProductImageName,
                        Quantity   = cartItem.Quantity
                    }).ToList()
                };

                /* Yaradilacaq olan mehsul aliwi ile elaqeli DB qeydi ucun 'Order' obyektini hazirlayiram: */
                Order order = new Order()
                {
                    UserID      = userID,
                    FirstName   = model.FirstName,
                    LastName    = model.LastName,
                    City        = model.City,
                    Address     = model.Address,
                    PostalCode  = model.PostalCode,
                    Phone       = $"{countryCode}{model.Phone}",
                    Email       = model.Email,
                    Note        = model.Note,
                    OrderDate   = DateTime.Now,
                    OrderNumber = new Random().Next(100, int.MaxValue),
                    OrderState  = OrderState.Completed,
                    OrderItems  = model.CartModel.CartItems.Select(cartItemModel => new OrderItem()
                    {
                        Price     = cartItemModel.Price,
                        Quantity  = cartItemModel.Quantity,
                        ProductID = cartItemModel.ProductID
                    }).ToList()
                };

                /* Yeni bir Order (mehsul aliwi) datasi yaradiram: */
                _orderService.Create(order);

                /* User 'Cart (Sebet)'-indeki mehsullari alib deye, userin 'Cart (Sebet)'-ni temizleyirem: */
                _cartService.ClearCart(model.CartModel.CartID);

                /* Mehsul ugurla sifariw verildi deye useri yonlendirirem 'Success' sehifesine: */
                return View("Success");
            }
            else /* User dogru bir form post etmeyibse: */
            {
                /* Eger user 'country code' comboboxundan olke telefon kodu secmeyibse bu barede ModelState-e yeni bir xeta elave edirem: */
                if(countryCode == null) ModelState.AddModelError("", "Please select the correct country code for your phone number.");

                /* User dogru bir form post etmediyi zaman useri geriye 'Checkout' sehifesine yonlendireceyik, lakin bu sehifede userin almaq istediyi mehsullarida gosterirem, bu sebeble gerek 'CartModel'-de doldurum: */
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); /* 'Cart (Sebet)' melumatlarini elde edeceyim userin ilk once ID-sini tapiram: */
                Cart cart = _cartService.GetCartByUserID(userID); /* Userin 'Cart (Sebet)'-ni elde edirem */
                model.CartModel = new CartModel() /* Model Binding zamani 'CartDetailsModel' tipli 'model' obyekti icerisinde olan 'CartModel' fieldi bow olacaq, burada 'CartModel' obyektini userin hazirki 'Cart (Sebet)' melumatlari ile doldururam */
                {
                    CartID    = cart.ID,
                    CartItems = cart.CartItems.Select(cartItem => new CartItemModel()
                    {
                        CartItemID  = cartItem.ID,
                        ProductID   = cartItem.ProductID,
                        Name        = cartItem.Product.ProductName,
                        Description = cartItem.Product.ProductDescription,
                        Price       = (double)cartItem.Product.ProductPrice,
                        ImageName   = cartItem.Product.ProductImageName,
                        Quantity    = cartItem.Quantity
                    }).ToList()
                };

                /* Userin forma daxil etdiyi datalar arasinda yanliw datalar varsa, useri daxil etmiw oldugu melumatlar ile bir yerde qaytaririq geriye 'Checkout' sehifesine: */
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult OrderList()
        {
            /* 'Order (sifariw)' melumatlarini elde edeceyim userin ilk once ID-sini tapiram: */
            int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            /* DB-dan hazirki userin etmiw oldugu sifariwleri elde edek: */
            List<Order> orderList = _orderService.GetOrders(userID);

            /* Sehifeye dondureceyim, ozunde sifariwler haqqinda melumatlari saxlayacaq olan kolleksiya('orderListModel') ve dovr daxilinde iwledeceyim muveqqeti deyiwen('orderModel'): */
            List<OrderListModel> orderListModel = new List<OrderListModel>();
            OrderListModel orderModel = null;

            /* Userin etmiw oldugu sifariw qeder dovr edirik ve her dovr zamani edilmiw her bir sifariw haqqinda melumatlari elde ederek yerlewdiririk yaratdigimiz kolleksiyaya: */
            foreach(Order order in orderList)
            {
                orderModel = new OrderListModel()
                {
                    ID = order.ID,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Address = order.Address,
                    City = order.City,
                    PostalCode = order.PostalCode,
                    OrderNumber = order.OrderNumber,
                    OrderDate = order.OrderDate,
                    Email = order.Email,
                    Note = order.Note,
                    Phone = order.Phone,

                    OrderItems = order.OrderItems.Select(oi => new OrderItemModel()
                    {
                        OrderItemID = oi.ID,
                        OrderItemName = oi.Product.ProductName,
                        OrderItemImageName = oi.Product.ProductImageName,
                        OrderItemPrice = oi.Price,
                        OrderItemQuantity = oi.Quantity
                    }).ToList()
                };

                /* Her dovrde yeni bir 'OrderListModel' yaradaraq icerisini DB-dan elde etmiw oldugum sifariw melumatlari ile doldururam ve son olaraq hemin bu 'OrderListModel' orneyini elave edirem sehifeye dondureceyim kolleksiyaya: */
                orderListModel.Add(orderModel);
            }

            /* Ozunde userin etmiw oldugu sifariwler ile bagli melumatlari saxlayan kolleksiyani dondururem sehifeye: */
            return View(orderListModel);
        }
    }
}