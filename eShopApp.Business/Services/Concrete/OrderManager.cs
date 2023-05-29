using eShopApp.Entity.Entities;
using eShopApp.Business.Services.Abstract;
using eShopApp.DataAccess.UnitOfWork.Abstract;

namespace eShopApp.Business.Services.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Create(Order entity)
        {
            _unitOfWork.Orders.Create(entity);
        }

        public List<Order> GetOrders(int UserID)
        {
            return _unitOfWork.Orders.GetOrders(UserID, true);
        }
    }
}