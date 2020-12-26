using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class SongDTO
    {
        //        <Song>
        //    <Name>Morning After</Name>
        //    <Duration>00:04:23</Duration>
        //    <CreatedOn>21/12/2007</CreatedOn>
        //    <Genre>Rap</Genre>
        //    <AlbumId>4</AlbumId>
        //    <WriterId>3</WriterId>
        //    <Price>10</Price>
        //  </Song>
        //


        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        [Required]

        public string Duration { get; set; }

        [Required]
        public string CreatedOn { get; set; }


        [Required]
        public string Genre { get; set; }


        [ForeignKey("Album")]
        public int? AlbumId { get; set; }




        [ForeignKey("Writer")]
        [Required]
        public int WriterId { get; set; }



        

        [Required]
        public decimal Price { get; set; }



    }
}
