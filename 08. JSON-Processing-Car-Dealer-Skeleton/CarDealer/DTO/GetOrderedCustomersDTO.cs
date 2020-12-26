using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class GetOrderedCustomersDTO
    {

        [NonSerialized]
        public DateTime Date;

        public string Name { get; set; }

        public string BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

    }
}
