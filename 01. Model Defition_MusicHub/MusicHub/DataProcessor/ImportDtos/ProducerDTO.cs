using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.DataProcessor.ImportDtos
{
    [JsonObject("Producer")]
    public class ProducerDTO
    {
        //        {
        //    "Name": "Invalid",
        //    "Pseudonym": "Rog Coiley",
        //    "PhoneNumber": "(105) 9339880",
        //    "Albums": [
        //      {
        //        "Name": "Sweetbitter",
        //        "ReleaseDate": "07/1/2018"
        //      },
        //      {
        //        "Name": "Emergency",
        //        "ReleaseDate": "16/09/2018"
        //      }
        //    ]
        //  },

        // •	Name– text with min length 3 and max length 30 (required)
        //•	Pseudonym – text, consisting of two words separated with space and each /word /must      start with one upper letter and continue with many lower-case /letters/(Example:  "Bon   Jovi")
        //•	PhoneNumber – text, consisting only of three groups(separated by space) of //three       digits and starting always with "+359" (Example: "+359 887 234 267")

        public ProducerDTO()
        {
            this.AlbumsDTOs = new List<AlbumDTO>();
        }

            [MinLength(3)]
            [MaxLength(30)]
            [Required]
        public string Name { get; set; }

        [RegularExpression(@"^([A-Z][a-z]+) +([A-Z][a-z]+)$")]
        public string Pseudonym { get; set; }

        [RegularExpression(@"^(\+359) +([\d]{3}) +([\d]{3}) +([\d]{3})$")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Albums")]
        public List<AlbumDTO> AlbumsDTOs { get; set; }
    }

    [JsonObject("Album")]
    public class AlbumDTO
    {
        //•	Name – text with min length 3 and max length 40 (required)
        //•	ReleaseDate – Date(required)

        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string  Name { get; set; }

        [Required]
        public string ReleaseDate { get; set; }
    }
}
