using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Performer")]
    public class SongPerformerDTO
    {
        //          <Performer>
        //    <FirstName>Peter</FirstName>
        //    <LastName>Bree</LastName>
        //    <Age>25</Age>
        //    <NetWorth>3243</NetWorth>
        //    <PerformersSongs>
        //      <Song id = "2" />
        //      < Song id="1" />
        //    </PerformersSongs>
        //  </Performer>
        //

        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [Range(18,70)]
        public int Age { get; set; }

        [Required]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public List<Song_PerformerDTO> Song_PerformerDTOs { get; set; }
        

    }

    [XmlType(nameof(Song))]
    public class Song_PerformerDTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
