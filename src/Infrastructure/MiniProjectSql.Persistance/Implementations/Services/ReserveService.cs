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
    public class ReserveService
    {

        public void ReserveBook()
        {
            Console.WriteLine("Please enter book Id");
            int bookId;
            string result = Console.ReadLine();
            int.TryParse(result, out bookId);
            Console.Clear();
            using (var context = new AppDbContex())
            {


                var book = context.Books.FirstOrDefault(b => b.Id == bookId);

                if (book == null)
                {
                    Console.WriteLine("Book not found!");
                    return;
                }

                Console.WriteLine($"You are reserving book: {book.Name}");
                Console.Write("Enter your FinCode: ");
                string finCode = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(finCode) || finCode.Length != 7)
                {
                    Console.WriteLine("Invalid FinCode. It must be 7 characters.");
                    return;
                }


                Console.Write("Enter start date (yyyy-MM-dd): ");
                Console.Clear();
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("Invalid start date format!");
                    return;
                }


                Console.Write("Enter end date (yyyy-MM-dd): ");
                Console.Clear();
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
                {
                    Console.WriteLine("Invalid end date format!");
                    return;
                }


                if (startDate < DateTime.Now)
                {
                    Console.WriteLine("Start date cannot be in the past!");
                    return;
                }

                if (endTime <= startDate)
                {
                    Console.WriteLine("End date must be after start date!");
                    return;
                }


                bool conflict = context.ReservedItems.Any(r =>
                    r.BookId == bookId &&
                    r.Status != Status.Canceled &&
                    r.Status != Status.Completed &&
                    ((startDate >= r.StartDate && startDate <= r.EndTime) ||
                     (endTime >= r.StartDate && endTime <= r.EndTime)));

                if (conflict)
                {
                    Console.WriteLine("This book is already reserved in the selected date range!");
                    return;
                }

                var reservation = new ReservedItem
                {
                    FinCode = finCode,
                    BookId = bookId,
                    StartDate = startDate,
                    EndTime = endTime,
                    Status = Status.Confirmed
                };

                book.IsReserved = true;
                context.ReservedItems.Add(reservation);
                context.SaveChanges();

                Console.WriteLine("Book successfully reserved!");
                Console.WriteLine($"Reservation Period: {startDate:yyyy-MM-dd} → {endTime:yyyy-MM-dd}");
                Console.WriteLine($"Status: {reservation.Status}");
            }
        }


        public List<ReservedItem> ReservationList()
        {
            using (var context = new AppDbContex())
            {
                var reservations = context.ReservedItems
              .Include(r => r.Book)
              .OrderBy(r => r.Status)
              .ToList();

                if (reservations.Count == 0)
                {
                    Console.WriteLine("No reservations found!");
                }
                else
                {
                    foreach (var item in reservations)
                    {
                        Console.WriteLine($"Reservation Id: {item.Id}");
                        Console.WriteLine($"Book: {item.Book?.Name ?? "Book not found"}");
                        Console.WriteLine($"Reserved Date: {item.EndTime}");
                        Console.WriteLine($"Status: {item.Status}");
                    }
                }

                return reservations;
            }
        }


        public void ChangeReservationStatus()
        {
            using (var context = new AppDbContex())
            {
                Console.WriteLine("Please enter Reservation ID:");
                string id = Console.ReadLine();
                if (!int.TryParse(id, out int reservationId))
                {
                    Console.WriteLine("Invalid ID format!");
                    return;
                }

                var reservation = context.ReservedItems.FirstOrDefault(r => r.Id == reservationId);
                if (reservation == null)
                {
                    Console.WriteLine("Reservation not found!");
                    return;
                }

                Console.WriteLine($"\nCurrent Status: {reservation.Status}");
                Console.WriteLine("Select new status:");
                foreach (var value in Enum.GetValues(typeof(Status)))
                {
                    Console.WriteLine($"{(int)value} - {value}");
                }

                string statusInput = Console.ReadLine();
                if (!int.TryParse(statusInput, out int statusNumber) ||
                    !Enum.IsDefined(typeof(Status), statusNumber))
                {
                    Console.WriteLine("Invalid status selection!");
                    return;
                }

                reservation.Status = (Status)statusNumber;

                context.ReservedItems.Update(reservation);
                context.SaveChanges();

                Console.WriteLine("Reservation status successfully updated!");
            }
        }


        public List<ReservedItem> UsersReservationsist()
        {

            Console.WriteLine("Please enter FinCode:");
            string finCode = Console.ReadLine()?.Trim();
            Console.Clear();

            if (string.IsNullOrWhiteSpace(finCode))
            {
                Console.WriteLine("FinCode cannot be empty!");
                return new List<ReservedItem>();
            }

            using (var context = new AppDbContex())
            {
               
                var reservations = context.ReservedItems
                    .Include(r => r.Book) 
                    .Where(r => r.FinCode == finCode)
                    .OrderBy(r => r.Status)
                    .ToList();

                if (reservations.Count == 0)
                {
                    Console.WriteLine("No reservations found for this FinCode!");
                }
                else
                {
                    Console.WriteLine($"Reservations for FinCode: {finCode}");
                    Console.WriteLine("---------------------------");

                    foreach (var item in reservations)
                    {
                        Console.WriteLine($"Reservation Id: {item.Id}");
                        Console.WriteLine($"Book: {item.Book?.Name ?? "Book not found"}");
                        Console.WriteLine($"Status: {item.Status}");
                        Console.WriteLine($"Reserved Date: {item.EndTime}");
                        
                    }
                }

                return reservations;
            }

        }


    }
}



