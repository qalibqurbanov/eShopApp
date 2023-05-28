using eShopApp.Entity.Entities;

namespace eShopApp.Business.Services.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);

        /* IOrderRepository: */
        List<Order> GetOrders(int UserID);
    }
}