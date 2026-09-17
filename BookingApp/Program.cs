using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Controllers;
using BookingApp.Interfaces;

Console.WriteLine("=== Лабораторна робота №1: Принципи GRASP ===");
Console.WriteLine("=== Предметна область: Бронювання столиків ===\n");

// 1. Ініціалізація даних (Setup)
Restaurant myRestaurant = new Restaurant { Name = "C# Resto" };
myRestaurant.Tables.Add(new Table { Id = 1, Capacity = 2 }); // Столик для двох
myRestaurant.Tables.Add(new Table { Id = 2, Capacity = 4 }); // Столик для чотирьох
myRestaurant.Tables.Add(new Table { Id = 3, Capacity = 6 }); // Столик для шістьох

INotificationService notificationService = new NotificationService();
ReservationController controller = new ReservationController(myRestaurant, notificationService);

Customer customer1 = new Customer { Id = 101, FullName = "Іван Іванов", PhoneNumber = "+380501234567" };
Customer customer2 = new Customer { Id = 102, FullName = "Петро Петров", PhoneNumber = "+380671234567" };

// 2. Симуляція роботи: Успішне бронювання
controller.MakeReservation(customer1, 4, DateTime.Now.AddHours(2));

// 3. Симуляція роботи: Спроба забронювати столик, якого вже немає (на 4 особи)
controller.MakeReservation(customer2, 4, DateTime.Now.AddHours(2));

// 4. Тестування додаткових методів-заглушок
Console.WriteLine("\n--- ТЕСТУВАННЯ ІНШИХ МЕТОДІВ ---");
customer1.UpdateContactInfo("+380999999999");

Reservation testCancel = new Reservation { Id = 1, ReservedTable = myRestaurant.Tables[0] };
testCancel.CancelReservation();

Console.WriteLine("\nРоботу завершено.");