using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.Data.Models
{
    public class User
    {
        //        User
        //•	Id – integer, Primary Key
        //•	Username – text with length[3, 20] (required)
        //•	FullName – text, which has two words, consisting of Latin letters.Both start with an /   upper /ббletter and are followed by lower letters.The two words are separated by a  single /   space (ex./ бб"John Smith") (required)
        //•	Email – text(required)
        //•	Age – integer in the range[3, 103] (required)
        //•	Cards – collection of type Card

        public User()
        {
            this.Cards = new HashSet<Card>();
        }

        [Key]
        public int Id { get; set; }
        
        [MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(([A-Z][a-z]+) ([A-Z][a-z]+))$")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(103)]
        [Required]
        public int Age { get; set; }


        public virtual ICollection<Card> Cards { get; set; }
    }
}
