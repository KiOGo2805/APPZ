namespace BookingApp.Services;

using BookingApp.Models;

/// <summary>
/// Основна сутність ресторану в предметній області.
/// Клас агрегує доступні столики та поточні бронювання, тому він виступає як <c>Information Expert</c>
/// для інформації про всі ресурси закладу та їх стан.
/// </summary>
public class Restaurant
{
    /// <summary>
    /// Назва ресторану, яка відображається в інтерфейсі системи та логах.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Список усіх столиків, що належать ресторану.
    /// </summary>
    public List<Table> Tables { get; set; } = new List<Table>();

    /// <summary>
    /// Повертає перший доступний стіл, що задовольняє необхідну місткість.
    /// </summary>
    /// <param name="requiredCapacity">Необхідна кількість місць для гостей.</param>
    /// <param name="time">Час, на який здійснюється перевірка доступності.</param>
    /// <returns>Об'єкт <see cref="Table"/> або <c>null</c>, якщо вільний стіл не знайдено.</returns>
    public Table? GetAvailableTable(int requiredCapacity, DateTime time)
    {
        Console.WriteLine($"[Restaurant.GetAvailableTable] Пошук столика на {requiredCapacity} осіб. Час: {time}");

        // Заглушка: шукаємо перший вільний стіл, який вміщує потрібну кількість людей
        return Tables.FirstOrDefault(t => t.Capacity >= requiredCapacity && t.IsAvailable);
    }

    /// <summary>
    /// Список активних або історичних бронювань, пов'язаних з рестораном.
    /// </summary>
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}