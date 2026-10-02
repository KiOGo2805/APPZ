using System.Text;
using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Controllers;
using BookingApp.Interfaces;
using BookingApp.Services.Notifications;
using BookingApp.Factories;
using BookingApp.Builders;
using BookingApp.Pools;



namespace BookingApp
{
    /// <summary>
    /// Entry point for the booking application demo.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs the factory-pattern demonstration.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== ДЕМОНСТРАЦІЯ ЛР №4: CREATIONAL PATTERNS ===\n");

            var customer = new Customer { Id = 1 };
            var reservation = new Reservation { Id = 101 };

            Console.WriteLine("--- 1. Factory Method ---");
            NotificationCreator emailCreator = new EmailNotificationCreator();
            emailCreator.Notify(customer, reservation);

            NotificationCreator smsCreator = new SmsNotificationCreator();
            smsCreator.Notify(customer, reservation);

            Console.WriteLine();

            Console.WriteLine("--- 2. Abstract Factory ---");

            IRestaurantZoneFactory vipFactory = new VipZoneFactory();
            ITableSetup vipTable = vipFactory.CreateTableSetup();
            IMenuPlan vipMenu = vipFactory.CreateMenuPlan();
            vipTable.Setup(5);
            vipMenu.PresentMenu(2);

            IRestaurantZoneFactory standardFactory = new StandardZoneFactory();
            ITableSetup standardTable = standardFactory.CreateTableSetup();
            IMenuPlan standardMenu = standardFactory.CreateMenuPlan();
            standardTable.Setup(12);
            standardMenu.PresentMenu(4);

            Console.WriteLine("\nРоботу завершено успішно.");

            Console.WriteLine("\n=== ДЕМОНСТРАЦІЯ ЛР №5: CREATIONAL PATTERNS ===\n");

            Console.WriteLine("--- 3. Builder Pattern ---");
            IReservationBuilder builder = new VipBanquetBuilder();
            BanquetReservation banquet = builder
                .SetCustomer("АнатолійЧОМУ")
                .SetGuests(25)
                .AddCatering()
                .AddMusicBand()
                .Build();
            banquet.Display();

            Console.WriteLine("\n--- 4. Object Pool Pattern ---");
            OrderTicketPool pool = new OrderTicketPool();

            OrderTicket ticket1 = pool.Acquire();
            ticket1.OrderDetails = "Кава, Десерт";
            ticket1.Print();

            OrderTicket ticket2 = pool.Acquire();
            ticket2.OrderDetails = "Паста, Сік";
            ticket2.Print();

            pool.Release(ticket1);

            OrderTicket ticket3 = pool.Acquire();
            ticket3.OrderDetails = "Стейк, Вино";
            ticket3.Print();

            Console.WriteLine("\nРоботу завершено успішно.");
        }
    }
}