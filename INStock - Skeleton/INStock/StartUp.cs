namespace INStock
{
    using INStock.Contracts;
    using INStock.Models;
    using System;
    using System.Collections.Generic;

    public static class StartUp
    {
        public static void Main(string[] args)
        {
            ProductStock ps = new ProductStock();

            Product product = new Product("Tin",3,8);
           
            ps.Add(product);

            IEnumerable<IProduct> act = ps.FindAllInRange(1, 4);

        }
    }
}
