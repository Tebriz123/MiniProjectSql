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
                if (pageCount < 0)
                {
                    Console.WriteLine("PageCount can not be negative");
                    return;
                }
                Console.WriteLine("Please enter Author: ");
                int authorId;
                string result = Console.ReadLine();
                int.TryParse(result, out authorId);
                book2 = context.Books.FirstOrDefault(b=>b.AuthorId == authorId);
                if (book2 == null)
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

        }

        public void GetBookById()
        {



        }

        //public List<Book> GetAll()
        //{
        //    using (var context = new AppDbContex())
        //    {
        //        //return context.Books.FirstOrDefault.ToList();
        //    }
        //}

    }
}
