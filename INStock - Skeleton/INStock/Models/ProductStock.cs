using INStock.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace INStock.Models
{
    public class ProductStock : IProductStock
    {
        private List<IProduct> products;

        public ProductStock()
        {
            this.products = new List<IProduct>();
        }

        public IProduct this[int index] { get => products[index]; set => this[index]=value; }

        public int Count => this.products.Count;

        public void Add(IProduct product)
        {
            if (product!=null)
            {
                this.products.Add(product);
            }
        }

        public bool Contains(IProduct product)
        {

            return this.products.Contains(product);
        }

        public IProduct Find(int index)
        {
            if (this.Count<=index||index<0)
            {
                throw new IndexOutOfRangeException();
            }
            return this[index];
        }

        public IEnumerable<IProduct> FindAllByPrice(double price)
        {
            var priceToDecimal = decimal.Parse(price.ToString());
            IEnumerable<IProduct> collectionOfMatches = this.products.Where(p => p.Price == priceToDecimal);

            if (collectionOfMatches.Count()==0)
            {
                throw new InvalidOperationException();
            }

            return collectionOfMatches;
        }

        public IEnumerable<IProduct> FindAllByQuantity(int quantity)
        {
            IEnumerable<IProduct> collOfMatches = this.products.Where(p => p.Quantity == quantity);

            int countOfElements = collOfMatches.Count();

            if (countOfElements==0)
            {
                throw new InvalidOperationException();
            }

            return collOfMatches;
        }

        public IEnumerable<IProduct> FindAllInRange(double lo, double hi)
        {
            decimal loToDecimal = decimal.Parse(lo.ToString());
            decimal hiToDecimal = decimal.Parse(hi.ToString());

            IEnumerable<IProduct> collOfMatches = this.products.Where(p => p.Price >= loToDecimal).Where(p=>p.Price<=hiToDecimal).OrderBy(p=>p.Price);

            if (!collOfMatches.Any())
            {
                throw new InvalidOperationException();
            }

            return collOfMatches;
        }

        public IProduct FindByLabel(string label)
        {
            IProduct actual = this.products.First(p => p.Label == label);

            if (actual==null)
            {
                throw new InvalidOperationException();
            }

            return actual;
        }

        public IProduct FindMostExpensiveProduct()
        {
            IProduct product = this.products.OrderByDescending(p=>p.Price).First();

            if (product is null)
            {
                throw new InvalidOperationException();
            }

            return product;
        }

        public IEnumerator<IProduct> GetEnumerator()
        {
            return this.products.GetEnumerator();
        }

        public bool Remove(IProduct product)
        {
            if (this.products.Count==0)
            {
                throw new InvalidOperationException();
            }

            return this.products.Remove(product);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.products.GetEnumerator();
        }
    }
}
