using GraphqlCoreDemo.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GraphqlCoreDemo.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>().HasData(
                new Store { StoreId = 1, Name = "Walmart" },
                new Store { StoreId = 2, Name = "Tarket" },
                new Store { StoreId = 3, Name = "Best Buy" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, StoreId = 1, Barcode = "123", Title = "Headphone", SellingPrice = 10.00m },
                new Product { ProductId = 2, StoreId = 1, Barcode = "456", Title = "Keyboard", SellingPrice = 20.00m },
                new Product { ProductId = 3, StoreId = 2, Barcode = "163", Title = "Monitor", SellingPrice = 30.99m },
                new Product { ProductId = 4, StoreId = 3, Barcode = "904", Title = "CPU", SellingPrice = 40.00m },
                new Product { ProductId = 5, StoreId = 1, Barcode = "938", Title = "Fan", SellingPrice = 50.00m },
                new Product { ProductId = 6, StoreId = 1, Barcode = "485", Title = "Motherboard", SellingPrice = 60.00m },
                new Product { ProductId = 7, StoreId = 2, Barcode = "304", Title = "WIFI-Adapter", SellingPrice = 70.00m },
                new Product { ProductId = 8, StoreId = 3, Barcode = "498", Title = "Mouse", SellingPrice = 80.99m },
                new Product { ProductId = 9, StoreId = 1, Barcode = "234", Title = "Speakers", SellingPrice = 90.00m },
                new Product { ProductId = 10, StoreId = 2, Barcode = "098", Title = "Television", SellingPrice = 10.99m },
                new Product { ProductId = 11, StoreId = 3, Barcode = "483", Title = "Xbox One", SellingPrice = 12.99m },
                new Product { ProductId = 12, StoreId = 1, Barcode = "211", Title = "Monitor", SellingPrice = 13.001m }
            );

        }
    }
}
