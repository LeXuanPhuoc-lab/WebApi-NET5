using FirstWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Heplers
{
    public class DbContextHelper : DbContext
    {
        public DbContextHelper(DbContextOptions options) : base(options) { }

        // generate DB table 
        #region
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        #endregion
    }
}
