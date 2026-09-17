namespace BookingApp.Models;

/// <summary>
/// Клас предметної області, що описує столик у закладі.
/// Information Expert щодо власного стану (зайнятий/вільний).
/// </summary>
public class Table
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; } = true;

    public void MarkAsReserved()
    {
        Console.WriteLine($"[Table.MarkAsReserved] Викликано для столика Id={Id}");
        IsAvailable = false;
    }

    public void MarkAsAvailable()
    {
        Console.WriteLine($"[Table.MarkAsAvailable] Викликано для столика Id={Id}");
        IsAvailable = true;
    }
}