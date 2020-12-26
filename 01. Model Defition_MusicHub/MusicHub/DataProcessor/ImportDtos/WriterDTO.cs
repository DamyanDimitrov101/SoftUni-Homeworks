using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.DataProcessor.ImportDtos
{
    [JsonObject("Writer")]
    public class WriterDTO
    {
        //•	Name– text with min length 3 and max length 20 (required)
        //•	Pseudonym – text, consisting of two words separated with space and each /word /must      start with one upper letter and continue with many lower-case /letters/(Example:  "Freddie    Mercury")

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [RegularExpression("^([A-Z][a-z]+) +([A-Z][a-z]+)$")]
        [JsonProperty("Pseudonym")]
        public string Pseudonym { get; set; }

    }

}
