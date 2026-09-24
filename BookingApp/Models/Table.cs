namespace BookingApp.Models;

/// <summary>
/// Клас предметної області, що описує столик у закладі.
/// Реалізує шаблон Prototype (Прототип).
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
    /// Створює точну копію поточного об'єкта (Prototype).
    /// Це дозволяє швидко генерувати набір однакових посадкових місць 
    /// без необхідності щоразу викликати конструктор і налаштовувати атрибути.
    /// </summary>
    /// <returns>Клон поточного столика.</returns>
    public Table Clone()
    {
        Console.WriteLine($"[Table.Clone] Клонування (Prototype) столика-шаблону на {Capacity} осіб.");
        return (Table)this.MemberwiseClone();
    }

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