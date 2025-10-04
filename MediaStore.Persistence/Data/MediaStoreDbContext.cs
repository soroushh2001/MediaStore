using MediaStore.Domain.Entities;
using MediaStore.Persistence.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Persistence.Data
{
    public class MediaStoreDbContext : DbContext
    {
        #region Constructor

        public MediaStoreDbContext(DbContextOptions<MediaStoreDbContext> options) : base(options)
        {

        }

        #endregion

        #region DbSets
        
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderDetail> OrderDetails { get; set; }
 
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
