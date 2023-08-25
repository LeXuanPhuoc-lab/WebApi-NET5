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
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }
        #endregion

        // Fluent API 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>(e => {
                e.ToTable("Orders");
                e.HasKey(o => o.OrderId);
                e.Property(o => o.OrderDate).HasDefaultValueSql("getutcdate()");
                //e.Property(o => o.TotalPrice).IsRequired();
            });

            modelBuilder.Entity<OrderDetailEntity>(e => {
                e.ToTable("OrderDetails");
                e.HasKey(od => new { od.OrderId, od.ProductId});
                // map relationship
                e.HasOne(od => od.order)
                 .WithMany(o => o.orderDetails)
                 .HasForeignKey(e => e.OrderId)
                 .HasConstraintName("FK_OrderDetails_OrderId");

                e.HasOne(od => od.product)
                 .WithMany(p => p.orderDetails)
                 .HasForeignKey(e => e.ProductId)
                 .HasConstraintName("FK_OrderDetails_ProductId");
            });
        }
    }
}
