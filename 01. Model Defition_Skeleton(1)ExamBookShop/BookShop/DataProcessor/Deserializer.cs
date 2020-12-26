namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var books = XMLConverter.Deserializer<ImportBookDTO>(xmlString, "Books");

            StringBuilder sb = new StringBuilder();

            List<Book> validBooks = new List<Book>();

            foreach (var book in books)
            {
                if (!IsValid(book))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime result;
                bool isDateTimeValid = DateTime.TryParseExact(book.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,out result);

                if (!isDateTimeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Book bookValid = new Book()
                {
                   Name = book.Name,
                   Genre = (Genre)book.Genre,
                   Price = book.Price,
                   Pages = book.Pages,
                   PublishedOn = result
                };

                validBooks.Add(bookValid);

                sb.AppendLine(String.Format(SuccessfullyImportedBook, bookValid.Name, bookValid.Price));
            }


            context.Books.AddRange(validBooks);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {

            StringBuilder sb = new StringBuilder();

            var authors = JsonConvert.DeserializeObject<ImportAuthorDTO[]>(jsonString);


            List<Author> authorsValid = new List<Author>();

            foreach (var author in authors)
            {
                if (!IsValid(author))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (authorsValid.Any(a => a.Email == author.Email))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var authorValid = new Author()
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Email = author.Email,
                    Phone = author.Phone
                };



                AddBooks(context, author, authorValid);



                if (authorValid.AuthorsBooks.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }



                authorsValid.Add(authorValid);
                sb.AppendLine(string.Format(SuccessfullyImportedAuthor, $"{author.FirstName} {author.LastName}", authorValid.AuthorsBooks.Count));
            }

            context.Authors.AddRange(authorsValid);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static void AddBooks(BookShopContext context, ImportAuthorDTO author, Author authorValid)
        {
            foreach (var book in author.Books)
            {

                if (!book.Id.HasValue)
                {
                    continue;
                }


                if (!context.Books.Any(b => b.Id == book.Id))
                {
                    continue;
                }


                var validBook = context.Books.FirstOrDefault(b => b.Id == book.Id);



                if (validBook == null)
                {
                    continue;
                }

                authorValid.AuthorsBooks.Add(new AuthorBook()
                {
                    Author = authorValid,
                    Book = validBook
                });

                //sb.AppendLine(string.Format(SuccessfullyImportedBook, (book.Id), $"{validBook.Price:F2}"));

            }
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}