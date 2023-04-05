using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;
using eShopApp.Entity.Entities;

namespace eShopApp.DataAccess.Repository.Abstract
{
    /// <summary>
    /// Mehsul ile elaqeli operasiyalarin skletini saxlayir.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(int ID);
        List<Product> GetProductsByCategoryID(int? CategoryID);
    }
}
