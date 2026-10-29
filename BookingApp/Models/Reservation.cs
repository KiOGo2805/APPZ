namespace BookingApp.Models;

using BookingApp.Mementos;

/// <summary>
/// Представляє бронювання столика.
/// </summary>
/// <remarks>
/// Клас описує зв'язок між клієнтом, столиком і часовими параметрами бронювання.
/// </remarks>
public class Reservation
{
    /// <summary>
    /// Отримує або задає унікальний ідентифікатор бронювання.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Отримує або задає столик, який було заброньовано.
    /// </summary>
    public Table? ReservedTable { get; set; }

    /// <summary>
    /// Отримує або задає клієнта, який здійснює бронювання.
    /// </summary>
    public Customer? Client { get; set; }

    /// <summary>
    /// Отримує або задає час початку бронювання.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Отримує або задає тривалість бронювання.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Отримує або задає значення, що показує, чи бронювання активне.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Створює незмінний знімок значущого стану поточного бронювання.
    /// </summary>
    /// <returns>Знімок, який можна передати до історії бронювань.</returns>
    internal ReservationMemento CreateMemento() =>
        new(IsActive, StartTime, Duration, ReservedTable, ReservedTable?.IsAvailable);

    /// <summary>
    /// Відновлює поля бронювання та доступність його столика зі знімка.
    /// </summary>
    /// <param name="memento">Знімок попереднього стану.</param>
    internal void RestoreMemento(ReservationMemento memento)
    {
        ArgumentNullException.ThrowIfNull(memento);
        IsActive = memento.IsActive;
        StartTime = memento.StartTime;
        Duration = memento.Duration;
        ReservedTable = memento.ReservedTable;

        if (ReservedTable is not null && memento.TableWasAvailable.HasValue)
        {
            ReservedTable.IsAvailable = memento.TableWasAvailable.Value;
        }
    }

    /// <summary>
    /// Скасовує поточне бронювання.
    /// </summary>
    public void CancelReservation()
    {
        Console.WriteLine($"[Reservation.CancelReservation] Викликано для броні Id={Id}");
        IsActive = false;
        if (ReservedTable != null)
        {
            ReservedTable.MarkAsAvailable();
        }
    }
}