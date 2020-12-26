using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    [JsonObject("Department")]
    public class Department_Import_DTO
    {
        //    "Name": "Cybersecurity",
        //    "Cells": [
        //      {
        //        "CellNumber": 101,
        //        "HasWindow": true
        //      },
        //      {
        //        "CellNumber": 102,
        //        "HasWindow": false
        //      },
        //      {
        //        "CellNumber": 103,
        //        "HasWindow": true
        //      },
        //      {
        //        "CellNumber": 104,
        //        "HasWindow": false
        //      },
        //      {
        //        "CellNumber": 105,
        //        "HasWindow": true
        //      }
        //    ]
        //  },
        //


        //•	Name – text with min length 3 and max length 25 (required)
        //•	Cells - collection of type Cell

            [Required]
            [MaxLength(25)]
            [MinLength(3)]
        public string Name { get; set; }


        public Cells_Import_DTO[] Cells { get; set; }
    }

    [JsonObject("Cells")]
    public class Cells_Import_DTO
    {
        //•	CellNumber – integer in the range[1, 1000] (required)
        //•	HasWindow – bool (required)

            [Required]
            [Range(1,1000)]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }
    }
}
