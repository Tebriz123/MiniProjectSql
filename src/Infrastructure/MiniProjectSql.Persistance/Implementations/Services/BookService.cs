using Microsoft.EntityFrameworkCore;
using MiniProjectSql.Appilicatin.Interfaces.Repostories;
using MiniProjectSql.Appilicatin.Interfaces.Services;
using MiniProjectSql.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Persistance.Implementations.Services
{

    public class BookService
    {

        public AuthorService AuthorService { get; set; }
        public BookService()
        {
            AuthorService = new AuthorService();
        }

        public void CreateBook()
        {
            using (var context = new AppDbContex())
            {

                Console.WriteLine("Please enter book name: ");
                string name = Console.ReadLine().Trim();
                Console.Clear();
                if (name.Length <= 1)
                {
                    Console.WriteLine("please enter right name:");
                    return;
                }
                Book book2 = context.Books.FirstOrDefault(b => b.Name == name);
                if (book2 != null)
                {
                    Console.WriteLine("bu adda kitab var");
                    return;
                }

                Console.WriteLine("Please enter PageCount:");
                int pageCount;
                string ans = Console.ReadLine();
                int.TryParse(ans, out pageCount);
                Console.Clear();
                if (pageCount <= 0)
                {
                    Console.WriteLine("PageCount can  be positive");
                    return;
                }
                Console.WriteLine("Please enter Author: ");
                AuthorService.GetAll();
                int authorId;
                string result = Console.ReadLine();
                int.TryParse(result, out authorId);
                Console.Clear();
                book2 = context.Books.FirstOrDefault(b=>b.AuthorId == authorId);
                if (book2 != null)
                {
                    Console.WriteLine("Duzgun Author daxil edin!");
                    return; 
                }
                Book newBook = new Book
                {
                    Name = name,
                    PageCount = pageCount,
                    AuthorId = authorId
                };
 
                context.Books.Add(newBook);
                context.SaveChanges();

                Console.WriteLine("Book created successfully!");
            }
        }


        public void DeleteBook()
        {
            Console.WriteLine("Please enter Book Id: ");
            int bookId;
            string result = Console.ReadLine();
            int.TryParse(result, out bookId);
            Console.Clear();
            using (var context = new AppDbContex())
            { 
                Book book = context.Books.FirstOrDefault(b => b.Id == bookId);
                if (book == null)
                {
                    Console.WriteLine("Bu Id li kitab yoxdur!");


                }
                else if (book != null && book.IsReserved == false)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("This book is reserve");
                }



            }
        }

        public List<Book> GetBookById()
        {
            Console.WriteLine("Please enter Book Id: ");
            int bookId;
            string result = Console.ReadLine();
            int.TryParse(result, out bookId);
            Console.Clear();

            using (var context = new AppDbContex())
            {
                var books = context.Books
                    .Where(b => b.Id == bookId)
                    .ToList();


                if (books.Count == 0)
                {
                    Console.WriteLine("Book not found!");
                }
                else
                {
                    foreach (var book in books)
                    {
                        Console.WriteLine($"Id: {book.Id}");
                        Console.WriteLine($"Title: {book.Name}");
                        //Console.WriteLine($"Author: {book.Author.Name} ");
                        Console.WriteLine($"Page Count: {book.PageCount}");
                        Console.WriteLine("---------------------------");
                    }
                }

                return books;
            }
        }

        public List<Book> GetAll()
        {
            using (var context = new AppDbContex())
            {
                var books = context.Books
                    .Include(b => b.Author) 
                    .ToList();

                if (books.Count == 0)
                {
                    Console.WriteLine("No books found!");
                }
                else
                {
                    foreach (var book in books)
                    {
                        Console.WriteLine($"Id: {book.Id}");
                        Console.WriteLine($"Title: {book.Name}");
                        Console.WriteLine($"Author: {book.Author.Name} {book.Author.Surname}");
                        Console.WriteLine($"Page Count: {book.PageCount}");
                       
                    }
                }

                return books;
            }


        }

    }
}
