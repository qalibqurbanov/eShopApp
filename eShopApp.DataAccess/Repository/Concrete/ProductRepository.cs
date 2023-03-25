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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace eShopApp.DataAccess.Repository.Concrete
{
    public class ProductRepository : Repository<Product, ShopDbContext>, IProductRepository
    {
        private readonly ShopDbContext _dbContext;
        public ProductRepository(ShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<Product> DbTable => _dbContext.Set<Product>();

        public List<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetTop5Products()
        {
            throw new NotImplementedException();
        }
    }
}
