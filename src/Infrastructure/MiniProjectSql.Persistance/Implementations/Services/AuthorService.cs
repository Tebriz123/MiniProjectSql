using Microsoft.EntityFrameworkCore;
using MiniProjectSql.Appilicatin.Interfaces.Services;
using MiniProjectSql.Domain.Entities;
using MiniProjectSql.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Persistance.Implementations.Services
{
    public class AuthorService
    {
        public void Create()
        {
            using (var context = new AppDbContex())
            {
                Console.WriteLine("Please enter author name: ");
                string name = Console.ReadLine().Trim();
                Console.Clear();
                if (name.Length <= 1 || !name.All(char.IsLetter))
                {
                    Console.WriteLine("Please enter right author name!");
                    return;
                }

                Console.WriteLine("Please enter author surname: ");
                string surname = Console.ReadLine().Trim();
                Console.Clear();

                if (surname.Length <= 1)
                {
                    Console.WriteLine("Please enter right author surname!");
                    return;
                }

                Console.WriteLine("Please select gender:");

                foreach (var value in Enum.GetValues(typeof(Gender)))
                {
                    Console.WriteLine($"{(int)value} - {value}");
                }

                Console.Write("Enter number: ");
                string input = Console.ReadLine();
                int genderNumber;
                Console.Clear();
                if (!int.TryParse(input, out genderNumber) || !Enum.IsDefined(typeof(Gender), genderNumber))
                {
                    Console.WriteLine("Invalid gender selection!");
                    return;
                }

                Gender gender = (Gender)genderNumber;

                Author newAuthor = new Author
                {
                    Name = name,
                    Surname = surname,
                    Gender = gender
                };

                context.Authors.Add(newAuthor);
                context.SaveChanges();
                Console.WriteLine("Author created successfully!");

            }
        }

        public List<Author> GetAll()
        {
            using (var context = new AppDbContex())
            {
                var authors = context.Authors
                    .Include(a => a.Books)
                    .ToList();

                if (authors.Count == 0)
                {
                    Console.WriteLine("No authors found!");
                }
                else
                {
                    foreach (var author in authors)
                    {
                        Console.WriteLine($"Author Id: {author.Id}");
                        Console.WriteLine($"Name: {author.Name} {author.Surname}");
                        Console.WriteLine($"Gender: {author.Gender}");
                        Console.WriteLine("Books:");

                        if (author.Books != null && author.Books.Count > 0)
                        {
                            foreach (var book in author.Books)
                            {
                                Console.WriteLine($"  - {book.Name} (Pages: {book.PageCount})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("  No books found for this author.");
                        }

                        Console.WriteLine("---------------------------");
                    }
                }

                return authors;
            }



        }

        public List<Book> ShowAuthorsBooks()
        {

            Console.WriteLine("Please enter Author Id: ");
            string result = Console.ReadLine()?.Trim();
            Console.Clear();

            if (!int.TryParse(result, out int authorId))
            {
                Console.WriteLine("Invalid Author Id!");
                return new List<Book>();
            }

            using (var context = new AppDbContex())
            {
                var books = context.Books
                    .Include(b => b.Author) 
                    .Where(b => b.AuthorId == authorId)
                    .ToList();

                if (books.Count == 0)
                {
                    Console.WriteLine("No books found for this Author Id!");
                }
                else
                {
                    Console.WriteLine($"Books for Author Id: {authorId}");
                    Console.WriteLine("---------------------------");

                    foreach (var book in books)
                    {
                        Console.WriteLine($"Book Id: {book.Id}");
                        Console.WriteLine($"Title: {book.Name}");
                        Console.WriteLine($"Author: {book.Author?.Name} {book.Author?.Surname}");
                        Console.WriteLine($"Page Count: {book.PageCount}");
                        Console.WriteLine("---------------------------");
                    }
                }

                return books;
            }



        }








    }
}
