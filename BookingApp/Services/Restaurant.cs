namespace BookingApp.Services;

using BookingApp.Models;

/// <summary>
/// Ресторан, що агрегує столики.
/// Information Expert щодо стану всіх столиків у закладі.
/// </summary>
public class Restaurant
{
    public string Name { get; set; } = string.Empty;
    public List<Table> Tables { get; set; } = new List<Table>();

    public Table? GetAvailableTable(int requiredCapacity, DateTime time)
    {
        Console.WriteLine($"[Restaurant.GetAvailableTable] Пошук столика на {requiredCapacity} осіб. Час: {time}");

        // Заглушка: шукаємо перший вільний стіл, який вміщує потрібну кількість людей
        return Tables.FirstOrDefault(t => t.Capacity >= requiredCapacity && t.IsAvailable);
    }

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}