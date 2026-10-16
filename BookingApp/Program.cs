using System;
using BookingApp.Models;
using BookingApp.Composite;
using BookingApp.Flyweight;

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
        }
    }
}