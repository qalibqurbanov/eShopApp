using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.UnitOfWork.Abstract;
using Microsoft.Extensions.DependencyInjection;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;

namespace eShopApp.DataAccess.UnitOfWork.Concrete
{
    /// <summary>
    /// Concrete Repository siniflerini ozunde saxlayir, bundan elave DB ile elaqeli her bir emeliyyatin anliq olaraq DB-ya yansidilmasi evezine butun emeliyyatlari toplayaraq bir butun cem weklinde tek bir transaction uzerinden/daxilinde reallawdirmagi hedefleyen (ve belece DB-ya olan yuku de azaltmiw olan bir) metoda(Commit) sahibdir.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /* IoC Container-dan obyekt orneyi teleb edecem deye lazimdir: */
        private readonly IServiceProvider _serviceProvider; 

        /* Uzerinde iwleyeceyimiz Database: */
        private readonly ShopDbContext _dbContext;

        public UnitOfWork(ShopDbContext dbContext, IServiceProvider serviceProvider)
        {
            this._dbContext = dbContext;
            this._serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Entityler uzerinde edilmiw Insert/Update/Delete sorgusunu tek bir transaction daxilinde DB-ya gonderib execute eden funksiyadir.
        /// </summary>
        /// <returns>Geriye, sorgu neticesinde nece dene data/row-ya tesir olundugunu dondurecek.</returns>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// DbContext orneyi ucun ayrilmiw resurslari azad edir.
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        /* Awagida 'lazy loading' tetbiq edirem, yeni lazim oldu-olmadi Repository-ler HEAP-de yer ayrilmayacaq (eger Concrete Repositoryleri 'UnitOfWork' konstruktorunda initializasiya etseydim, her 'UnitOfWork'-un obyekt yaradiliwinda lazim oldu-olmadi her bir Repository ucun de bir obyekt yaradilacaq idi). Awagidaki public getter propertyler sayesinde yalniz lazim olan Repositoryleri initialize edirik, meselen: yalniz 'Products' cagirilanda yeni bir 'ProductRepository' orneyi IoC Containerdan elde olunaraq verilecek 'Products'-a ve elecede her bir diger repo, yalniz uygun public property cagirilanda initialize edilecek Repositorymiz: */

        /// <summary>
        /// IoC Containerdan teleb olunan(T) tipde bir obyekt elde edir.
        /// </summary>
        /// <typeparam name="T">IoC Containerdan ne tipli obyekt teleb edirik.</typeparam>
        /// <param name="member">IoC Containerdan elde olunmuw obyekt hara set olunsun ve ya bawqa sozle hara verilsin.</param>
        /// <returns>Geriye, IoC Containerdan elde etdiyi obyekti dondurur.</returns>
        private T InitializeService<T>(T member) => member ??= _serviceProvider.GetService<T>(); /* 'member' nulldursa, IoC-den 'T' orneyi elde ederek menimset 'member'-a ve geri dondur, eks halda 'member' geri dondurulsun */

        private readonly IProductRepository  _Products;
        private readonly ICategoryRepository _Categories;
        private readonly ICartRepository     _Carts;
        private readonly IOrderRepository    _Orders;

        public IProductRepository  Products   { get { return InitializeService(_Products);   } }
        public ICategoryRepository Categories { get { return InitializeService(_Categories); } }
        public ICartRepository     Carts      { get { return InitializeService(_Carts);      } }
        public IOrderRepository    Orders     { get { return InitializeService(_Orders);     } }

        /// <summary>
        /// Geriye concrete 'Repository' tipli sinif orneyi dondurur.
        /// </summary>
        /// <typeparam name="TEntity">Repository ile uzerinde iw goreceyimiz entity.</typeparam>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new eShopApp.DataAccess.Repository.Concrete.GenericRepositories.Repository<TEntity, ShopDbContext>(_dbContext);
        }
    }
}