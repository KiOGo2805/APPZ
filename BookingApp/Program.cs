using System.Text;
using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Controllers;
using BookingApp.Interfaces;
using BookingApp.Services.Notifications;
using BookingApp.Factories;



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

            // Тестові дані
            var customer = new Customer { Id = 1 };
            var reservation = new Reservation { Id = 101 };

            // 1. ДЕМОНСТРАЦІЯ ФАБРИЧНОГО МЕТОДУ (FACTORY METHOD)
            Console.WriteLine("--- 1. Factory Method ---");
            NotificationCreator emailCreator = new EmailNotificationCreator();
            emailCreator.Notify(customer, reservation);

            NotificationCreator smsCreator = new SmsNotificationCreator();
            smsCreator.Notify(customer, reservation);

            Console.WriteLine();

            // 2. ДЕМОНСТРАЦІЯ АБСТРАКТНОЇ ФАБРИКИ (ABSTRACT FACTORY)
            Console.WriteLine("--- 2. Abstract Factory ---");
            
            // Клієнтський код обирає VIP-зону
            IRestaurantZoneFactory vipFactory = new VipZoneFactory();
            ITableSetup vipTable = vipFactory.CreateTableSetup();
            IMenuPlan vipMenu = vipFactory.CreateMenuPlan();
            vipTable.Setup(5);
            vipMenu.PresentMenu(2);

            // Клієнтський код обирає Стандартну зону
            IRestaurantZoneFactory standardFactory = new StandardZoneFactory();
            ITableSetup standardTable = standardFactory.CreateTableSetup();
            IMenuPlan standardMenu = standardFactory.CreateMenuPlan();
            standardTable.Setup(12);
            standardMenu.PresentMenu(4);

            Console.WriteLine("\nРоботу завершено успішно.");
        }
    }
}