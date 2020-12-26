﻿using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class BookCategory
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}