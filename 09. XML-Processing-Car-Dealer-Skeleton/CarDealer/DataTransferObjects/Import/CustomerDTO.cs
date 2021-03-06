﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Import
{
    [XmlType("Customer")]
    public class CustomerDTO
    {
        [XmlElement("name")]
        public string Name { get; set; }
        
        [XmlElement("birthDate")]
        public DateTime BirthDate { get; set; }
        
        [XmlElement("isYoungDriver")]
        public bool IsYoungDriver { get; set; }

    }
    //<Customer>
    //    <name>Natalie Poli</name>
    //    <birthDate>1990-10-04T00:00:00</birthDate>
    //    <isYoungDriver>false</isYoungDriver>
    //</Customer>
}
