namespace BookingApp.Models;

/// <summary>
/// Модель бронювання ресторанного столика.
/// Представляє зв'язок між клієнтом та обраним столиком, а також дату та тривалість візиту.
/// Цей клас описує результат операції бронювання в системі та є основною сутністю для обробки
/// життєвого циклу замовлення.
/// </summary>
public class Reservation
{
    /// <summary>
    /// Унікальний ідентифікатор бронювання.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Столик, який був заброньований для клієнта.
    /// </summary>
    public Table? ReservedTable { get; set; }

    /// <summary>
    /// Клієнт, що взяв бронювання.
    /// </summary>
    public Customer? Client { get; set; }

    /// <summary>
    /// Час початку візиту або бронювання.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Тривалість бронювання у форматі <see cref="TimeSpan"/>.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Показує, чи бронювання ще активне та не скасоване.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Скасовує поточне бронювання та звільняє пов'язаний стіл.
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