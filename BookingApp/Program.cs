using System.Text;
using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Controllers;
using BookingApp.Interfaces;
using BookingApp.Adapters;
using BookingApp.Bridge;

namespace BookingApp
{
    /// <summary>
    /// Точка входу та демонстрація структурних шаблонів проєкту.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Запускає демонстрацію шаблонів Adapter і Bridge.
        /// </summary>
        /// <param name="args">Аргументи командного рядка.</param>
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var restaurant = Restaurant.Instance;
            restaurant.Tables.Add(new Table { Id = 1, Capacity = 2, IsAvailable = true });
            restaurant.Tables.Add(new Table { Id = 2, Capacity = 4, IsAvailable = true });

            var customer = new Customer
            {
                Id = 1,
                FullName = "Степан Мельник",
                PhoneNumber = "+380501234567"
            };

            Console.WriteLine("=== ДЕМОНСТРАЦІЯ ЛР №6: STRUCTURAL PATTERNS ===\n");

            Console.WriteLine("--- 1. Adapter Pattern ---");
            var telegramApi = new ExternalTelegramApi();

            INotificationService telegramAdapter = new TelegramNotificationAdapter(telegramApi);

            var controllerWithAdapter = new ReservationController(restaurant, telegramAdapter);

            controllerWithAdapter.MakeReservation(customer, 2, DateTime.Now.AddDays(1));


            Console.WriteLine("\n--- 2. Bridge Pattern ---");

            var bridgeReservation = new Reservation
            {
                Id = 999,
                ReservedTable = restaurant.Tables[1],
                StartTime = DateTime.Now.AddHours(5)
            };

            IMessageSender emailSender = new EmailSender();
            NotificationMessage emailConfirmation = new ConfirmationMessage(emailSender);
            emailConfirmation.ProcessNotification(customer, bridgeReservation);

            Console.WriteLine();

            IMessageSender smsSender = new SmsSender();
            NotificationMessage smsCancellation = new CancellationMessage(smsSender);
            smsCancellation.ProcessNotification(customer, bridgeReservation);

            Console.WriteLine("\nРоботу завершено успішно.");
        }
    }
}