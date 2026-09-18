namespace BookingApp.Services;

using BookingApp.Models;

/// <summary>
/// Представляє ресторан як агрегатор столиків і бронювань.
/// </summary>
/// <remarks>
/// Клас зберігає дані про доступні ресурси закладу й відповідає за пошук вільного столика.
/// </remarks>
public class Restaurant
{
    /// <summary>
    /// Отримує або задає назву ресторану.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або задає список столиків ресторану.
    /// </summary>
    public List<Table> Tables { get; set; } = new List<Table>();

    /// <summary>
    /// Повертає перший доступний стіл, який відповідає потрібній місткості.
    /// </summary>
    /// <param name="requiredCapacity">Потрібна кількість місць.</param>
    /// <param name="time">Час перевірки доступності.</param>
    /// <returns>Вільний стіл або <c>null</c>, якщо підходящого немає.</returns>
    public Table? GetAvailableTable(int requiredCapacity, DateTime time)
    {
        Console.WriteLine($"[Restaurant.GetAvailableTable] Пошук столика на {requiredCapacity} осіб. Час: {time}");

        // Заглушка: шукаємо перший вільний стіл, який вміщує потрібну кількість людей
        return Tables.FirstOrDefault(t => t.Capacity >= requiredCapacity && t.IsAvailable);
    }

    /// <summary>
    /// Отримує або задає список бронювань, пов'язаних із рестораном.
    /// </summary>
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}