using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete.GenericRepositories;

namespace eShopApp.DataAccess.Repository.Concrete
{
    /// <summary>
    /// Kateqoriya ile elaqeli operasiyalarin implementasiyasini saxlayir.
    /// </summary>
    public class CategoryRepository : Repository<Category, ShopDbContext>, ICategoryRepository
    {
        private readonly ShopDbContext _dbContext;
        public CategoryRepository(ShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<Category> DbTable => _dbContext.Set<Category>();

        public Category GetByIdWithProducts(int CategoryID, bool DisableChangeTracker)
        {
            var category = DbTable.AsQueryable();

            if (!string.IsNullOrEmpty(CategoryID.ToString()))
            {
                if (DisableChangeTracker)
                {
                    category = DbTable
                        .AsNoTracking()
                        .Where(cat => cat.CategoryID == CategoryID)
                        .Include(cat => cat.ProductCategories)
                        .ThenInclude(prodCat => prodCat.Product);
                }
                else
                {
                    category = DbTable
                        .Where(cat => cat.CategoryID == CategoryID)
                        .Include(cat => cat.ProductCategories)
                        .ThenInclude(prodCat => prodCat.Product);
                }
            }

            /* 'category' ozunde hem verdiyimiz ID-ye sahib kateqoriyani hemde hemin kateqoriyaya aid mehsullari saxlayir: */
            return category.FirstOrDefault();
        }

        public void DeleteProductFromCategory(int ProductID, int CategoryID)
        {
            ProductCategory dataToDelete = _dbContext.ProductCategory.FirstOrDefault(prodCat => prodCat.CategoryID == CategoryID && prodCat.ProductID == ProductID);
            if (dataToDelete != null)
            {
                _dbContext.ProductCategory.Remove(dataToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}