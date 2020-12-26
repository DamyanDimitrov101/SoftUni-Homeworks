using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using ProductShop.XMLTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new ProductShopContext();

            //ResetDatabase(db);

            //string xmlPath = File.ReadAllText(@"../../../Datasets/categories-products.xml");

            string result = GetUsersWithProducts(db);

            //Console.WriteLine(result);

            File.WriteAllText("../../../Results/getUsersWithProducts.xml", result);
        }



        private static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }


        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            //Import the users from the provided file users.xml.

            var usersDTOs = XMLConverter.Deserializer<ImpUserDTO>(inputXml, "Users");

            var users = usersDTOs
                .Select(u => new User
            {
                FirstName=u.FirstName,
                LastName=u.LastName,
                Age=u.Age
            })
            .ToArray();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            //Import the products from the provided file products.xml.
            //Your method should return string with message $"Successfully imported {Products.Count}";

            var productsDTOs = XMLConverter.Deserializer<ImpProductDTO>(inputXml, "Products");

            var products = productsDTOs
                .Select(p => new Product
                {
                    Name = p.Name,
                    Price=p.Price,
                    SellerId = p.SellerId,
                    BuyerId = p.BuyerId
                })
                .ToList();
            ;
            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var categoriesDTOs = XMLConverter.Deserializer<ImpCategoryDTO>(inputXml, "Categories");

            var categories = categoriesDTOs
                .Where(c=>c.Name!=null)
                .Select(c=>new Category 
                {
                    Name=c.Name
                })
                .ToList();

            context.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var categoryProductsDTOs = XMLConverter.Deserializer<ImpCategoryProductDTO>(inputXml, "CategoryProducts");

            var categoryProducts = categoryProductsDTOs
                .Where(i =>
                            context.Categories.Any(c => c.Id == i.CategoryId) &&
                            context.Products.Any(p => p.Id == i.ProductId))
                .Select(cp => new CategoryProduct
                {
                    CategoryId = cp.CategoryId,
                    ProductId = cp.ProductId
                })
                .ToList();

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p=>p.Price >=500 && p.Price<=1000)
                .Select(p => new ExpProductDTO
            {
                Name=p.Name,
                Price=p.Price,
                Buyer = p.Buyer.FirstName+" "+ p.Buyer.LastName
            })
                .OrderBy(p=>p.Price)
                .Take(10)
                .ToList();

            var productsString = XMLConverter.Serialize(products, "Products");

            return productsString;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(u => new ExpUsers_GetSoldProductsDTO
                {
                     FirstName = u.FirstName
                    ,LastName = u.LastName
                    ,SoldProducts = u.ProductsSold.Select(ps=>new Products_GetSoldProductsDTO
                    {
                        Name=ps.Name
                        ,Price=ps.Price
                    })
                    .ToArray()
                })
                .OrderBy(u=>u.LastName)
                .ThenBy(u=>u.FirstName)
                .Take(5)
                .ToList();

            string result = XMLConverter.Serialize(users, "Users");

            return result;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new ExpCategories_GetCategoriesByProductsCountDTO
                {
                    Name=c.Name
                    ,Count = c.CategoryProducts.Count()
                    ,AveragePrice = c.CategoryProducts.Average(s=>s.Product.Price)
                    ,TotalRevenue = c.CategoryProducts.Sum(s=> s.Product.Price)
                })
                .OrderByDescending(p=>p.Count)
                .ThenBy(p=>p.TotalRevenue)
                .ToList();

            string result = XMLConverter.Serialize(categories, "Categories");

            return result;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            //Select users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest). Select only their first and last name, age, count of sold products and for each product - name and price sorted by price (descending). Take top 10 records.

            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(x => new ExpUsers_GetUsersWithProductsDTO
                {
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                    Age=x.Age,
                    SoldProducts= new ExpSoldProductsDTO 
                    {   
                        Count = x.ProductsSold.Count(),
                        Products = x.ProductsSold.Select(p=> new ExpProduct_UsersDTO
                        {
                           Name = p.Name,
                           Price = p.Price
                        })
                        .OrderByDescending(p=>p.Price)
                        .ToArray()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .Take(10)
                .ToArray();

            var final = new ExpGetUsersWithProductsDTO
            {
                Count = context.Users.Count(u=>u.ProductsSold.Any()),
                Users = users
            };


            return XMLConverter.Serialize(final, "Users");
        }
    }
}