using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.Entity.Entities;

namespace eShopApp.Business.Services.Abstract
{
    public interface IProductService
    {
        Product GetByID(int ID);
        List<Product> GetAll();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}
