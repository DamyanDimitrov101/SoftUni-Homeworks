using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Song
    {
        //        Song
        //•	Id – integer, Primary Key
        //•	Name – text with min length 3 and max length 20 (required)
        //•	Duration – TimeSpan(required)
        //•	CreatedOn – Date(required)
        //•	Genre ¬– Genre enumeration with possible values: "Blues, Rap, PopMusic, Rock,       Jazz" (required)
        //•	AlbumId– integer foreign key
        //•	Album– the song’s album
        //•	WriterId - integer, foreign key(required)
        //•	Writer – the song’s writer
        //•	Price – decimal (non-negative, minimum value: 0) (required)
        //•	SongPerformers - collection of type SongPerformer

        public Song()
        {
            this.SongPerformers = new HashSet<SongPerformer>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [ForeignKey("Album")]
        public int? AlbumId { get; set; }

        public virtual Album Album { get; set; }

        [ForeignKey("Writer")]
        [Required]
        public int WriterId { get; set; }

        public virtual Writer Writer { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<SongPerformer> SongPerformers { get; set; }
    }


}
