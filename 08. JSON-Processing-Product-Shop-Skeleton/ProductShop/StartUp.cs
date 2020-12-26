using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new ProductShopContext();

            //ResetDataBase(db);
            string inputJsonPath = @"../../../Datasets/Results/getUsersWithProducts.json";

            //string inputJson = File.ReadAllText(inputJsonPath);

            var result = GetUsersWithProducts(db);

            //Console.WriteLine(result);
            File.WriteAllText(inputJsonPath, result);
        }

        private static void ResetDataBase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database successfuly deleted!");

            db.Database.EnsureCreated();
            Console.WriteLine("Database successfully created!");
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert
                .DeserializeObject<List<Category>>(inputJson)
                .Where(c => c.Name != null)
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //Export Data

        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .ToList();

            string jsonProducts = JsonConvert.SerializeObject(productsInRange);

            return jsonProducts;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var usersWithSoldItem = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                                    .Where(p => p.Buyer != null)
                                    .Select(x => new
                                    {
                                        name = x.Name,
                                        price = x.Price,
                                        buyerFirstName = x.Buyer.FirstName,
                                        buyerLastName = x.Buyer.LastName
                                    })
                                    .ToList()
                })
                .OrderBy(x => x.lastName)
                .ThenBy(x => x.firstName)
                .ToList();

            string json = JsonConvert.SerializeObject(usersWithSoldItem, Formatting.Indented);

            return json;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = c.CategoryProducts
                        .Average(x => x.Product.Price)
                        .ToString("f2"),
                    totalRevenue = c.CategoryProducts
                        .Sum(x => x.Product.Price)
                        .ToString("f2")
                })
                .OrderByDescending(x => x.productsCount)
                .ToList();

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count,
                        products = u.ProductsSold
                                    .Where(p=>p.Buyer != null)
                                    .Select(p => new
                                    {
                                        name = p.Name,
                                        price=p.Price
                                    })
                                    .ToList()
                    }
                })
                .OrderByDescending(u=>u.soldProducts.count)
                .ToList();

            var allUsers = new
            {
                usersCount = users.Count,
                users = users
            };

            string json = JsonConvert.SerializeObject(allUsers,new JsonSerializerSettings 
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting= Formatting.Indented
            });

            return json;
        }
    }
}