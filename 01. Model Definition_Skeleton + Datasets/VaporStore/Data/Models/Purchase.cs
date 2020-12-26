﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Purchase
    {
        //Purchase
        //•	Id – integer, Primary Key
        //•	Type – enumeration of type PurchaseType, with possible values(“Retail”, “Digital/”) /     (required) 
        //•	ProductKey – text, which consists of 3 pairs of 4 uppercase Latin letters and //digits,    separated by dashes(ex. “ABCD-EFGH-1J3L”) (required)
        //•	Date – Date(required)
        //•	CardId – integer, foreign key(required)
        //•	Card – the purchase’s card(required)
        //•	GameId – integer, foreign key(required)
        //•	Game – the purchase’s game(required)

        [Key]
        public int Id { get; set; }

        public PurchaseType Type { get; set; }

        [Required]
        public string ProductKey { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Card")]
        public int CardId { get; set; }

        public virtual Card Card { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}


