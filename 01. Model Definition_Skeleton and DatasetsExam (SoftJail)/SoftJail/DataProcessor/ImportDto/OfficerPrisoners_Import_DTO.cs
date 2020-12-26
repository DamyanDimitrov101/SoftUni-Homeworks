using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficerPrisoners_Import_DTO
    {
        //        <Officer>
        //    <Name>Minerva Holl</Name>
        //    <Money>2582.55</Money>
        //    <Position>Overseer</Position>
        //    <Weapon>ChainRifle</Weapon>
        //    <DepartmentId>2</DepartmentId>
        //    <Prisoners>
        //      <Prisoner id = "15" />
        //    </ Prisoners >
        //  </ Officer >
        //

        //•	FullName – text with min length 3 and max length 30 (required)
        //•	Salary – decimal (non-negative, minimum value: 0) (required)
        //•	Position - Position enumeration with possible values: “Overseer, Guard, //Watcher,      Labour” (required)
        //•	Weapon - Weapon enumeration with possible values: “Knife, FlashPulse, //ChainRifle,     Pistol, Sniper” (required)
        //•	DepartmentId - integer, foreign key(required)
        //•	Department – the officer's department (required)
        //•	OfficerPrisoners - collection of type OfficerPrisoner

            [Required]
            [MaxLength(30)]
            [MinLength(3)]
            [XmlElement("Name")]
        public string FullName { get; set; }

        [Required]
        public decimal Money { get; set; }
        
        [Required]
        public string Position { get; set; }

        [Required]
        public string Weapon { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public Prisoners_Import_DTO[] Prisoners { get; set; }
    }

    [XmlType("Prisoner")]
    public class Prisoners_Import_DTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
