using INStock.Contracts;
using System;

namespace INStock.Models
{
    public class Product : IProduct
    {
        public Product(string label, decimal price, int quantity)
        {
            if (string.IsNullOrEmpty(label))
            {
                throw new System.ArgumentNullException();
            }

            if (price<0)
            {
                throw new IndexOutOfRangeException();
            }
            if (quantity < 0)
            {
                throw new IndexOutOfRangeException();
            }
            this.Label = label;
            this.Price = price;
            this.Quantity = quantity;
        }

        public string Label { get;  set; }

        public decimal Price { get;  set; }

        public int Quantity { get; set; }

        public int CompareTo(IProduct other)
        {
            return this.Label.CompareTo(other.Label);
        }
    }
}
