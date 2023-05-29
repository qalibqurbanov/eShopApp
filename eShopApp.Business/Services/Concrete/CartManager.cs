using eShopApp.Entity.Entities;
using eShopApp.Business.Services.Abstract;
using eShopApp.DataAccess.UnitOfWork.Abstract;

namespace eShopApp.Business.Services.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /* Burada 'AddToCart' funksionalligini teyin etmiwem - userin elave etmeye caliwdigi mehsul cartda varsa, hemin mehsulun miqdarin bir artiririq sebet icerisinde, yox eger sebetde yoxdursa onda hemin mehsulu elave edirik sebete: */
        public void AddToCart(int UserID, int ProductID, int ProductQuantity)
        {
            /* Userin 'Cart (Sebet)'-ni ve hemin bu sebetdeki mehsullari elde edirik: */
            Cart cart = GetCartByUserID(UserID);

            /* Usere 'Cart (Sebet)' tehkim edilibse: */
            if(cart != null)
            {
                /* 'Cart (Sebet)'-a mehsul elave eden zaman userin 'Cart (Sebet)' melumatlarini etrafli bir wekilde elde etmeliydim ve etmiwem. Bu melumatlar mene ona gore lazimdir ki, user once sebetinde olmayan bir mehsulu elave edir? ya evvelceden sebetinde olan bir mehsulu elave edir? mehsul daha onceden sebetinde var imiwse bu zaman hemin mehsulun miqdarini/quantity bir artiracam. */

                /* Yoxlayiram ki - userin hazirda elave etmeye caliwdigi mehsul hazirda sebetinde var? varsa hemin mehsulu elde etmeyime ehtiyac yoxdur, awagidaki kimi index-ni yoxlayaraq movcudlugunu yoxlamagim kifayetdir. Hazirda elave edilmeye caliwilan mehsul sebetde var? varsa index-ni qaytar (belece bilmiw olacam ki userin hazirda elave etmeye caliwdigi mehsul hazirda sebetde var ve bu ise o demekdir ki geriye qalir sebetdeki movcud mehsulun miqdarini/quantity bir artirmaq): */
                int index = cart.CartItems.FindIndex(cartItem => cartItem.ProductID == ProductID);

                if(index < 0) /* 'index' 0-dan kicikdirse demeli - Userin hazirda sebetine elave etmeye caliwdigi mehsul daha once sebetinde yox imiw */
                {
                    /* Userin 'Cart'-na yeni bir mehsul elave edirem: */
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductID = ProductID,
                        Quantity = ProductQuantity,
                        CartID = cart.ID
                    });
                }
                else /* eks halda demeli - Userin hazirda sebetine elave etmeye caliwdigi mehsul daha once sebetinde var imiw */
                {
                    /* Yuxarida sebetde movcud olan mehsulun kolleksiyadaki('CartItems' - userin sebetindeki her bir mehsulu ozunde saxlayan kolleksiya) indeksini elde etmiwdim, burada hemin indeks komeyile sebetdeki mehsulun miqdarini 'ProductQuantity' qeder artiriram: */
                    cart.CartItems[index].Quantity += ProductQuantity;
                }

                /* Yuxaridaki: "karta yeni mehsul elave et" :ve ya: "varolan Quantity sutununu yenile" :emeliyyatimizdan sonra etdiyimiz hemin bu deyiwikliyi DB-ya('Carts' cedveline) yaziriq: */
                _unitOfWork.Carts.Update(cart);
                _unitOfWork.Commit();
            }
        }

        public void ClearCart(int CartID)
        {
            _unitOfWork.Carts.ClearCart(CartID, false);
            _unitOfWork.Commit();
        }

        public void DeleteFromCart(int UserID, int ProductID)
        {
            /* Ilk once userin ID-si esasinda, icerisinden mehsul sileceyim sebeti elde edirem: */
            Cart cart = GetCartByUserID(UserID);

            /* Eger usere sebet tehkim olunubsa: */
            if(cart != null)
            {
                /* Userin sebetinden mehsulu sil: */
                _unitOfWork.Carts.DeleteFromCart(cart.ID, ProductID, false);
                _unitOfWork.Commit();
            }
        }

        /* ID-sini daxil etdiyimiz userin 'Cart (Sebet)' melumatlarini dondurur: */
        public Cart GetCartByUserID(int UserID)
        {
            return _unitOfWork.Carts.GetByUserID(UserID, true);
        }

        /* 'Initialize' yeni, 'bawlatmaq' - "usere cart/sebet tehkim edilsin, verilsin" ve benzer menalardadir. Bu metod sayesinde DB-ya 'Cart (Sebet)' qeydi elave ederek usere 'Cart (Sebet)' tehkim edirik: */
        public void InitializeCart(int UserID)
        {
            /* ID-sini gosterdiyimiz usere aid yeni bir qeyd yaradilacaq DB-daki 'Carts' cedelinde (ve bu ise o menaya gelecek ki: "XXX ID-li Usere sebet tehkim edilib"): */
            _unitOfWork.Carts.Create(new Cart() { UserID = UserID });
            _unitOfWork.Commit();
        }
    }
}