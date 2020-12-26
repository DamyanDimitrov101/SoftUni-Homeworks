using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Performer
    {
        //        Performer
        //•	Id – integer, Primary Key
        //•	FirstName– text with min length 3 and max length 20 (required) 
        //•	LastName– text with min length 3 and max length 20 (required) 
        //•	Age – integer(in range between 18 and 70) (required)
        //•	NetWorth – decimal (non-negative, minimum value: 0) (required)
        //•	PerformerSongs - collection of type SongPerformer
        //

        public Performer()
        {
            this.PerformerSongs = new HashSet<SongPerformer>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal NetWorth { get; set; }

        public virtual ICollection<SongPerformer> PerformerSongs { get; set; }
    }
}
