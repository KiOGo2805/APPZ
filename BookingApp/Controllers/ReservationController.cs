namespace BookingApp.Controllers;

using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Interfaces;

/// <summary>
/// Керує процесом бронювання столиків.
/// </summary>
/// <remarks>
/// Клас реалізує роль контролера та координує перевірку доступності столика й створення бронювання.
/// </remarks>
public class ReservationController
{
    /// <summary>
    /// Ресторан, з яким взаємодіє контролер.
    /// </summary>
    private readonly Restaurant _restaurant;

    /// <summary>
    /// Служба сповіщень для підтвердження бронювання.
    /// </summary>
    private readonly INotificationService _notificationService;

    /// <summary>
    /// Лічильник для генерації ідентифікаторів бронювань.
    /// </summary>
    private int _reservationIdCounter = 1;

    /// <summary>
    /// Ініціалізує новий екземпляр контролера.
    /// </summary>
    /// <param name="restaurant">Ресторан, де виконується бронювання.</param>
    /// <param name="notificationService">Служба для відправки сповіщень.</param>
    public ReservationController(Restaurant restaurant, INotificationService notificationService)
    {
        _restaurant = restaurant;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Створює бронювання для клієнта, якщо є вільний стіл.
    /// </summary>
    /// <param name="customer">Клієнт, який хоче забронювати стіл.</param>
    /// <param name="requiredCapacity">Необхідна місткість столика.</param>
    /// <param name="time">Час бронювання.</param>
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