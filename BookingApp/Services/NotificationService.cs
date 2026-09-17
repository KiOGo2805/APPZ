namespace BookingApp.Services;

using BookingApp.Interfaces;
using BookingApp.Models;

/// <summary>
/// Штучний об'єкт (Pure Fabrication) для відправки сповіщень.
/// Не є сутністю реального світу в контексті ресторану, але створений 
/// для забезпечення високої зв'язності (High Cohesion) контролера.
/// </summary>
public class NotificationService : INotificationService
{
    public void SendConfirmation(Customer customer, Reservation reservation)
    {
        Console.WriteLine($"[NotificationService.SendConfirmation] SMS: Клієнт {customer.FullName} ({customer.PhoneNumber}), ваш столик #{reservation.ReservedTable?.Id} заброньовано на {reservation.StartTime}");
    }
}