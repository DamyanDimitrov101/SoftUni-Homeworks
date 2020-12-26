using P03_SalesDatabase.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Customer
    {
        //•	Customer:
        //	CustomerId
        //	Name(up to 100 characters, unicode)
        //	Email(up to 80 characters, not unicode)
        //	CreditCardNumber(string)
        //	Sales

        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }
        
        public int CustomerId { get; set; }

        [MaxLength(CommonData.CustomerNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(CommonData.CustomerEmailMaxLen)]
        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
