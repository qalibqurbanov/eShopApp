using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApp.DataAccess.DatabaseContext;
using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopApp.DataAccess
{
    public class SeedDatabase
    {
        private static ShopDbContext _dbContext;
        public SeedDatabase(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static void Seed()
        {
            /* Icra olunmagi gozleyen migration yoxdursa ve ya migrationlar icra olunub qutariblarsa artiq gozleyen migration yoxdur ve sayi 0-dir demeli: */
            if (_dbContext.Database.GetPendingMigrations().Count() == 0)
            {
                /* Eger 'Categories' cedveli bowdursa: */
                if (_dbContext.Categories.Count() == 0)
                {
                    _dbContext.Categories.AddRange
                    (
                        new Category() { CategoryName = "Mobil telefonlar" },
                        new Category() { CategoryName = "Kompyuterler" },
                        new Category() { CategoryName = "Saatlar" }
                    );
                }

                /* Eger 'Products' cedveli bowdursa: */
                if (_dbContext.Products.Count() == 0)

                    _dbContext.Products.AddRange
                    (
                        new Product() { ProductName = "Samsung S72", ProductPrice = 2023, ProductImageUrl = "~/img/ProductImages/1.jpg", ProductDescription = "aliquam faucibus purus in massa tempor nec feugiat nisl pretium fusce id velit ut tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus faucibus ornare suspendisse sed nisi lacus sed viverra tellus in hac habitasse platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus.", ProductIsApproved = true },
                        new Product() { ProductName = "Samsung S24", ProductPrice = 1958, ProductImageUrl = "~/img/ProductImages/2.jpg", ProductDescription = "Cursus vitae congue mauris rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas maecenas pharetra convallis posuere morbi leo urna molestie at elementum eu facilisis sed odio morbi quis commodo odio aenean sed adipiscing diam donec adipiscing tristique.", ProductIsApproved = false },
                        new Product() { ProductName = "Samsung S180", ProductPrice = 1977, ProductImageUrl = "~/img/ProductImages/3.jpg", ProductDescription = "Nunc sed blandit libero volutpat sed cras ornare arcu dui vivamus arcu felis bibendum ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum fusce ut placerat orci nulla pellentesque dignissim enim sit amet venenatis urna cursus eget nunc scelerisque viverra mauris in aliquam sem fringilla ut morbi tincidunt augue.", ProductIsApproved = true },
                        new Product() { ProductName = "Samsung S35", ProductPrice = 2004, ProductImageUrl = "~/img/ProductImages/4.jpg", ProductDescription = "Tincidunt lobortis feugiat vivamus at augue eget arcu dictum varius duis at consectetur lorem donec massa sapien faucibus et molestie ac feugiat sed lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt ornare massa eget egestas purus viverra accumsan in nisl nisi scelerisque eu ultrices vitae auctor eu augue ut.", ProductIsApproved = false },
                        new Product() { ProductName = "Samsung S23", ProductPrice = 1999, ProductImageUrl = "~/img/ProductImages/5.jpg", ProductDescription = "Scelerisque in dictum non consectetur a erat nam at lectus urna duis convallis convallis tellus id interdum velit laoreet id donec ultrices tincidunt arcu non sodales neque sodales ut etiam sit amet nisl purus in mollis nunc sed id semper risus in hendrerit gravida rutrum quisque non tellus orci ac.", ProductIsApproved = true },
                        new Product() { ProductName = "Samsung S403", ProductPrice = 5000, ProductImageUrl = "~/img/ProductImages/6.jpg", ProductDescription = "Venenatis cras sed felis eget velit aliquet sagittis id consectetur purus ut faucibus pulvinar elementum integer enim neque volutpat ac tincidunt vitae semper quis lectus nulla at volutpat diam ut venenatis tellus in metus vulputate eu scelerisque felis imperdiet proin fermentum leo vel orci porta non pulvinar neque laoreet suspendissem.", ProductIsApproved = true },
                        new Product() { ProductName = "Samsung S151", ProductPrice = 7600, ProductImageUrl = "~/img/ProductImages/7.jpg", ProductDescription = "Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel risus commodo viverra maecenas accumsan lacus vel facilisis volutpat est velit egestas dui id ornare arcu odio ut sem nulla pharetra diam sit amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor elit sed vulputate mi sit amet mauris.", ProductIsApproved = true }
                    );

                _dbContext.SaveChanges();
            }
        }
    }
}