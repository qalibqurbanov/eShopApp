using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.DataAccess.Repository.Abstract;
using eShopApp.DataAccess.Repository.Abstract.GenericRepositories;
using eShopApp.Entity.Entities;

namespace eShopApp.Business.Services.Abstract
{
    public interface IProductService : IRepository<Product>, IProductRepository
    {
        
    }
}
