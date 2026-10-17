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

            Console.WriteLine("=== COMPOSITE (Деревовидна структура: Меню) ===\n");
            
            var mainCourse = new Dish("Стейк", 350m);
            var drink = new Dish("Сік", 50m);
            
            var dinnerCombo = new ComboMeal("Вечірнє комбо");
            dinnerCombo.Add(mainCourse);
            dinnerCombo.Add(drink);
            
            dinnerCombo.Display();
            Console.WriteLine();
            dinnerCombo.GetPrice();


            Console.WriteLine("\n\n=== FLYWEIGHT (Пристосуванець: Столики) ===\n");

            var tableFactory = new TableFactory();


            var terraceTableType = tableFactory.GetTableType("Ротанг", "Тераса", "Вид на річку");


            var baseTable = new Table 
            { 
                Id = 1, 
                Capacity = 4, 
                SharedType = terraceTableType
            };


            var clonedTable = baseTable.Clone();
            clonedTable.Id = 2; // Змінюємо лише унікальні дані


            Console.WriteLine("\nСтворені столики:");
            baseTable.DisplayFullInfo();
            clonedTable.DisplayFullInfo();


            Console.WriteLine();
            tableFactory.DisplayCacheStats(); 

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