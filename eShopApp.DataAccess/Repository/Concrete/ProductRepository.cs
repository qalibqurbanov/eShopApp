using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete.GenericRepositories;
using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopApp.DataAccess.Repository.Concrete
{
    /// <summary>
    /// Mehsul ile elaqeli operasiyalarin implementasiyasini saxlayir.
    /// </summary>
    public class ProductRepository : Repository<Product, ShopDbContext>, IProductRepository
    {
        private readonly ShopDbContext _dbContext;
        public ProductRepository(ShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<Product> DbTable => _dbContext.Set<Product>();

        public Product GetProductDetails(int ID, bool DisableChangeTracker)
        {
            if (DisableChangeTracker)
            {
                return _dbContext.Products
                        .AsNoTracking()
                        .Where(prod => prod.ProductID == ID)
                        .Include(prod => prod.ProductCategories)
                        .ThenInclude(cat => cat.Category)
                        .FirstOrDefault();
            }
            else
            {
                return _dbContext.Products
                        .Where(prod => prod.ProductID == ID)
                        .Include(prod => prod.ProductCategories)
                        .ThenInclude(cat => cat.Category)
                        .FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategoryID(int? CategoryID, int Page, int ProductCountByPage, bool DisableChangeTracker)
        {
            /* 'Product' cedvelini icra olunmamiw sorgu weklinde elde edirem: */
            var products = DbTable.AsQueryable();

            if (!string.IsNullOrEmpty(CategoryID.ToString()))
            {
                if (DisableChangeTracker)
                {
                    products = products
                        .AsNoTracking()
                        .Where(prod => prod.ProductIsApproved)
                        .Include(prod => prod.ProductCategories)
                        .ThenInclude(prodCat => prodCat.Category)
                        .Where(prod => prod.ProductCategories.Any(prod => prod.CategoryID == CategoryID)); /* Where geriye gosterdiyimiz CategoryID-ye sahib 'Product'-lari dondurecek */
                }
                else
                {
                    products = products
                        .Where(prod => prod.ProductIsApproved)
                        .Include(prod => prod.ProductCategories)
                        .ThenInclude(prodCat => prodCat.Category)
                        .Where(prod => prod.ProductCategories.Any(prod => prod.CategoryID == CategoryID)); /* Where geriye gosterdiyimiz CategoryID-ye sahib 'Product'-lari dondurecek */
                }
            }

            /* Niye "(Page - 1) * ProductCountByPage" : Eger 'Page' verilmese, Controllerden bura optional Page parametrinde olan 1 deyeri gelecek ve 0-da 'ProductCountByPage' -e vururuq, netice olacaq 0. Yeni, 'Page' verilmese hec bir datani skipleme - demiw oluruq */
            return products.Skip((Page - 1) * ProductCountByPage).Take(ProductCountByPage).ToList();
        }

        public int GetProductCountByCategoryID(int? CategoryID, bool DisableChangeTracker)
        {
            /* 'Product' cedvelini icra olunmamiw sorgu weklinde elde edirem: */
            var products = DbTable.AsQueryable();

            if (!string.IsNullOrEmpty(CategoryID.ToString()))
            {
                if (DisableChangeTracker)
                {
                    products = products
                        .AsNoTracking()
                        .Where(prod => prod.ProductIsApproved)
                        .Include(prod => prod.ProductCategories)
                        .ThenInclude(prodCat => prodCat.Category)
                        .Where(prod => prod.ProductCategories.Any(prod => prod.CategoryID == CategoryID)); /* Where geriye gosterdiyimiz CategoryID-ye sahib 'Product'-lari dondurecek */
                }
                else
                {
                    products = products
                        .Where(prod => prod.ProductIsApproved)
                        .Include(prod => prod.ProductCategories)
                        .ThenInclude(prodCat => prodCat.Category)
                        .Where(prod => prod.ProductCategories.Any(prod => prod.CategoryID == CategoryID)); /* Where geriye gosterdiyimiz CategoryID-ye sahib 'Product'-lari dondurecek */
                }
            }

            return products.Count();
        }

        public List<Product> GetHomePageProducts(bool DisableChangeTracker)
        {
            var products = DbTable.AsQueryable();

            if(DisableChangeTracker)
            {
                products = products
                    .AsQueryable()
                    .Where(prod => prod.ProductIsHome == true && prod.ProductIsApproved == true);
            }
            else
            {
                products = products
                    .Where(prod => prod.ProductIsHome == true && prod.ProductIsApproved == true);
            }

            return products.ToList();
        }
    }
}