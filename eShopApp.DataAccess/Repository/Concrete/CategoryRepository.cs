using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Concrete.GenericRepositories;
using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopApp.DataAccess.Repository.Concrete
{
    public class CategoryRepository : Repository<Category, ShopDbContext>, ICategoryRepository
    {
        private readonly ShopDbContext _dbContext;
        public CategoryRepository(ShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<Category> DbTable => _dbContext.Set<Category>();

        public List<Category> GetPopularCategories()
        {
            throw new NotImplementedException();
        }
    }
}