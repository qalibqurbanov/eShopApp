using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;
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
            TEntity entity = DbTable.Find(ID);

            return entity;
        }

        public void Update(TEntity entity)
        {
            DbTable.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
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
    }
}
