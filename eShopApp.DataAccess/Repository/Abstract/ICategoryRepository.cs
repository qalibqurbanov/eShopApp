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
    /// Kateqoriya ile elaqeli operasiyalari saxlayir.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetPopularCategories();
    }
}
