using BookShop.Models.Constants;
using BookShop.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookShop.Models
{
    public class Book
    {
        //•	Book:
        //       o BookId
        // Title(up to 50 characters, unicode)
        // Description(up to 1000 characters, unicode)
        // ReleaseDate(not required)
        // Copies(an integer)
        // Price
        // EditionType – enum (Normal, Promo, Gold)
        //   AgeRestriction – enum (Minor, Teen, Adult)
        //   Author
        //   BookCategories

        public Book()
        {
            this.BookCategories = new HashSet<BookCategory>();

        }

        [Key]
        public int BookId { get; set; }

        [MaxLength(ConstantValuesLength.BookTitle)]
        public string Title { get; set; }

        [MaxLength(ConstantValuesLength.BookDescription)]
        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int Copies { get; set; }

        public decimal Price { get; set; }

        public EditionType EditionType { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
