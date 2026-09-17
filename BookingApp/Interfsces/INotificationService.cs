namespace BookingApp.Interfaces;

using BookingApp.Models;

/// <summary>
/// Абстракція для служби сповіщень.
/// Сприяє низькій зв'язності (Low Coupling) та інверсії залежностей (DIP).
/// </summary>
public interface INotificationService
{
    void SendConfirmation(Customer customer, Reservation reservation);
}