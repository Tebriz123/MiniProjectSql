using MiniProjectSql.Persistance.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.ConsoleApp.Helper
{
    internal class ManagmentAppilication
    {
        public BookService BookService { get; set; }

        public AuthorService AuthorService { get; set; }
        public ReserveService ReserveService { get; set; }

        public ManagmentAppilication()
        {
            BookService = new BookService();
            AuthorService = new AuthorService();
            ReserveService = new ReserveService();
        }

        public void Run()
        {
            int num = 0;
            string str = null;
            bool result = false;

            Console.Clear();

            while (!(num == 0 && result))
            {
             

                Console.WriteLine("1.Create Book\n2.Delete Book\n3.Get Book by Id\n4.Show All Books\n5.Create Author\n6.Show All Authors\n7.Show Author's Books\n8.Reserve Book\n9.Reservation List\n10.Change Reservation Status\n11.User's Reservations List\n\n0.Exit");
                str = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(str, out num);
                switch (num)
                {
                    case 1:
                        BookService.CreateBook();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:
                        break;
                    case 0:
                        if (result)
                        {
                            Console.WriteLine("Program finished");
                        }
                        else
                        {
                            Console.WriteLine("Wrong input");
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;

                }
            }
        }
    }
}
