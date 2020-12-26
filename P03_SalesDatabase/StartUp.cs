using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Seeders;
using P03_SalesDatabase.Data.Seeders.Contracts;
using System;
using System.Collections.Generic;

namespace P03_SalesDatabase
{
    class StartUp
    {
        static void Main(string[] args)
        {
            SalesContext salesContext = new SalesContext();

            Random random = new Random();

            ICollection<ISeeder> seeders = new List<ISeeder>();

            seeders.Add( new ProductSeeder(salesContext,random));
            seeders.Add( new StoreSeeder(salesContext));

            foreach (var seeder in seeders)
            {
                seeder.Seed();
            }
        }
    }
}
