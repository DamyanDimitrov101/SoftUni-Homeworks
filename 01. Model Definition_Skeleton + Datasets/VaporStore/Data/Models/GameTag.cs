using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VaporStore.Data.Models
{
    public class GameTag
    {
        //GameTag
        //•	GameId – integer, Primary Key, foreign key(required)
        //•	Game – Game
        //•	TagId – integer, Primary Key, foreign key(required)
        //•	Tag – Tag

        [ForeignKey("Game")]
        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [ForeignKey("Tag")]
        [Required]
        public int TagId { get; set; }
        
        public Tag Tag { get; set; }
    }
}
