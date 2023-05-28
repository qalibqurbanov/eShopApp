using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete.GenericRepositories;

namespace eShopApp.DataAccess.Repository.Concrete
{
    /// <summary>
    /// Mehsul alma emeliyyatlarinin implementasiyasini saxlayir.
    /// </summary>
    public class OrderRepository : Repository<Order, ShopDbContext>, IOrderRepository
    {
        private readonly ShopDbContext _dbContext;
        public OrderRepository(ShopDbContext dbContext) : base(dbContext) 
        {
            this._dbContext = dbContext;
        }

        private DbSet<Order> DbTable => _dbContext.Set<Order>();

        public List<Order> GetOrders(int UserID, bool DisableChangeTracker)
        {
            IQueryable<Order> result = DbTable.AsQueryable();

            /* Sehifede 'Order (sifariw)'-leri gosterecem deye hemin sifariwe aid uygun 'OrderItems' ve 'Product' datalarini da elave edirem sorguya: */
            if(DisableChangeTracker)
            {
                result = _dbContext.Orders
                    .AsNoTracking()
                    .Where(order => order.UserID == UserID)
                    .Include(order => order.OrderItems)
                    .ThenInclude(orderItem => orderItem.Product)
                    .AsQueryable();
            }
            else
            {
                result = _dbContext.Orders
                    .Where(order => order.UserID == UserID)
                    .Include(order => order.OrderItems)
                    .ThenInclude(orderItem => orderItem.Product)
                    .AsQueryable();
            }

            /* Orderleri dondururem geri: */
            if(string.IsNullOrEmpty(UserID.ToString()) && UserID > -1) /* Her hansi bir userin orderleri istenilse, hemin usere aid olan orderleri elde edek: */
            {
                return result.Where(order => order.UserID == UserID).ToList();
            }
            else /* 'UserID' verilmeyibse demeli her hansi bir usere aid orderler yox, butun orderler elde edilmek istenilir: */
            {
                 return result.ToList();
            }
        }
    }
}