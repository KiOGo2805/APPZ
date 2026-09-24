using System.Text;
using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Controllers;
using BookingApp.Interfaces;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("=== Лабораторна робота №3: Породжувальні шаблони ===");
Console.WriteLine("=== Шаблони: Singleton та Prototype ===\n");

// 1. Демонстрація Singleton (Одинак)
// Ми більше не використовуємо `new Restaurant()`, а звертаємось до єдиного екземпляра
Restaurant myRestaurant = Restaurant.Instance;
myRestaurant.Name = "C# Resto (Singleton)";

// Перевірка Singleton:
Restaurant anotherReference = Restaurant.Instance;
Console.WriteLine($"[Singleton Test] Чи посилаються змінні на один об'єкт? : {ReferenceEquals(myRestaurant, anotherReference)}\n");

// 2. Демонстрація Prototype (Прототип)
// Створюємо базовий шаблон столика на 4 особи
Table baseFourPersonTable = new Table { Id = 1, Capacity = 4 };
myRestaurant.Tables.Add(baseFourPersonTable);

// Клонуємо столики замість створення з нуля
Table clonedTable1 = baseFourPersonTable.Clone();
clonedTable1.Id = 2; // Змінюємо лише унікальний ідентифікатор

Table clonedTable2 = baseFourPersonTable.Clone();
clonedTable2.Id = 3;
clonedTable2.Capacity = 6; // Робимо цей клонований стіл на 6 осіб

myRestaurant.Tables.Add(clonedTable1);
myRestaurant.Tables.Add(clonedTable2);

Console.WriteLine($"\n[Prototype Test] У ресторані створено {myRestaurant.Tables.Count} столики(-ів).\n");

// 3. Ініціалізація сервісів та контролера
INotificationService notificationService = new NotificationService();
ReservationController controller = new ReservationController(myRestaurant, notificationService);

Customer customer1 = new Customer { Id = 101, FullName = "Іван Іванов", PhoneNumber = "+380501234567" };
Customer customer2 = new Customer { Id = 102, FullName = "Петро Петров", PhoneNumber = "+380671234567" };
Customer customer3 = new Customer { Id = 103, FullName = "Олена Коваленко", PhoneNumber = "+380509999999" };

// 4. Симуляція роботи: Успішне бронювання
controller.MakeReservation(customer1, 4, DateTime.Now.AddHours(2));

// 5. Симуляція роботи: Бронюємо ще один на 4 особи (їх всього 2)
controller.MakeReservation(customer2, 4, DateTime.Now.AddHours(2));

// 6. Симуляція роботи: Спроба забронювати столик, якого вже немає (обидва на 4 особи вже зайняті)
controller.MakeReservation(customer3, 4, DateTime.Now.AddHours(2));

// 7. Тестування додаткових методів-заглушок
Console.WriteLine("\n--- ТЕСТУВАННЯ ІНШИХ МЕТОДІВ ---");
customer1.UpdateContactInfo("+380999999999");

Reservation testCancel = new Reservation { Id = 1, ReservedTable = myRestaurant.Tables[0] };
testCancel.CancelReservation();

Console.WriteLine("\nРоботу завершено.");