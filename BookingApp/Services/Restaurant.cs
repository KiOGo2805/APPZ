namespace BookingApp.Services;

using BookingApp.Models;

/// <summary>
/// Ресторан, що агрегує столики.
/// Реалізує шаблон Singleton (Одинак).
/// Гарантує, що в системі існує лише один екземпляр ресторану, 
/// і надає глобальну точку доступу до його ресурсів (столиків).
/// </summary>
/// <remarks>
/// Клас зберігає дані про доступні ресурси закладу й відповідає за пошук вільного столика.
/// </remarks>
public class Restaurant
{
    private static readonly Lazy<Restaurant> _instance = new Lazy<Restaurant>(() => new Restaurant());

    /// <summary>
    /// Отримує або задає назву ресторану.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або задає список столиків ресторану.
    /// </summary>
    public List<Table> Tables { get; set; } = new List<Table>();

    /// <summary>
    /// Приватний конструктор (Singleton).
    /// Забороняє створення екземплярів класу ззовні за допомогою оператора new.
    /// </summary>
    private Restaurant()
    {
        Console.WriteLine("[Restaurant] Створено єдиний екземпляр ресторану (Singleton).");
    }

    /// <summary>
    /// Глобальна точка доступу до екземпляра ресторану (Singleton).
    /// </summary>
    public static Restaurant Instance => _instance.Value;

    /// <summary>
    /// Повертає перший доступний стіл, який відповідає потрібній місткості.
    /// </summary>
    /// <param name="requiredCapacity">Потрібна кількість місць.</param>
    /// <param name="time">Час перевірки доступності.</param>
    /// <returns>Вільний стіл або <c>null</c>, якщо підходящого немає.</returns>
    public Table? GetAvailableTable(int requiredCapacity, DateTime time)
    {
        Console.WriteLine($"[Restaurant.GetAvailableTable] Пошук столика на {requiredCapacity} осіб. Час: {time}");
        return Tables.FirstOrDefault(t => t.Capacity >= requiredCapacity && t.IsAvailable);
    }
}