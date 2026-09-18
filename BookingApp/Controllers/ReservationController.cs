namespace BookingApp.Controllers;

using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Interfaces;

/// <summary>
/// Контролер, що координує процес бронювання столиків.
/// Клас реалізує шаблон <c>Controller</c> та відповідає за прийняття запиту від користувача,
/// перевірку доступності столика та створення нового екземпляра <see cref="Reservation"/>.
/// </summary>
public class ReservationController
{
    /// <summary>
    /// Посилання на ресторан, з яким взаємодіє контролер.
    /// </summary>
    private readonly Restaurant _restaurant;

    /// <summary>
    /// Служба для надсилання сповіщень після успішного бронювання.
    /// </summary>
    private readonly INotificationService _notificationService;

    /// <summary>
    /// Лічильник для генерації унікальних ідентифікаторів бронювань.
    /// </summary>
    private int _reservationIdCounter = 1;

    /// <summary>
    /// Ініціалізує контролер для конкретного ресторану та служби сповіщень.
    /// </summary>
    /// <param name="restaurant">Ресторан, для якого здійснюється бронювання.</param>
    /// <param name="notificationService">Служба для надсилання підтверджень клієнту.</param>
    public ReservationController(Restaurant restaurant, INotificationService notificationService)
    {
        _restaurant = restaurant;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Створює нове бронювання для клієнта, якщо є доступний стіл відповідної місткості.
    /// </summary>
    /// <param name="customer">Клієнт, який хоче забронювати місце.</param>
    /// <param name="requiredCapacity">Необхідна кількість місць для столика.</param>
    /// <param name="time">Час, на який планується візит.</param>
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