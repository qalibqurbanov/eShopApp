using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.Entity.Entities;

namespace eShopApp.DataAccess.Repository.Abstract.GenericRepositories
{
    /// <summary>
    /// Repository interfeysleri ucun ortaq funksionalliqlarin skletini saxlayir.
    /// </summary>
    /// <typeparam name="TEntity">Istifade edilecek modelin tipi.</typeparam>
    public interface IRepository<TEntity>
    {
        TEntity GetByID(int ID, bool DisableChangeTracker);
        List<TEntity> GetAll(bool DisableChangeTracker);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Update(TEntity entity, int[] CategoryIDs);
        void Delete(TEntity entity);
    }
}