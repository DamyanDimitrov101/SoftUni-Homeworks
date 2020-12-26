namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            //int input = int.Parse(Console.ReadLine());

            int result = RemoveBooks(db);

            db.SaveChanges();
            Console.WriteLine(result);
            //Console.WriteLine(result.Length);
        }

        //Problem 02
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .AsEnumerable()
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(x=>new
                {
                    x.Title
                })
                .OrderBy(x=>x.Title)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }

        //Problem 03
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold)
                .Where(b => b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }

        //Problem 04
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString();
        }

        //Problem 05
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b=>b.BookId)
                .Select(b => b.Title)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }

        //Problem 06
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            string[] arrCategories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c=>c.ToLower())
                .ToArray();

            var booksTitles = new List<string>();

            foreach (var category in arrCategories)
            {
                var booksCurrent = context
                    .Books
                    .Where(b => b.BookCategories
                        .Any(bc => bc.Category.Name.ToLower() == category))
                    .Select(b=>b.Title)
                    .ToList();

                booksTitles.AddRange(booksCurrent);
            }

            foreach (var title in booksTitles.OrderBy(bc => bc))
            {
                sb.AppendLine(title);
            }

            return sb.ToString();
        }

        //Problem 07
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            DateTime dateInput = DateTime.ParseExact(date, "dd-MM-yyyy",null);

            var books = context.Books
                .Where(b => b.ReleaseDate.Value<dateInput)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .ToList();

            foreach (var book in books.OrderByDescending(b => b.ReleaseDate))
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 08
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var authorNames = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a=> new
                {
                    FullName = a.FirstName +" "+a.LastName
                })
                .ToList();

            foreach (var author in authorNames.OrderBy(a=>a.FullName))
            {
                sb.AppendLine(author.FullName);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 09
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            string nameTitle = input.ToLower();

            var booksStringTitle = context.Books
                .AsEnumerable()        
                .Where(b => b.Title.Contains(nameTitle,StringComparison.CurrentCultureIgnoreCase))
                        .Select(b=> b.Title)
                        .OrderBy(bt => bt)
                        .ToList();


            return String.Join(System.Environment.NewLine,booksStringTitle);
        }

        //Problem 10
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();


            var booksAndTitles = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b=>b.BookId)
                .Select(b => new 
                {
                    AuthorFullName=b.Author.FirstName + " "+ b.Author.LastName,
                    b.Title
                })
                .ToList();


            foreach (var book in booksAndTitles)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorFullName})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 11
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return books;
        }

        //Problem 12
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var copies = context.Authors
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    BookCopies = a.Books
                    .Select(b => b.Copies)
                    .Sum()
                })
                .OrderByDescending(c=>c.BookCopies)
                .ToList();

            foreach (var copy in copies)
            {
                sb.AppendLine($"{copy.AuthorName} - {copy.BookCopies}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 13
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context.Categories.Select(c => new
            {
                c.Name
                ,
                Profit = c.CategoryBooks.Select(b => b.Book.Price*b.Book.Copies).Sum()
            })
                .OrderByDescending(c=>c.Profit)
                .ToList();

            foreach (var cat in categories)
            {
                sb.AppendLine($"{cat.Name} ${cat.Profit:F2}");
            }

            return sb.ToString().Trim();
        }

        //Problem 14
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    BooksRelease = c.CategoryBooks.Select(cb => new
                    {
                        Title = cb.Book.Title
                        ,cb.Book.ReleaseDate
                    })
                    .OrderByDescending(c => c.ReleaseDate)
                    .Take(3)
                    .ToList()
                })
                .OrderBy(c=>c.Name)
                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");
                foreach (var book in category.BooksRelease)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }

        //Problem 15
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //Problem 16
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();

            return books.Count;
        }
    }
}
