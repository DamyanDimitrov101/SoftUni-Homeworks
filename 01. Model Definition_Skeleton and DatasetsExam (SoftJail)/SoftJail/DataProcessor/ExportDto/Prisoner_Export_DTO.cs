using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class Prisoner_Export_DTO
    {

        public int Id { get; set; }


        public string Name { get; set; }


        public string IncarcerationDate { get; set; }


        public Message[] EncryptedMessages { get; set; }
    }

    public class Message
    {
        public string Description { get; set; }
    }


    //<Prisoner>
    //    <Id>3</Id>
    //    <Name>Binni Cornhill</Name>
    //    <IncarcerationDate>1967-04-29</IncarcerationDate>
    //    <EncryptedMessages>
    //      <Message>
    //        <Description>!?sdnasuoht evif-ytnewt rof deksa uoy ro orez artxe na ereht sI</Description>
    //      </Message>
    //    </EncryptedMessages>
    //  </Prisoner>

}
