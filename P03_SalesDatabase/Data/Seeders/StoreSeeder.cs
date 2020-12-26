using P03_SalesDatabase.Data.Models;
using P03_SalesDatabase.Data.Seeders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.Seeders
{
    public class StoreSeeder : ISeeder
    {
        private readonly SalesContext salesContext;
        

        public StoreSeeder(SalesContext salesContext)
        {
            this.salesContext = salesContext;
        }

        public void Seed()
        {
            Store[] stores = new Store[]
            {
                new Store(){ Name = "PCStoreBurgas"},
                new Store(){ Name = "PCStoreSofia"},
                new Store(){ Name = "MedenKabelStore"},
                new Store(){ Name = "SarafovoStore"},
                new Store(){ Name = "PCStorePleven"},
            };

            this.salesContext.Stores.AddRange(stores);
            this.salesContext.SaveChanges();
        }
    }
}
