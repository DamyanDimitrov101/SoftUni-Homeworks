using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ImportDto
{
    public class Hall_Import_DTO
    {
        //  {
        //    "Name": "Methocarbamol",
        //    "Is4Dx": false,
        //    "Is3D": true,
        //    "Seats": 52
        //  },

        //Hall
        //•	Name – text with length[3, 20] (required)
        //•	Is4Dx - bool
        //•	Is3D - bool
        //•	Projections - collection of type Projection
        //•	Seats - collection of type Seat


        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }


        public int Seats { get; set; }

    }
}
