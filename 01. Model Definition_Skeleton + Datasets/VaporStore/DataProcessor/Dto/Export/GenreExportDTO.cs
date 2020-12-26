using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [JsonObject("Genre")]
    public class GenreExportDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Genre")]
        public string Genre { get; set; }

        [JsonProperty("Games")]
        public GamesExportDTO[] Games { get; set; }

        [JsonProperty("TotalPlayers")]
        public int TotalPlayers { get; set; }
    }

    public class GamesExportDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Developer")]
        public string Developer { get; set; }

        [JsonProperty("Tags")]
        public string Tags { get; set; }

        [JsonProperty("Players")]
        public int Players { get; set; }
    }
}
