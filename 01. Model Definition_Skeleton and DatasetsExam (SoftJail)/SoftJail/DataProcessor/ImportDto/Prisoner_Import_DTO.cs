using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    [JsonObject("Prisoner")]
    public class Prisoner_Import_DTO
    {
        //          {
        //    "FullName": "Melanie Simonich",
        //    "Nickname": "The Wallaby",
        //    "Age": 32,
        //    "IncarcerationDate": "29/03/1957",
        //    "ReleaseDate": "27/03/2006",
        //    "Bail": null,
        //    "CellId": 5,
        //    "Mails": [
        //      {
        //        "Description": "please add me to your LinkedIn network",
        //        "Sender": "Zonda Vasiljevic",
        //        "Address": "51677 Rieder Center str."
        //      },
        //      {
        //        "Description": "Melanie i hope you found the best place for you!",
        //        "Sender": "Shell Lofthouse",
        //        "Address": "5877 Shoshone Way str."
        //      },
        //      {
        //        "Description": "Turns out they wanted to implement things like fully responsive //dynamic        content, useful apps, etc – all things I told them they needed in /the first/ place   but which  they opted not to include.",
        //        "Sender": "My Ansell",
        //        "Address": "71908 Waubesa Plaza str."
        //      }


        //•	FullName – text with min length 3 and max length 20 (required)
        //•	Nickname – text starting with "The " and a single word only of letters with /an/      uppercase letter for beginning(example: The Prisoner) (required)
        //•	Age – integer in the range[18, 65] (required)
        //•	IncarcerationDate ¬– Date(required)
        //•	ReleaseDate– Date
        //•	Bail– decimal (non-negative, minimum value: 0)
        //•	CellId - integer, foreign key
        //•	Cell – the prisoner's cell
        //•	Mails - collection of type Mail
        //•	PrisonerOfficers - collection of type OfficerPrisoner

            [Required]
            [MaxLength(20)]
            [MinLength(3)]
        public string FullName { get; set; }
        
        [Required]
        [RegularExpression(@"^(The) ([A-Z][a-z]+)$")]
        public string Nickname { get; set; }

        [Required]
        [Range(18,65)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public Mail_Import_DTO[] Mails { get; set; }
    }

    [JsonObject("Mails")]

    public class Mail_Import_DTO
    {
        //•	Description– text(required)
        //•	Sender – text(required)
        //•	Address – text, consisting only of letters, spaces and numbers, which ends //with “   str.  ” (required) (Example: “62 Muir Hill str.“)
        //•	PrisonerId - integer, foreign key(required)
        //•	Prisoner – the mail's Prisoner (required)


            [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^(([\w]+) )+(str.)$")]
        public string Address { get; set; }
    }
}
