using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;
using eShopApp.Entity.Entities;

namespace eShopApp.DataAccess.Repository.Abstract
{
    /// <summary>
    /// Mehsul alma emeliyyatlarinin imzasini saxlayir.
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrders(int UserID, bool DisableChangeTracker);
    }
}