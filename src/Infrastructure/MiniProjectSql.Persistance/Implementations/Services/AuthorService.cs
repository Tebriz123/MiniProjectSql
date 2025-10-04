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

                if (name.Length <= 1)
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






        public void Delete()
        {

        }

        //public List<Author> GetAll()
        //{

        //}

        //    public List<Author> GetById()
        //    {

        //    }
    }
}
