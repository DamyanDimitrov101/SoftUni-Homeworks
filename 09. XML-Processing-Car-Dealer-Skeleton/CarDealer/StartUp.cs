using CarDealer.Data;
using CarDealer.DataTransferObjects.Export;
using CarDealer.DataTransferObjects.Import;
using CarDealer.Models;
using ProductShop.XMLTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new CarDealerContext();

            //ResetDatabase(db);

            //string xmlAsString = File.ReadAllText("../../../Datasets/sales.xml");

            string result = GetSalesWithAppliedDiscount(db);

            File.WriteAllText("../../../Datasets/Results/getSalesWithAppliedDiscount.xml", result);

            Console.WriteLine(result);
        }

        private static void ResetDatabase(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine("Database was reset!");
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var result = XMLConverter.Deserializer<SupplierDTO>(inputXml, "Suppliers");

            var suppliers = result.Select(s => new Supplier
            {
                Name = s.Name,
                IsImporter = s.IsImporter
            })
            .ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var partsDTO = XMLConverter.Deserializer<PartDTO>(inputXml, "Parts");

            var parts = partsDTO
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .Select(p => new Part
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = (int)p.SupplierId
                })
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsDTOs = XMLConverter.Deserializer<CarDTO>(inputXml, "Cars");

            var cars = new List<Car>();

            foreach (var car in carsDTOs)
            {
                var uniqueParts = car.Parts.Select(p => p.Id).Distinct().ToArray();
                var existingParts = uniqueParts.Where(id => context.Parts.Any(x => x.Id == id));


                var carResult = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TraveledDistance,
                    PartCars = existingParts.Select(p => new PartCar
                    {
                        PartId = p
                    })
                    .ToArray()
                };

                cars.Add(carResult);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customerDTOs = XMLConverter.Deserializer<CustomerDTO>(inputXml, "Customers");

            var customers = customerDTOs.Select(c => new Customer
            {
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver
            })
                .ToArray();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var salesDTOs = XMLConverter.Deserializer<SaleDTO>(inputXml, "Sales");

            var sales = salesDTOs
                .Where(s => context.Cars.Any(c => c.Id == s.CarId))
                .Select(s => new Sale
                {
                     CustomerId = s.CustomerId,
                     CarId = s.CarId,
                     Discount = s.Discount
                })
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .Select(c => new GetCarsWithDistanceDTO
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(x=>x.Make)
                .ThenBy(x=>x.Model)
                .Take(10)
                .ToArray();

            var xml = XMLConverter.Serialize(cars, "cars");

            return xml;

        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var bmw = context.Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarsFromMakeBmwDTO
                {
                        Id = c.Id,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    
                })
                .ToArray();

            return XMLConverter.Serialize(bmw, "cars");
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new GetLocalSuppliersDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count                    
                })
                .ToArray();

            return XMLConverter.Serialize(suppliers, "suppliers");
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            //Get all cars along with their list of parts. For the car get only make, model and travelled distance and for the parts get only name and price and sort all parts by price (descending). Sort all cars by travelled distance (descending) then by model (ascending). Select top 5 records.

            var car = context.Cars
                .Select(c => new GetCarsWithTheirListOfPartsDTO
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(pc=> new PartCarDTO
                    {
                       Name = pc.Part.Name,
                       Price = pc.Part.Price
                    })
                    .OrderByDescending(p=>p.Price)
                    .ToArray()
                })
                .OrderByDescending(c=>c.TravelledDistance)
                .ThenBy(c=>c.Model)
                .Take(5)
                .ToArray();

            return XMLConverter.Serialize(car, "cars");
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any())
                .ToArray();

            var totalCustomers = new List<GetTotalSalesByCustomerDTO>();

            foreach (var c in customers)
            {
                var customerDto = new GetTotalSalesByCustomerDTO
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Sum(s=>s.Car.PartCars.Sum(pc=>pc.Part.Price))
                };

                totalCustomers.Add(customerDto);
            }

            totalCustomers = totalCustomers
                .OrderByDescending(c => c.SpentMoney)
                .ToList();

            return XMLConverter.Serialize(totalCustomers.ToArray(), "customers");
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Where(s=>context.Cars.Any(c=>c.Id==s.Car.Id))
                .Select(s=>new GetSalesWithAppliedDiscountDTO
            {
                Car = new CarSalesDTO
                {
                   Make = s.Car.Make,
                   Model = s.Car.Model,
                   TravelledDistance = s.Car.TravelledDistance
                },
                Discount = s.Discount,
                CustomerName = s.Customer.Name,
                Price = s.Car.PartCars.Sum(c=>c.Part.Price),
                PriceWithDiscount = s.Car.PartCars.Sum(c => c.Part.Price) - ((s.Car.PartCars.Sum(c => c.Part.Price) * (s.Discount/100)))
            })
            .ToArray();

            return XMLConverter.Serialize(sales, "sales");
        }
    }
}