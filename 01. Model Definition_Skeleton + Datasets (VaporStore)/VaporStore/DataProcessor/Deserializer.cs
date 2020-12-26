namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		private static readonly string ErrorMessage = "Invalid Data";

		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{

			StringBuilder sb = new StringBuilder();


			var gamesDtos = JsonConvert.DeserializeObject<ImportGamesDTO[]>(jsonString);

			List<Game> games = new List<Game>();

			List<Genre> genres = new List<Genre>();

			List<Developer> developers = new List<Developer>();

			List<Tag> tags = new List<Tag>();


			foreach (var gameDTO in gamesDtos)
			{
				if (!IsValid(gameDTO))
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				if (gameDTO.Price<0)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				DateTime releaseDateValid;
				bool isReleaseDateValid = DateTime.TryParseExact(gameDTO.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDateValid);

				if (!isReleaseDateValid)
				{
					sb.AppendLine(ErrorMessage);

					continue;
				}

				if (string.IsNullOrEmpty(gameDTO.Developer))
				{
					sb.AppendLine(ErrorMessage);

					continue;
				}

				Developer developerValid = developers.FirstOrDefault(d => d.Name == gameDTO.Developer);

				if (developerValid == null)
				{
					developerValid = new Developer()
					{
						Name = gameDTO.Developer
					};

					developers.Add(developerValid);
				}


				Genre genreValid = genres.FirstOrDefault(g => g.Name == gameDTO.Genre);

				if (genreValid == null)
				{
					genreValid = new Genre()
					{
						Name = gameDTO.Genre
					};
					genres.Add(genreValid);
				}

				var game = new Game
				{
					Name = gameDTO.Name,
					Price = gameDTO.Price,
				    ReleaseDate = releaseDateValid,
					Developer = developerValid,
				    Genre = genreValid
				};

				foreach (var tag in gameDTO.Tags)
				{
					
						Tag tag1 = tags.FirstOrDefault(t=>t.Name==tag);
						
						if (tag1 == null)
						{

							tag1 = new Tag()
							{
								Name = tag
							};

							tags.Add(tag1);
						}


						GameTag gameTagValid = new GameTag()
						{
							Tag = tag1,
							Game = game
						};

						game.GameTags.Add(gameTagValid);
					
				}

				if (game.GameTags.Count == 0)
				{
					sb.AppendLine(ErrorMessage);

					continue;
				}


				games.Add(game);

				sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
			}

			context.Games.AddRange(games);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{

			StringBuilder sb = new StringBuilder();

			var userDTOs = JsonConvert.DeserializeObject<UsersDTO[]>(jsonString);

			List<User> users = new List<User>();

			foreach (var user in userDTOs)
			{
				if (!IsValid(user))
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var userValid = new User()
				{
					FullName = user.FullName,
					Username = user.Username,
				    Age = user.Age,
				    Email = user.Email,
				};

				bool invalidCard = false;
				foreach (var card in user.Cards)
				{
					if (!IsValid(card))
					{
						sb.AppendLine(ErrorMessage);
						invalidCard = true;
						break;
					}

					CardType typeCard;
					if (card.Type== "Credit")
					{
						typeCard = CardType.Credit;
					}
					else
					{
						typeCard = CardType.Debit;
					}

					Card cardValid = new Card()
					{
					  Number = card.Number,
					   Cvc = card.CVC,
					    Type = typeCard,
					     User = userValid
					};

					userValid.Cards.Add(cardValid);
				}

				if (invalidCard)
				{
					continue;
				}

				users.Add(userValid);
				sb.AppendLine($"Imported {userValid.Username} with {userValid.Cards.Count} cards");
			}

			context.Users.AddRange(users);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{

			StringBuilder sb = new StringBuilder();

			var purchaseDTOs = XMLConverter.Deserializer<PurchaseDTOImport>(xmlString, "Purchases");

			var purchases = new List<Purchase>();

			foreach (var pur in purchaseDTOs)
			{
				if (!IsValid(pur))
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				object? result;
				bool isTypeValid = Enum.TryParse(typeof(PurchaseType), pur.Type,out result);

				if (!isTypeValid)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}
				PurchaseType purchaseType = (PurchaseType)result;


				DateTime date;
				bool isDateValid = DateTime.TryParseExact(pur.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);


				if (!isDateValid)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Game game = context.Games.FirstOrDefault(g => g.Name == pur.Title);

				if (game == null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Card cardValid = context.Cards.FirstOrDefault(c => c.Number == pur.Card);

				if (cardValid == null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}


				var purchaseValid = new Purchase()
				{
					Type = purchaseType,
					Card = cardValid,
					Date = date,
					Game = game,
					ProductKey = pur.Key
				};

				purchases.Add(purchaseValid);



				sb.AppendLine($"Imported {pur.Title} for {purchaseValid.Card.User.Username}");
			}

			context.Purchases.AddRange(purchases);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}



		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}