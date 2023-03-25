using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.Entity.Entities;

namespace eShopApp.DataAccess.Repository.Abstract.GenericRepositories
{
    /// <summary>
    /// Repository interfeysleri ucun ortaq funksionalliqlari saxlayir.
    /// </summary>
    /// <typeparam name="TEntity">Istifade edilecek modelin tipi.</typeparam>
    public interface IRepository<TEntity>
    {
        TEntity GetByID(int ID);
        List<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
