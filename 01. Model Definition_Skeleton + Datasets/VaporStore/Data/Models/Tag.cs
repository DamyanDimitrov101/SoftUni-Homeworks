using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.Data.Models
{
    public class Tag
    {
        //Tag
        //•	Id – integer, Primary Key
        //•	Name – Text(required)
        //•	GameTags - collection of type GameTag

        public Tag()
        {
            this.GameTags = new HashSet<GameTag>();
        }

            [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<GameTag> GameTags { get; set; }
    }
}
