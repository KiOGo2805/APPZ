namespace BookingApp.Models;

/// <summary>
/// Сутність, що фіксує факт бронювання (Асоціація між Customer та Table).
/// </summary>
public class Reservation
{
    public int Id { get; set; }
    public Table? ReservedTable { get; set; }
    public Customer? Client { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsActive { get; set; } = true;

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