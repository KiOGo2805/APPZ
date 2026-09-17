namespace BookingApp.Controllers;

using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Interfaces;

/// <summary>
/// Обробник зовнішніх запитів. Реалізує патерн Controller.
/// Також виступає Творцем (Creator) для об'єктів Reservation.
/// </summary>
public class ReservationController
{
    private readonly Restaurant _restaurant;
    private readonly INotificationService _notificationService;
    private int _reservationIdCounter = 1;


    public ReservationController(Restaurant restaurant, INotificationService notificationService)
    {
        _restaurant = restaurant;
        _notificationService = notificationService;
    }

    public void MakeReservation(Customer customer, int requiredCapacity, DateTime time)
    {
        Console.WriteLine($"\n--- ЗАПИТ НА БРОНЮВАННЯ ---");
        Console.WriteLine($"[ReservationController.MakeReservation] Від: {customer.FullName}, Кількість осіб: {requiredCapacity}, Час: {time}");

        Table? availableTable = _restaurant.GetAvailableTable(requiredCapacity, time);

        if (availableTable != null)
        {
            Reservation newReservation = new Reservation
            {
                Id = _reservationIdCounter++,
                Client = customer,
                ReservedTable = availableTable,
                StartTime = time,
                Duration = TimeSpan.FromHours(2) // Заглушка: стандартна тривалість 2 години
            };

            availableTable.MarkAsReserved();
            _notificationService.SendConfirmation(customer, newReservation);

            Console.WriteLine("[ReservationController.MakeReservation] УСПІХ: Бронювання створено!");
        }
        else
        {
            Console.WriteLine("[ReservationController.MakeReservation] ВІДМОВА: На жаль, вільних столиків немає.");
        }
    }
}