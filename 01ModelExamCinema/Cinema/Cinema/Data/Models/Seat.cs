using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    public class Seat
    {
        //Seat
        //•	Id – integer, Primary Key
        //•	HallId – integer, foreign key(required)
        //•	Hall – the seat’s hall

            [Key]
        public int Id { get; set; }

        [ForeignKey("Hall")]
        public int HallId { get; set; }

        public Hall Hall { get; set; }

    }
}