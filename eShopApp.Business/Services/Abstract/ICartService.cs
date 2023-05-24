using eShopApp.Entity.Entities;

namespace eShopApp.Business.Services.Abstract
{
    public interface ICartService
    {
        void InitializeCart(int UserID);
        Cart GetCartByUserID(int UserID);
        void AddToCart(int UserID, int ProductID, int ProductQuantity);
        void DeleteFromCart(int UserID, int ProductID);
    }
}