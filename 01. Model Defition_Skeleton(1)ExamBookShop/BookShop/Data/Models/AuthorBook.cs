using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookShop.Data.Models
{
    public class AuthorBook
    {
        //•	AuthorId - integer, Primary Key, Foreign key (required)
        //•	Author -  Author
        //•	BookId - integer, Primary Key, Foreign key(required)
        //•	Book - Book
        [ForeignKey(nameof(Author))]
        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }


        [ForeignKey(nameof(Book))]
        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
