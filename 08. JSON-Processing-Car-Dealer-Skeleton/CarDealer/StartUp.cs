using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            InitializeMapper();
            var context = new CarDealerContext();

            //ResetDatabase(context);
            
            //string inputJson = File.ReadAllText("../../../Datasets/sales.json");

            string result = GetSalesWithAppliedDiscount(context);

            Console.WriteLine(result);

            CheckIfDirectoryExists();

            File.WriteAllText("../../../Datasets/Results/getSalesWithAppliedDiscount.json", result);
        }

        private static void CheckIfDirectoryExists()
        {
            if (!Directory.Exists("../../../Datasets/Results"))
            {
                Directory.CreateDirectory("../../../Datasets/Results");
            }
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
        }

        private static void ResetDatabase(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database successfully deleted!");

            db.Database.EnsureCreated();
            Console.WriteLine("Database successfully created!");
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert
                .DeserializeObject<List<Supplier>>(inputJson)
                .ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts =JsonConvert
                .DeserializeObject<List<Part>>(inputJson);

            var supplierMaxId = context.Suppliers.Select(s => s.Id).Max();

            int countAdded = 0;
            foreach (var part in parts)
            {
                if (part.SupplierId<=supplierMaxId)
                {
                    context.Parts.Add(part);
                    countAdded++;
                }
            }

            context.SaveChanges();

            return $"Successfully imported {countAdded}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<List<Car>>(inputJson);

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .ProjectTo<GetOrderedCustomersDTO>()
                .OrderBy(c=>c.Date)
                .ThenBy(c=>c.IsYoungDriver)
                .ToList();

            var json = JsonConvert.SerializeObject(customers,Formatting.Indented);

            return json;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotas = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c=>c.TravelledDistance)
                .ProjectTo<GetCarsFromMakeToyotaDTO>()
                .ToList();

            var json = JsonConvert.SerializeObject(toyotas, Formatting.Indented);

            return json;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                            .Where(s => s.IsImporter == false)
                            .ProjectTo<GetLocalSuppliersDTO>()
                            .ToList();

            string json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return json;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars.Select(x=> new 
                    {
                        x.Part.Name,
                        Price = x.Part.Price.ToString("F2")
                    })
                    .ToList()
                })
                .ToList();

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = c.Sales
                                    .Sum(s => s.Car.PartCars.Sum(pc=>pc.Part.Price))
                })
                .Where(c => c.boughtCars > 0)
                .ToList();

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented); 

            return json;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                    .Take(10)
                    //.Select(s => new
                    //{
                    //    car =  new
                    //    {
                    //        s.Car.Make,
                    //        s.Car.Model,
                    //        s.Car.TravelledDistance
                    //    },
                    //    customerName = s.Customer.Name,
                    //    Discount = s.Discount,
                    //    price = s.Car.PartCars.Sum(pc=>pc.Part.Price),
                    //    priceWithDiscount = (s.Car.PartCars.Sum(pc => pc.Part.Price))
                    //})
                    .ProjectTo<GetSalesWithAppliedDiscountDTO>()
                    .ToList();

            foreach (var sale in sales)
            {
                sale.priceWithDiscountToDec -=  sale.priceToDec * (sale.DiscountToDec * 0.01m);

                sale.price = sale.priceToDec.ToString("F2");
                sale.priceWithDiscount = sale.priceWithDiscountToDec.ToString("F2");
                sale.Discount = sale.DiscountToDec.ToString();
            }

            string json = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return json;
        }
    }
}