using Microsoft.EntityFrameworkCore;
using System;
using BookShop.Data.ConnectionManager;
using BookShop.Models;
using BookShop.Data.EntityConfigurations;

namespace BookShop.Data
{
    public class BookShopContext : DbContext
    {
        public BookShopContext()
        {

        }

        public BookShopContext(DbContextOptions contextOptions)
            : base(contextOptions)
        {

        }

        public DbSet<Author> Authors{ get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory>  BookCategories { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConnection.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookCategoryConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
        }
    }
}
