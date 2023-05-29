using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;

namespace eShopApp.DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }

        public int Commit();
    }
}