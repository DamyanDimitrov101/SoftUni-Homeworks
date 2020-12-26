using BookShop.Models.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Category
    {
        //•	Category:
        // CategoryId
        //o Name(up to 50 characters, unicode)
        //o CategoryBooks
        public Category()
        {
            this.CategoryBooks = new HashSet<BookCategory>();

        }

        [Key]
        public int CategoryId { get; set; }

        [MaxLength(ConstantValuesLength.CategoryTitle)]
        public string Name { get; set; }

        public virtual ICollection<BookCategory> CategoryBooks { get; set; }
    }
}