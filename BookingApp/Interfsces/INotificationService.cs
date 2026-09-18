namespace BookingApp.Interfaces;

using BookingApp.Models;

/// <summary>
/// Визначає контракт служби сповіщень.
/// </summary>
/// <remarks>
/// Інтерфейс дозволяє відокремити контролер від конкретної реалізації повідомлень.
/// </remarks>
public interface INotificationService
{
    /// <summary>
    /// Надсилає підтвердження бронювання клієнту.
    /// </summary>
    /// <param name="customer">Клієнт, який отримує повідомлення.</param>
    /// <param name="reservation">Бронювання, що підтверджується.</param>
    void SendConfirmation(Customer customer, Reservation reservation);
}