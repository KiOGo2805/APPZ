using BookingApp.Flyweight; // Додаємо using для доступу до TableType
using System;

namespace BookingApp.Models;

/// <summary>
/// Клас предметної області, що описує столик у закладі.
/// Реалізує шаблон Prototype (Прототип) та виступає Контекстом для Flyweight.
/// </summary>
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
    /// Посилання на спільний стан (Flyweight), який описує характеристики цього типу столиків.
    /// </summary>
    public required TableType SharedType { get; set; }

    /// <summary>
    /// Створює точну копію поточного об'єкта (Prototype).
    /// </summary>
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

    /// <summary>
    /// Відображає повну інформацію про столик, об'єднуючи унікальні дані з кешованими характеристиками.
    /// </summary>
    public void DisplayFullInfo()
    {
        Console.Write($"Столик №{Id} [{Capacity} місць] | Статус: {(IsAvailable ? "Вільний" : "Зайнятий")} | Характеристики: ");
        if (SharedType != null)
        {
            SharedType.Display();
        }
        else
        {
            Console.WriteLine("Без додаткових характеристик.");
        }
    }
}