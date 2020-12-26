using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class Customer_Import_Dto
    {
        //      <Customer>
        //  <FirstName>Randi</FirstName>
        //  <LastName>Ferraraccio</LastName>
        //  <Age>20</Age>
        //  <Balance>59.44</Balance>
        //  <Tickets>
        //    <Ticket>
        //      <ProjectionId>1</ProjectionId>
        //      <Price>7</Price>
        //    </Ticket>
        //  </Tickets>
        //</Customer>


        //•	FirstName – text with length[3, 20] (required)
        //•	LastName – text with length[3, 20] (required)
        //•	Age – integer in the range[12, 110] (required)
        //•	Balance - decimal (non-negative, minimum value: 0.01) (required)
        //•	Tickets - collection of type Ticket


        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [Range(12,110)]
        public int Age { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public List<Tickets_Import_DTO> Tickets { get; set; }
    }

    [XmlType("Ticket")]
    public class Tickets_Import_DTO
    {
        //•	Price – decimal (non-negative, minimum value: 0.01) (required)
        //•	CustomerId – integer, foreign key(required)
        //•	Customer – the customer’s ticket 
        //•	ProjectionId – integer, foreign key(required)
        //•	Projection – the projection’s ticket

        [Required]
        public int ProjectionId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
