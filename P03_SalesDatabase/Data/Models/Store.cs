using P03_SalesDatabase.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Store
    {
        //•	Store:
        //	StoreId
        //	Name(up to 80 characters, unicode)
        //	Sales

        public Store()
        {
            this.Sales = new HashSet<Sale>();
        }

        [Key]
        public int StoreId { get; set; }

        [MaxLength(CommonData.StoreNameMaxLen)]
        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
