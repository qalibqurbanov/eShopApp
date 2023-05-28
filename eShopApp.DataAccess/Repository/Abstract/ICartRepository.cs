using eShopApp.Entity.Entities;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;

namespace eShopApp.DataAccess.Repository.Abstract
{
    /// <summary>
    /// 'Cart (Sebet)' ile elaqeli emeliyyatlarin imzasini saxlayir.
    /// </summary>
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetByUserID(int UserID, bool DisableChangeTracker);
        void DeleteFromCart(int CartID, int ProductID, bool DisableChangeTracker);
        void ClearCart(int CartID, bool DisableChangeTracker);
    }
}