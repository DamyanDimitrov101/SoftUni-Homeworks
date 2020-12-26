namespace Cinema.DataProcessor
{
    using System;
    using System.Linq;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            //            {
            //                "MovieName": "SIS",
            //    "Rating": "10.00",
            //    "TotalIncomes": "184.04",
            //    "Customers": [
            //      {
            //        "FirstName": "Davita",
            //        "LastName": "Lister",
            //        "Balance": "279.76"
            //      },
            //


            var movies = context.Movies
                .Where(m => m.Projections.Any(p => p.Tickets.Count >= 1))
                .Where(m => m.Rating >= rating)
                .OrderByDescending(m => m.Rating)
                .ThenByDescending(m => m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("F2"),
                    TotalIncomes = m.Projections.Sum(t => t.Tickets.Sum(p => p.Price)).ToString("F2"),
                    Customers = m.Projections.SelectMany(p => p.Tickets)
                    .Select(t => new
                    {
                        FirstName = t.Customer.FirstName,
                        LastName = t.Customer.LastName,
                        Balance = t.Customer.Balance.ToString("F2")
                    })
                    .OrderByDescending(c => c.Balance)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                    .ToArray()
                })
                .Take(10)
                .ToArray();





            return JsonConvert.SerializeObject(movies, Formatting.Indented);
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            // <Customer FirstName="Marjy" LastName="Starbeck">
            //    < SpentMoney > 82.65 </ SpentMoney >
            //    < SpentTime > 17:04:00 </ SpentTime >   
            //     </ Customer >


            var customers = context.Customers
             .Where(c => c.Age >= age)  
             .OrderByDescending(c=> c.Tickets.Sum(t => t.Price))
             .Select(c => new Customer_Export_DTO
             {
                 FirstName = c.FirstName,
                 LastName = c.LastName,
                 SpentMoney = c.Tickets.Sum(t => t.Price).ToString("F2"),
                 SpentTime = new TimeSpan(c.Tickets.Sum(t => t.Projection.Movie.Duration.Ticks)).ToString(@"hh\:mm\:ss")
             })
                .Take(10)
                .ToArray();


            return XMLConverter.Serialize(customers, "Customers");
        }
    }
}