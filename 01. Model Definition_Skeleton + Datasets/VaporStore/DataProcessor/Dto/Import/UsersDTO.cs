using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class UsersDTO
	{
		//{
		//	"FullName": "Nell Paradyce",
		//	"Username": "nparadyce",
		//	"Email": "nparadyce@softuni.bg",
		//	"Age": 48,
		//	"Cards": [
		//		{
		//			"Number": "0798 3871 2521 2016",
		//			"CVC": "036",
		//			"Type": "Debit"
		//		},
		//		{
		//			"Number": "1661 2121 6244 8487",
		//			"CVC": "289",
		//			"Type": "Debit"
		//		}
		//	]
		//},


		[Required]
		[RegularExpression("^(([A-Z][a-z]+) ([A-Z][a-z]+))$")]
		public string FullName { get; set; }

		[MaxLength(20)]
		[MinLength(3)]
		[Required]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Range(3,103)]
		[Required]
		public int Age { get; set; }
		
		public CardDTO[] Cards { get; set; }
	}

	[JsonObject]
	public class CardDTO
	{
		[Required]
		[RegularExpression(@"^(([\d]+) ([\d]+) ([\d]+) ([\d]+))$")]
		public string Number { get; set; }

		[Required]
		[RegularExpression(@"^([\d]{3})$")]
		public string CVC { get; set; }

		[Required]
		public string Type { get; set; }
	}

}
