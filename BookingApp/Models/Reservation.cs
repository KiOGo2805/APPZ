namespace BookingApp.Models;

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