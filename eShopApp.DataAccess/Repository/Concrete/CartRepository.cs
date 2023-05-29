using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete.GenericRepositories;

namespace eShopApp.DataAccess.Repository.Concrete
{
    /// <summary>
    /// 'Cart (Sebet)' ile elaqeli emeliyyatlari saxlayir.
    /// </summary>
    public class CartRepository : Repository<Cart, ShopDbContext>, ICartRepository
    {
        private readonly ShopDbContext _dbContext;
        public CartRepository(ShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<Cart> DbTable_Cart => _dbContext.Set<Cart>();
        private DbSet<CartItem> DbTable_CartItem => _dbContext.Set<CartItem>();

        public void DeleteFromCart(int CartID, int ProductID, bool DisableChangeTracker)
        {
            /* Ilk once 'CartItems' cedvelini icra olunmamiw sorgu kimi ve ya bawqa sozle uzerine sorgu elave ede bileceyim bir wekilde elde edirem: */
            var cartItem = DbTable_CartItem.AsQueryable();

            if(DisableChangeTracker)
            {
                cartItem = cartItem
                    .AsNoTracking()
                    .Where(cartItem => cartItem.CartID == CartID && cartItem.ProductID == ProductID); /* Bu wekilde bir filter komeyile silmek istediyim 'CartItem'-i secirem */
            }
            else
            {
                cartItem = cartItem
                    .Where(cartItem => cartItem.CartID == CartID && cartItem.ProductID == ProductID); /* Bu wekilde bir filter komeyile silmek istediyim 'CartItem'-i secirem */
            }

            _dbContext.CartItems.Remove(cartItem.FirstOrDefault()); /* 'CartItem'-i silirem ve belece mehsulu/CartItem 'Cart (Sebet)'-dan silmiw oluram */

            //_dbContext.SaveChanges();
        }

        /* Cedvelden, 'UserID'-ye aid olan 'Cart' melumatlarini (+ hemin 'Cart'-la elaqeli olan 'CartItems' ve 'Product' melumatlarini) dondururem: */
        public Cart GetByUserID(int UserID, bool DisableChangeTracker)
        {
            #nullable disable

            if (DisableChangeTracker)
            {
                return DbTable_Cart
                    .AsNoTracking() /* DB-dan elde olunacaq datalar track edilmesin, cunki hemin datalar uzerinde deyiwiklik etmeyecem, yalnizca oxuyacam. */
                    .Include(cart => cart.CartItems) /* Sorguya 'Cart' icerisindeki 'CartItems' navigation propertysinide daxil edirem, bu da o demekdir ki 'Cart'-i elde etmeye caliwsam bu cur mene hemde o anki 'Cart' melumatlariyla birlikde uygun 'Cart'-in 'CartItems' melumatlarida gelecek. */
                    .ThenInclude(cartItem => cartItem.Product) /* Eyni wekilde sorguya 'CartItem' icerisindeki 'Product' propertysinide daxil edirem, bu da o demekdir ki 'CartItem'-i elde etdiyim zaman, hemin 'CartItem' ile elaqeli olan 'Product' melumatlarini da elde edecem. */
                    .FirstOrDefault(cart => cart.UserID == UserID); /* Elimde olan 'UserID' ile ilk qarwilawdigin uygun 'Cart' qeydini sec ('FirstOrDefault()' yerine yuxarida ilk once 'Where' ile wert qoya da bilerdim). */
            }
            else
            {
                return DbTable_Cart
                    .Include(cart => cart.CartItems) /* Sorguya 'Cart' icerisindeki 'CartItems' navigation propertysinide daxil edirem, bu da o demekdir ki 'Cart'-i elde etmeye caliwsam bu cur mene hemde o anki 'Cart' melumatlariyla birlikde uygun 'Cart'-in 'CartItems' melumatlarida gelecek. */
                    .ThenInclude(cartItem => cartItem.Product) /* Eyni wekilde sorguya 'CartItem' icerisindeki 'Product' propertysinide daxil edirem, bu da o demekdir ki 'CartItem'-i elde etdiyim zaman, hemin 'CartItem' ile elaqeli olan 'Product' melumatlarini da elde edecem. */
                    .FirstOrDefault(cart => cart.UserID == UserID); /* Elimde olan 'UserID' ile ilk qarwilawdigin uygun 'Cart' qeydini sec ('FirstOrDefault()' yerine yuxarida ilk once 'Where' ile wert qoya da bilerdim). */
            }

            #nullable enable
        }

        public void ClearCart(int CartID, bool DisableChangeTracker)
        {
            List<CartItem> cartItems = new List<CartItem>();

            if(DisableChangeTracker)
            {
                cartItems.AddRange
                (
                    DbTable_CartItem
                        .AsNoTracking()
                        .Where(cartItem => cartItem.CartID == CartID)
                        .ToList()
                );
            }
            else
            {
                cartItems.AddRange
                (
                    DbTable_CartItem
                        .Where(cartItem => cartItem.CartID == CartID)
                        .ToList()
                );
            }

            _dbContext.CartItems.RemoveRange(cartItems);

            //_dbContext.SaveChanges();
        }
    }
}