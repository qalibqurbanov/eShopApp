using System.Security.Claims;
using eShopApp.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using eShopApp.WebUI.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using eShopApp.Business.Services.Abstract;

namespace eShopApp.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
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
    }
}