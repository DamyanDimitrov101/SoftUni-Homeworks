using BookShop.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Author
    {
        //•	Author:
        //o AuthorId
        //o FirstName(up to 50 characters, unicode, not required)
        //o LastName(up to 50 characters, unicode)

        public Author()
        {
            this.Books = new HashSet<Book>();
        }

            [Key]
        public int AuthorId { get; set; }

        [MaxLength(ConstantValuesLength.AuthorName)]
        public string FirstName { get; set; }

        [MaxLength(ConstantValuesLength.AuthorName)]
        public string LastName { get; set; }


        public virtual ICollection<Book> Books { get; set; }
    }
}
