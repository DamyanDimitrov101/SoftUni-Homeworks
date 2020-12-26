using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Common;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
                
        }

        public SalesContext(DbContextOptions contextOptions)
            :base(contextOptions)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales{ get; set; }
        public DbSet<Store> Stores{ get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(CommonData.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                .Property("Email")
                .IsUnicode(false);

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity
                .Property("Description")
                .HasDefaultValue("No description");

            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

                entity
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);

                entity
                .HasOne(s => s.Store)
                .WithMany(st => st.Sales)
                .HasForeignKey(s => s.SaleId);

                entity.Property(s => s.Date)
                .HasDefaultValueSql("GETDATE()");
            });

        }
    }
}
