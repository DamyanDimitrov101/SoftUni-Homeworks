namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            //Select all authors along with their books. Select their name in format first name + ' ' + last name. For each book select its name and price formatted to the second digit after the decimal point. Order the books by price in descending order. Finally sort all authors by book count descending and then by author full name.
            //            NOTE: Before the orders, materialize the query(This is issue by Microsoft      in //   InMemory database library)!!!


            var authors = context.Authors
                .AsEnumerable()
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    Books = a.AuthorsBooks
                    .OrderByDescending(b => b.Book.Price)
                    .Select(b => new
                    {
                        BookName = b.Book.Name,
                        BookPrice = b.Book.Price.ToString("f2")
                    })
                .ToArray()
                })
                .OrderByDescending(a => a.Books.Length)
                .ThenBy(a => a.AuthorName)
               .ToArray();


            return JsonConvert.SerializeObject(authors,Formatting.Indented);
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context.Books
                .AsEnumerable()
                .Where(b=>DateTime.Compare(b.PublishedOn,date)<0 &&
                b.Genre == Genre.Science)
                .OrderByDescending(b => b.Pages)
                .ThenByDescending(b => b.PublishedOn)
                .Select(b => new BooksExportDTO
                {
                   Name = b.Name,
                   Pages = b.Pages,
                   Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .Take(10)
                .ToList();

            return XMLConverter.Serialize(books, "Books");
        }
    }
}