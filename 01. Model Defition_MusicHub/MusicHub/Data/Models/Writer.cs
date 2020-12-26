using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Writer
    {
        //        Writer
        //•	Id – integer, Primary Key
        //•	Name– text with min length 3 and max length 20 (required)
        //•	Pseudonym – text, consisting of two words separated with space and each /word /must      start with one upper letter and continue with many lower-case /letters/(Example:  "Freddie    Mercury")
        //•	Songs – collection of type Song

        public Writer()
        {
            this.Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public string Pseudonym { get; set; }


        public virtual ICollection<Song> Songs { get; set; }

    }
}
