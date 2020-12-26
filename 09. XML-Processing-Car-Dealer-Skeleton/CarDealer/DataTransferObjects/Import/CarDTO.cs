using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Import
{
    [XmlType("Car")]
    public class CarDTO
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public PartForCarDTO[] Parts { get; set; }

    }

    [XmlType("partId")]
    public class PartForCarDTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }

    //<Car>
    //  <make>Opel</make>
    //  <model>Kadet</model>
    //  <TraveledDistance>31737446</TraveledDistance>
    //  <parts>
    //    <partId id = "65" />
    //    < partId id="95"/>
    //    <partId id = "90" />
    //  </ parts >
    //</ Car >
}
