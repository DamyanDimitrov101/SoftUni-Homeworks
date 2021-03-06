﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    public class Projection
    {
        //Projection
        //•	Id – integer, Primary Key
        //•	MovieId – integer, foreign key(required)
        //•	Movie – the projection’s movie
        //•	HallId – integer, foreign key(required)
        //•	Hall – the projection’s hall 
        //•	DateTime - DateTime(required)
        //•	Tickets - collection of type Ticket

        public Projection()
        {
            this.Tickets = new HashSet<Ticket>();
        }

            [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Required]
        [ForeignKey("Hall")]
        public int HallId { get; set; }

        public Hall Hall { get; set; }

        [Required]
        public DateTime DateTime { get; set; }


        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}