namespace BookingApp.Services;

using BookingApp.Interfaces;
using BookingApp.Models;

/// <summary>
/// Реалізує надсилання сповіщень клієнтам.
/// </summary>
/// <remarks>
/// Клас є службою для відокремлення логіки повідомлень від контролера.
/// </remarks>
public class NotificationService : INotificationService
{
    /// <summary>
    /// Надсилає підтвердження бронювання клієнту.
    /// </summary>
    /// <param name="customer">Клієнт, якому надсилається підтвердження.</param>
    /// <param name="reservation">Бронювання для підтвердження.</param>
    public void SendConfirmation(Customer customer, Reservation reservation)
    {
        Console.WriteLine($"[NotificationService.SendConfirmation] SMS: Клієнт {customer.FullName} ({customer.PhoneNumber}), ваш столик #{reservation.ReservedTable?.Id} заброньовано на {reservation.StartTime}");
    }
}