namespace BookingApp.Interfaces;

using BookingApp.Models;

/// <summary>
/// Контракт служби сповіщення для системи бронювання.
/// Інтерфейс забезпечує слабку зв'язність між контролером і конкретною реалізацією
/// надсилання повідомлень, а також підтримує принцип інверсії залежностей.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Надсилає підтвердження бронювання клієнту.
    /// </summary>
    /// <param name="customer">Клієнт, який отримує повідомлення.</param>
    /// <param name="reservation">Бронювання, що підтверджується.</param>
    void SendConfirmation(Customer customer, Reservation reservation);
}