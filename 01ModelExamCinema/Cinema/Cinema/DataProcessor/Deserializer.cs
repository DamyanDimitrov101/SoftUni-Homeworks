namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var movieDTOs = JsonConvert.DeserializeObject<Movies_Import_DTO[]>(jsonString);

            List<Movie> movies = new List<Movie>();

            foreach (var mov in movieDTOs)
            {
              
                object genreValid;
                bool isValidGenreEnum = Enum.TryParse(typeof(Genre), mov.Genre,out genreValid);

                TimeSpan durationValid;
                bool isValidDurationTimeSpan = TimeSpan.TryParseExact(mov.Duration,"c", CultureInfo.InvariantCulture, out durationValid);

                bool movieExist = movies.Any(m=> m.Title==mov.Title);


                if (!isValidDurationTimeSpan || !IsValid(mov) || movieExist || !isValidGenreEnum)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = new Movie()
                {
                    Title = mov.Title,
                    Genre = (Genre)genreValid,
                    Duration = durationValid,
                    Rating = mov.Rating,
                    Director = mov.Director
                };

                movies.Add(movie);

                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("f2")));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            List<Hall> halls = new List<Hall>();

            var hallDTOs = JsonConvert.DeserializeObject<Hall_Import_DTO[]>(jsonString);


            foreach (var h in hallDTOs)
            {
                if (!IsValid(h))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Hall hallValid = new Hall()
                {
                    Name = h.Name,
                    Is3D = h.Is3D,
                    Is4Dx = h.Is4Dx,
                };


                if (h.Seats<=0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                List<Seat> seats = new List<Seat>();

                for (int i = 0; i < h.Seats; i++)
                {
                    Seat seatValid = new Seat()
                    {
                        Hall = hallValid 
                    };

                    seats.Add(seatValid);
                }

                hallValid.Seats = seats;


                StringBuilder typeView = new StringBuilder();

                if (hallValid.Is4Dx)
                {
                    typeView.Append("4Dx");

                    if (hallValid.Is3D)
                    {
                        typeView.Append("/");

                    }
                }

                if (hallValid.Is3D)
                {
                    typeView.Append("3D");
                }
                
               

                if (!hallValid.Is3D && !hallValid.Is4Dx)
                {
                    typeView.Append("Normal");
                }

                halls.Add(hallValid);
                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hallValid.Name, typeView.ToString().Trim(),hallValid.Seats.Count));
            }


            context.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            List<Projection> projections = new List<Projection>();

            var projectionDTOs = XMLConverter.Deserializer<Projection_Import_DTO>(xmlString, "Projections");

            foreach (var p in projectionDTOs)
            {
                if (!IsValid(p))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime dateTime;
                bool isValidDateTime = DateTime.TryParseExact(p.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);


                var hall = context.Halls.FirstOrDefault(h => h.Id == p.HallId);
                var movie = context.Movies.FirstOrDefault(m => m.Id == p.MovieId);

                if (!isValidDateTime || hall == null || movie == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Projection projectionValid = new Projection()
                {
                     DateTime = dateTime,
                     Hall = hall,
                     Movie = movie
                };

                projections.Add(projectionValid);
                sb.AppendLine(string.Format(SuccessfulImportProjection, projectionValid.Movie.Title, projectionValid.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();


            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            List<Customer> customers = new List<Customer>();

            var customersDTOs = XMLConverter.Deserializer<Customer_Import_Dto>(xmlString, "Customers");

            foreach (var c in customersDTOs)
            {
                if (!IsValid(c))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (c.Balance<=0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customerValid = new Customer()
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Age = c.Age,
                    Balance = c.Balance,
                };

                bool isInvalid = false;
                foreach (var t in c.Tickets)
                {
                    if (!IsValid(t))
                    {
                        isInvalid = true;
                        break;
                    }

                    var projection = context.Projections.FirstOrDefault(p => p.Id == t.ProjectionId);

                    if (projection == null)
                    {
                        isInvalid = true;
                        break;
                    }

                    Ticket ticket = new Ticket()
                    {
                        Customer = customerValid,
                         Projection = projection,
                         Price = t.Price
                    };

                    customerValid.Tickets.Add(ticket);
                }

                if (isInvalid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                customers.Add(customerValid);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket,customerValid.FirstName, customerValid.LastName, customerValid.Tickets.Count));
            }

            context.Customers.AddRange(customers);
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