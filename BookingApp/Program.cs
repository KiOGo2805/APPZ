using System;
using BookingApp.Models;
using BookingApp.Composite;
using BookingApp.Flyweight;
using BookingApp.Decorators;
using BookingApp.Proxies;

namespace BookingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Демонстрація Composite (Меню) ---
            Console.WriteLine("=== COMPOSITE (Деревовидна структура: Меню) ===\n");
            
            var mainCourse = new Dish("Стейк", 350m);
            var drink = new Dish("Сік", 50m);
            
            var dinnerCombo = new ComboMeal("Вечірнє комбо");
            dinnerCombo.Add(mainCourse);
            dinnerCombo.Add(drink);
            
            dinnerCombo.Display();
            Console.WriteLine();
            dinnerCombo.GetPrice();


            // --- Демонстрація Flyweight + Prototype (Столики) ---
            Console.WriteLine("\n\n=== FLYWEIGHT (Пристосуванець: Столики) ===\n");

            var tableFactory = new TableFactory();

            // Отримуємо спільні характеристики для столиків на терасі
            var terraceTableType = tableFactory.GetTableType("Ротанг", "Тераса", "Вид на річку");

            // Створюємо базовий столик (використовуючи твій існуючий код)
            var baseTable = new Table 
            { 
                Id = 1, 
                Capacity = 4, 
                SharedType = terraceTableType // Підключаємо Flyweight
            };

            // Використовуємо твій існуючий метод Prototype (Clone) для створення другого столика
            var clonedTable = baseTable.Clone();
            clonedTable.Id = 2; // Змінюємо лише унікальні дані

            // Демонструємо, що другий столик перевикористав той самий Flyweight з кешу
            Console.WriteLine("\nСтворені столики:");
            baseTable.DisplayFullInfo();
            clonedTable.DisplayFullInfo();

            // Перевіряємо скільки важких об'єктів з характеристиками реально створено в пам'яті
            Console.WriteLine();
            tableFactory.DisplayCacheStats(); // Повинно показати "1", незважаючи на 2 столики

            Console.ReadLine();



            Console.WriteLine("=== ТЕСТУВАННЯ DECORATOR ===\n");

            // 1. Створюємо базове бронювання
            IReservationComponent myReservation = new BaseReservation();
            
            // 2. Клієнт хоче прикрасити стіл (загортаємо в декоратор)
            myReservation = new TableDecorationDecorator(myReservation);
            
            // 3. Клієнт ще й замовляє живу музику (загортаємо ще раз)
            myReservation = new LiveMusicDecorator(myReservation);

            // Виконуємо розрахунок (виклик пройде через усі обгортки)
            Console.WriteLine("\n--- Підсумковий рахунок ---");
            Console.WriteLine($"Опис: {myReservation.GetDescription()}");
            Console.WriteLine($"Загальна сума: {myReservation.GetCost()} грн.\n");


            // ==========================================
            // ДЕМОНСТРАЦІЯ: PROXY (Контроль доступу)
            // ==========================================
            Console.WriteLine("\n=== ТЕСТУВАННЯ PROXY ===\n");

            int targetReservationId = 1042;

            // Спроба 1: Звичайний користувач
            Console.WriteLine("--- Спроба скасування: Звичайний користувач ---");
            IReservationManager userProxy = new ReservationManagerProxy("Guest");
            userProxy.CancelReservation(targetReservationId);

            Console.WriteLine("\n----------------------------------\n");

            // Спроба 2: Адміністратор
            Console.WriteLine("--- Спроба скасування: Адміністратор ---");
            IReservationManager adminProxy = new ReservationManagerProxy("Admin");
            adminProxy.CancelReservation(targetReservationId);

            Console.ReadLine();

            
        }
    }
}