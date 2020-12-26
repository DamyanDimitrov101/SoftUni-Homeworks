using P03_SalesDatabase.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        //•	Product:
        //	ProductId
        //	Name(up to 50 characters, unicode)
        //	Quantity(real number)
        //	Price
        //	Sales

        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }

        [Key]
        public int ProductId { get; set; }

        [MaxLength(CommonData.ProductNameMaxLen)]
        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }


        [MaxLength(CommonData.ProductDescriptionMaxLen)]
        public string Description { get; set; }
    }
}
