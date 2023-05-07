using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;
using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopApp.DataAccess.Repository.Concrete.GenericRepositories
{
    /// <summary>
    /// Konkret Repository siniflerinin her birinde gorulecek olan ortaq iwleri her birinde tekrarlamamaq ucun bu sinifde yazmiwam.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<TEntity> DbTable => _dbContext.Set<TEntity>();

        public List<TEntity> GetAll(bool DisableChangeTracker)
        {
            return DbTable.ToList();
        }

        public TEntity GetByID(int ID, bool DisableChangeTracker)
        {
            return DbTable.Find(ID);
        }

        public void Update(TEntity entity)
        {
            /* 
                * System.InvalidOperationException: 'The instance of entity type 'Product' cannot be tracked because another instance with the same key value for {'ProductID'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.'

                - Eyni xeta hem Product hemde Category editleyen zaman yaranir.

                * Bu sebeble hazirda movcud izlenilen Product/Category entity-sini izlenmekden cixarmali ve daha sonra hazirki entitymi update etmeliyem.
            */

            if (entity is Product prod)
            {
                /* Change Tracker terefinden izlenilmekde olan diger 'Product' entity-sini elde edek. Bunun ucun in-memory-de movcud olan(bir baxima cachelenmiwde deye bilerik) 'Product' entitylerinden lazimi ID-ye sahib 'Product'-i tapib cixariram (tapib cixarmaq ucun gorunduyu kimi - in-memorydeki kewlenmiw 'Product' ile 'Update()' metoduna gelmiw 'Product'-i qarwilawdiriram): */
                Product? product = _dbContext.Set<Product>()
                            .Local
                            .FirstOrDefault(entry => entry.ProductID.Equals(prod.ProductID));

                if (product != null)
                {
                    /* Tapilan mehsulu Change Trackerin izleme sisteminden cixariram, yeni Change Tracker artiq izlemesin hemin bu 'product'-da olan deyiwiklikleri: */
                    _dbContext.Entry(product).State = EntityState.Detached;
                }

                /* Oz entity-mi ise update edirem: */
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }

            else if(entity is Category cat)
            {
                /* Change Tracker terefinden izlenilmekde olan diger 'Category' entity-sini elde edek. Bunun ucun in-memory-de movcud olan(bir baxima cachelenmiwde deye bilerik) 'Category' entitylerinden lazimi ID-ye sahib 'Category'-ni tapib cixariram (tapib cixarmaq ucun gorunduyu kimi - in-memorydeki kewlenmiw 'Category' ile 'Update()' metoduna gelmiw 'Category'-ni qarwilawdiriram): */
                Category? category = _dbContext.Set<Category>()
                            .Local
                            .FirstOrDefault(entry => entry.CategoryID.Equals(cat.CategoryID));

                if (category != null)
                {
                    /* Tapilan kateqoriyani Change Trackerin izleme sisteminden cixariram, yeni Change Tracker artiq izlemesin hemin bu 'category'-da olan deyiwiklikleri: */
                    _dbContext.Entry(category).State = EntityState.Detached;
                }

                /* Oz entity-mi ise update edirem: */
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public void Update(TEntity entity, int[] CategoryIDs)
        {
            if (entity is Product prod)
            {
                /* Ilk once lazimi mehsulu ve hemin mehsula aid kateqoriyalari elde edirem: */
                Product? product = (_dbContext as ShopDbContext).Products
                    .Include(p => p.ProductCategories)
                    .FirstOrDefault(p => p.ProductID == prod.ProductID);

                if (product != null)
                {
                    product.ProductName = prod.ProductName;
                    product.ProductPrice = prod.ProductPrice;
                    product.ProductDescription = prod.ProductDescription;
                    product.ProductImageName = prod.ProductImageName;
                    product.ProductIsApproved = prod.ProductIsApproved;
                    product.ProductIsHome = prod.ProductIsHome;
                    product.ProductCategories = CategoryIDs.Select(catID => new ProductCategory()
                    {
                        /* Burada mehsulun kateqoriyalarini set edirik. 'CategoryIDs' massivinden elde etdiyimiz her bir ID esasinda yeni bir 'ProductCategory' obyekti yaradaraq veririk 'List<ProductCategory> ProductCategories' kolleksiyasina. Netice olaraq 'ProductCategories' ozunde mehsulun kateqoriyalarini saxlamiw olacaq: */

                        ProductID = prod.ProductID,
                        CategoryID = catID
                    }).ToList();

                    _dbContext.SaveChanges();
                }
            }
        }

        public void Delete(TEntity entity)
        {
            DbTable.Remove(entity);

            _dbContext.SaveChanges();
        }

        public void Create(TEntity entity)
        {
            DbTable.Add(entity);

            _dbContext.SaveChanges();
        }


        public void Create(TEntity entity, int[] CategoryIDs)
        {
            if (entity is Product prod)
            {
                if (prod != null)
                {
                    prod.ProductCategories = CategoryIDs.Select(catID => new ProductCategory()
                    {
                        /* Burada mehsulun kateqoriyalarini set edirik. 'CategoryIDs' massivinden elde etdiyimiz her bir ID esasinda yeni bir 'ProductCategory' obyekti yaradaraq veririk 'List<ProductCategory> ProductCategories' kolleksiyasina. Netice olaraq 'ProductCategories' ozunde mehsulun kateqoriyalarini saxlamiw olacaq: */

                        ProductID = prod.ProductID,
                        CategoryID = catID
                    }).ToList();

                    Create(prod as TEntity);

                    _dbContext.SaveChanges();
                }
            }


            _dbContext.SaveChanges();
        }
    }
}