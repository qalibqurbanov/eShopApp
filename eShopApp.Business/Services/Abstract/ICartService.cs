using eShopApp.Entity.Entities;
using Microsoft.Identity.Client;

namespace eShopApp.Business.Services.Abstract
{
    public interface ICartService
    {
        void InitializeCart(int UserID);
        Cart GetCartByUserID(int UserID);
        void AddToCart(int UserID, int ProductID, int ProductQuantity);

        /* ICartRepository: */
        void DeleteFromCart(int UserID, int ProductID);
        void ClearCart(int CartID);
    }
}