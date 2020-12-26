using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class GetSalesWithAppliedDiscountDTO
    {
        public CarDTO car { get; set; }

        public string customerName { get; set; }

        [NonSerialized]
        public decimal DiscountToDec;
        public string Discount { get; set; }

        [NonSerialized]
        public decimal priceToDec;
        public string price { get; set; }

        [NonSerialized]
        public decimal priceWithDiscountToDec;
        public string priceWithDiscount { get; set; }
    }
}
