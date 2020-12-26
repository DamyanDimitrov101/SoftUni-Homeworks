using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookShop.DataProcessor.ImportDto
{
    public class ImportAuthorDTO
    {
        //•	Id - integer, Primary Key
        //•	FirstName - text with length[3, 30]. (required)
        //•	LastName - text with length[3, 30]. (required)
        //•	Email - text(required). Validate it! There is attribute for //this    job.
        //•	Phone - text.Consists only of three groups(separated by '-'), //the   first  two consist of three digits and the last one - of /4 /digits.    (required)
        //•	AuthorsBooks - collection of type AuthorBook


        //  "FirstName": "K",
        //  "LastName": "Tribbeck",
        //  "Phone": "808-944-5051",
        //  "Email": "btribbeck0@last.fm",
        //  "Books": 
        //      "Id": 79
        //      "Id": 40
        
        [MaxLength(30)]
        [MinLength(3)]
        [Required]
        public string FirstName { get; set; }


        [MaxLength(30)]
        [MinLength(3)]
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [RegularExpression(@"^(\d{3})-(\d{3})-(\d{4})$")]
        public string Phone { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public virtual ICollection<BookIdsDTO> Books { get; set; }
    }

    public class BookIdsDTO
    {
        [JsonProperty("Id")]
        public int? Id { get; set; }
    }
}
