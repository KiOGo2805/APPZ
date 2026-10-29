using System;
using BookingApp.Models;
using BookingApp.Composite;
using BookingApp.Flyweight;
using BookingApp.Decorators;
using BookingApp.Proxies;
using BookingApp.ChainOfResponsibility;
using BookingApp.Commands;
using BookingApp.Controllers;
using BookingApp.Interfaces;
using BookingApp.Services;
using BookingApp.Services.Notifications;

namespace BookingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("=== ТЕСТУВАННЯ DECORATOR ===\n");

            IReservationComponent myReservation = new BaseReservation();
            

            myReservation = new TableDecorationDecorator(myReservation);
            

            myReservation = new LiveMusicDecorator(myReservation);


            Console.WriteLine("\n--- Підсумковий рахунок ---");
            Console.WriteLine($"Опис: {myReservation.GetDescription()}");
            Console.WriteLine($"Загальна сума: {myReservation.GetCost()} грн.\n");



            Console.WriteLine("\n=== ТЕСТУВАННЯ PROXY ===\n");

            int targetReservationId = 1042;


            Console.WriteLine("--- Спроба скасування: Звичайний користувач ---");
            IReservationManager userProxy = new ReservationManagerProxy("Guest");
            userProxy.CancelReservation(targetReservationId);

            Console.WriteLine("\n----------------------------------\n");

            Console.WriteLine("--- Спроба скасування: Адміністратор ---");
            IReservationManager adminProxy = new ReservationManagerProxy("Admin");
            adminProxy.CancelReservation(targetReservationId);

            Console.WriteLine("\n=== ТЕСТУВАННЯ CHAIN OF RESPONSIBILITY ===\n");

            Restaurant restaurant = Restaurant.Instance;
            restaurant.Name = "Ресторан";
            restaurant.Tables =
            [
                new Table
                {
                    Id = 1,
                    Capacity = 4,
                    SharedType = new TableType("Дерево", "Головна зала", "Стандартний столик")
                }
            ];

            IReservationValidationHandler validationChain = new CustomerValidationHandler();
            validationChain
                .SetNext(new GuestCountValidationHandler())
                .SetNext(new ReservationTimeValidationHandler())
                .SetNext(new TableAvailabilityValidationHandler(restaurant));

            Customer customer = new Customer
            {
                Id = 1,
                FullName = "Олена Коваленко",
                PhoneNumber = "+380000000000",
                Email = "olena@example.com"
            };
            ReservationRequest request = new(customer, 3, DateTime.Now.AddDays(1));
            ReservationValidationResult validationResult = validationChain.Handle(request);
            Console.WriteLine($"Результат перевірки: {validationResult.Message}");

            Console.WriteLine("\n=== ТЕСТУВАННЯ COMMAND ===\n");

            if (validationResult.IsValid)
            {
                INotificationService notificationService = new EmailNotification();
                ReservationController controller = new(restaurant, notificationService);
                ReservationCommandInvoker invoker = new();
                invoker.Execute(new MakeReservationCommand(
                    controller,
                    customer,
                    request.GuestCount,
                    request.StartTime));
                invoker.Execute(new CancelReservationCommand(adminProxy, targetReservationId));
            }

            Console.ReadLine();

            
        }
    }
}