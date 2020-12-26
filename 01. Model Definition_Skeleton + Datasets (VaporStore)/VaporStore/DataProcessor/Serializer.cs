namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			var genres = context.Genres
				.AsEnumerable()
				.Where(g=>  genreNames.Any(gn => gn == g.Name))
				.Where(g => g.Games.Any(g=>g.Purchases.Any()))
				.Select(g => new GenreExportDTO
				{
					Id = g.Id,
				    Genre = g.Name,
					Games = g.Games
					.Where(g=>g.Purchases.Any())
					.Select(game => new GamesExportDTO
					{
						Id = game.Id,
						 Developer = game.Developer.Name,
						  Title = game.Name,
						   Tags = string.Join(", ", game.GameTags.Select(t => t.Tag.Name).ToArray()),
						    Players = game.Purchases.Count
					})
					.OrderByDescending(g=>g.Players)
					.ThenBy(g=>g.Id)
					.ToArray(),
					TotalPlayers = g.Games.Sum(g => g.Purchases.Count)
				})
				.OrderByDescending(gn=>gn.TotalPlayers)
				.ThenBy(gn=>gn.Id)
				.ToList();

			return JsonConvert.SerializeObject(genres);
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{

			var users = context.Users
				.Where(u => u.Cards.Any(c => c.Purchases.Any()))
				.ToArray()
				.Select(u=> new ExportUserDTO 
				{
					  Username = u.Username,
					  Purchases = context.Purchases
							  .ToArray()
							  .Where(p=>p.Card.User.Username==u.Username
							  &&     p.Type.ToString() == storeType)
							  .OrderBy(p=>p.Date)
							  .Select(p=> new ExportPurchasesDTO
							  {
								  Card = p.Card.Number,
								  Cvc = p.Card.Cvc,
								  Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
								  Game = new ExportGameDTO
								  {
									   Genre = p.Game.Genre.Name,
									   Price = p.Game.Price,
									   title = p.Game.Name
								  }
							  })
							  .ToArray(),
					   TotalSpent = u.Cards.Sum(c=>c.Purchases.Where(p=>p.Type.ToString() == storeType).Sum(p=>p.Game.Price))
				})
				.Where(u=>u.Purchases.Any())
				.OrderByDescending(u=>u.TotalSpent)
				.ThenBy(u=>u.Username)
				.ToArray();


			return XMLConverter.Serialize(users, "Users");
		}
	}
}