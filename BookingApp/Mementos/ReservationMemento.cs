using BookingApp.Models;

namespace BookingApp.Mementos;

/// <summary>
/// Незмінний знімок стану бронювання для відновлення попереднього стану.
/// </summary>
public sealed class ReservationMemento
{
    internal bool IsActive { get; }
    internal DateTime StartTime { get; }
    internal TimeSpan Duration { get; }
    internal Table? ReservedTable { get; }
    internal bool? TableWasAvailable { get; }

    internal ReservationMemento(
        bool isActive,
        DateTime startTime,
        TimeSpan duration,
        Table? reservedTable,
        bool? tableWasAvailable)
    {
        IsActive = isActive;
        StartTime = startTime;
        Duration = duration;
        ReservedTable = reservedTable;
        TableWasAvailable = tableWasAvailable;
    }
}

/// <summary>
/// Зберігає історію знімків і відновлює бронювання у зворотному порядку.
/// </summary>
public sealed class ReservationHistory
{
    private readonly Stack<(Reservation Reservation, ReservationMemento Memento)> _history = new();

    /// <summary>
    /// Кількість збережених станів, доступних для відновлення.
    /// </summary>
    public int Count => _history.Count;

    /// <summary>
    /// Зберігає поточний стан бронювання перед його зміною.
    /// </summary>
    /// <param name="reservation">Бронювання, стан якого потрібно зберегти.</param>
    public void Save(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        _history.Push((reservation, reservation.CreateMemento()));
        Console.WriteLine($"[ReservationHistory.Save] Збережено стан бронювання №{reservation.Id}.");
    }

    /// <summary>
    /// Відновлює останній збережений стан бронювання.
    /// </summary>
    /// <returns><see langword="true"/>, якщо стан було відновлено; інакше <see langword="false"/>.</returns>
    public bool UndoLastChange()
    {
        if (!_history.TryPop(out (Reservation Reservation, ReservationMemento Memento) entry))
        {
            Console.WriteLine("[ReservationHistory.UndoLastChange] Немає збережених станів для відновлення.");
            return false;
        }

        entry.Reservation.RestoreMemento(entry.Memento);
        Console.WriteLine($"[ReservationHistory.UndoLastChange] Відновлено стан бронювання №{entry.Reservation.Id}.");
        return true;
    }
}
