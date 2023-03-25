using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace eShopApp.DataAccess.Repository.Concrete.GenericRepositories
{
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

        public void Create(TEntity entity)
        {
            DbTable.Add(entity);

            _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DbTable.Remove(entity);

            _dbContext.SaveChanges();
        }

        public List<TEntity> GetAll()
        {
            return DbTable.ToList();
        }

        public TEntity GetByID(int ID)
        {
            TEntity entity = DbTable.Find(ID);

            return entity;
        }

        public void Update(TEntity entity)
        {
            DbTable.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
