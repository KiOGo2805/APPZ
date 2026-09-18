namespace BookingApp.Models;

/// <summary>
/// Представляє стіл у ресторані.
/// </summary>
/// <remarks>
/// Клас містить дані про місткість та доступність столика і є інформаційним експертом для його стану.
/// </remarks>
public class Table
{
    /// <summary>
    /// Отримує або задає унікальний ідентифікатор столика.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Отримує або задає кількість місць за столом.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Отримує або задає значення, що показує, чи столик доступний для бронювання.
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Позначає стіл як зайнятий.
    /// </summary>
    public void MarkAsReserved()
    {
        Console.WriteLine($"[Table.MarkAsReserved] Викликано для столика Id={Id}");
        IsAvailable = false;
    }

    /// <summary>
    /// Позначає стіл як вільний.
    /// </summary>
    public void MarkAsAvailable()
    {
        Console.WriteLine($"[Table.MarkAsAvailable] Викликано для столика Id={Id}");
        IsAvailable = true;
    }
}