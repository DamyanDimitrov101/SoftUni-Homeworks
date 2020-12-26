using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
    public class ExportUserDTO
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")]
        public ExportPurchasesDTO[] Purchases { get; set; }

        [XmlElement("TotalSpent")]
        public decimal TotalSpent { get; set; }
    }

    [XmlType("Purchase")]
    public class ExportPurchasesDTO
    {
        [XmlElement("Card")]
        public string Card { get; set; }
        
        [XmlElement("Cvc")]
        public string Cvc { get; set; }


        [XmlElement("Date")]
        public string Date { get; set; }

        [XmlElement("Game")]
        public ExportGameDTO Game { get; set; }

    }

    [XmlType("Game")]
    public class ExportGameDTO
    {
        [XmlAttribute("title")]
        public string title { get; set; }

        [XmlElement("Genre")]
        public string Genre { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
