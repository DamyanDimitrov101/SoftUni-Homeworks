using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Album
    {
        //        Album
        //•	Id – integer, Primary Key
        //•	Name – text with min length 3 and max length 40 (required)
        //•	ReleaseDate – Date(required)
        //•	Price – calculated property(the sum of all song prices in the album)
        //•	ProducerId – integer foreign key
        //•	Producer – the album’s producer
        //•	Songs – collection of all songs in the album

        public Album()
        {
            this.Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(40)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public decimal Price => this.Songs.Sum(s => s.Price);

        [ForeignKey("Producer")]
        public int? ProducerId { get; set; }

        public virtual Producer Producer { get; set; }


        public virtual ICollection<Song> Songs { get; set; }
    }

}
