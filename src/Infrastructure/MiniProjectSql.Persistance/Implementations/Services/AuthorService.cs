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
    public class AuthorService:IAuthorService
    {
        public void Create()
        {
            using (var context = new AppDbContex())
            {

                Console.WriteLine("Please enter book name: ");
                string name = Console.ReadLine().Trim();
                Console.Clear();
                if (name.Length <= 1)
                {
                    Console.WriteLine("please enter right author:");
                    return;
                }
                string surname = Console.ReadLine().Trim();
                Console.Clear();
                if (surname.Length <= 1)
                {
                    Console.WriteLine("please enter right author:");
                    return;
                }
                //Gender gender = Console.ReadLine().Trim();
                Console.Clear();

                Author newAuthor = new Author
                {
                    Name = name,
                    Surname = surname,
                    //Gender = gender
                };

                context.Authors.Add(newAuthor);
                context.SaveChanges();

                Console.WriteLine("Book created successfully!");
            }

       







        }

        public void Delete()
        {
            
        }

        public List<Author> GetAll()
        {
           
        }

        public List<Author> GetById()
        {
            
        }
    }
}
