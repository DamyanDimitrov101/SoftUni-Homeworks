using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using P03_SalesDatabase.Data.Seeders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.Seeders
{
    public class ProductSeeder : ISeeder
    {
        private readonly SalesContext dbContext;
        private readonly Random random;
        public ProductSeeder(SalesContext salesContext,Random random)
        {
            this.dbContext = salesContext;
            this.random = random;
        }

        public void Seed()
        {
            string[] names = new string[]
            {
                "CPU"
                ,"Motherboard"
                ,"GPU"
                ,"RAM"
                ,"SSD"
                ,"HDD"
                ,"CD-RW"
            };

            ICollection<Product> products = new HashSet<Product>();

            for (int i = 0; i < 50; i++)
            {
                int nameIndex = this.random.Next(names.Length);
                string currentName = names[nameIndex];
                double currentQuantity = this.random.Next(1000);
                decimal currentPrice = this.random.Next(5000) * 1.247m;

                Product product = new Product()
                {
                    Name = currentName,
                    Quantity = currentQuantity,
                    Price = currentPrice
                };

                products.Add(product);

                Console.WriteLine($"Product: (Name: {currentName}) (Quantity: {currentQuantity}) (Price: {currentPrice})");
            }

            this.dbContext.Products.AddRange(products);
            this.dbContext.SaveChanges();
        }
    }
}
