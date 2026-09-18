namespace BookingApp.Services;

using BookingApp.Interfaces;
using BookingApp.Models;

/// <summary>
/// Служба сповіщень, що реалізує інтерфейс <see cref="INotificationService"/>.
/// Цей клас є прикладом <c>Pure Fabrication</c>: він не описує фізичну сутність ресторану,
/// але потрібен для відокремлення логіки надсилання підтверджень від контролера та збереження
/// чистої відповідальності між компонентами системи.
/// </summary>
public class NotificationService : INotificationService
{
    /// <summary>
    /// Надсилає підтвердження бронювання клієнту.
    /// </summary>
    /// <param name="customer">Клієнт, якому надсилається підтвердження.</param>
    /// <param name="reservation">Бронювання, щодо якого формується повідомлення.</param>
    public void SendConfirmation(Customer customer, Reservation reservation)
    {
        Console.WriteLine($"[NotificationService.SendConfirmation] SMS: Клієнт {customer.FullName} ({customer.PhoneNumber}), ваш столик #{reservation.ReservedTable?.Id} заброньовано на {reservation.StartTime}");
    }
}