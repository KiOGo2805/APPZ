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
using BookingApp.Iterators;
using BookingApp.Mediators;

namespace BookingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int targetReservationId = 1042;
            IReservationManager adminProxy = new ReservationManagerProxy("Admin");

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
                },
                new Table
                {
                    Id = 2,
                    Capacity = 2,
                    SharedType = new TableType("Дерево", "Тераса", "Столик на терасі")
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

                Console.WriteLine("\n=== ТЕСТУВАННЯ MEDIATOR ===\n");

                ReservationMediator mediator = new(
                    restaurant,
                    notificationService,
                    restaurant.Reservations);
                Customer secondCustomer = new()
                {
                    Id = 2,
                    FullName = "Андрій Мельник",
                    PhoneNumber = "+380000000001",
                    Email = "andrii@example.com"
                };
                mediator.CreateReservation(secondCustomer, 2, request.StartTime);
            }

            Console.WriteLine("\n=== ТЕСТУВАННЯ ITERATOR ===\n");

            IReservationIterator reservationIterator = restaurant.Reservations.CreateIterator();
            while (reservationIterator.HasNext())
            {
                Reservation reservation = reservationIterator.Next();
                Console.WriteLine(
                    $"Бронювання №{reservation.Id}: {reservation.Client?.FullName}, " +
                    $"столик №{reservation.ReservedTable?.Id}, час {reservation.StartTime}, " +
                    $"статус {(reservation.IsActive ? "активне" : "скасоване")}.");
            }

            Console.ReadLine();
        }
    }
}