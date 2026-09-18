namespace BookingApp.Models;

/// <summary>
/// Модель столика ресторану.
/// Клас інкапсулює дані про місце для розміщення відвідувачів, а також його поточну доступність.
/// Завдяки цьому він є <c>Information Expert</c> для інформації про стан конкретного столика.
/// </summary>
public class Table
{
    /// <summary>
    /// Унікальний ідентифікатор столика в закладі.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Кількість гостей, яких може прийняти даний стіл.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Вказує, чи доступний стіл для нової броні.
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Позначає стіл як зайнятий після успішного створення бронювання.
    /// </summary>
    public void MarkAsReserved()
    {
        Console.WriteLine($"[Table.MarkAsReserved] Викликано для столика Id={Id}");
        IsAvailable = false;
    }

    /// <summary>
    /// Звільняє стіл після скасування або завершення бронювання.
    /// </summary>
    public void MarkAsAvailable()
    {
        Console.WriteLine($"[Table.MarkAsAvailable] Викликано для столика Id={Id}");
        IsAvailable = true;
    }
}