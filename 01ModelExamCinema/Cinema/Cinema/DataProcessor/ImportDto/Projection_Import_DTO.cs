using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
    public class Projection_Import_DTO
    {
        //    <Projection>
        //<MovieId>38</MovieId>
        //<HallId>4</HallId>
        //<DateTime>2019-04-27 13:33:20</DateTime>
        //</Projection>


        //•	MovieId – integer, foreign key(required)
        //•	HallId – integer, foreign key(required)
        //•	Hall – the projection’s hall 
        //•	DateTime - DateTime(required)
        

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }


        [Required]
        [ForeignKey("Hall")]
        public int HallId { get; set; }


        [Required]
        public string DateTime { get; set; }

    }
}
