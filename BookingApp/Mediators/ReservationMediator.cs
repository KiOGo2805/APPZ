using BookingApp.Interfaces;
using BookingApp.Iterators;
using BookingApp.Models;
using BookingApp.Services;

namespace BookingApp.Mediators;

/// <summary>
/// Посередник бронювання, який координує пошук столика, збереження броні та сповіщення.
/// </summary>
public sealed class ReservationMediator
{
    private readonly Restaurant _restaurant;
    private readonly INotificationService _notificationService;
    private readonly ReservationCollection _reservations;

    /// <summary>
    /// Створює посередника для взаємодії учасників процесу бронювання.
    /// </summary>
    /// <param name="restaurant">Ресторан, у якому шукають столик.</param>
    /// <param name="notificationService">Служба сповіщень клієнтів.</param>
    /// <param name="reservations">Колекція, до якої додаються створені бронювання.</param>
    public ReservationMediator(
        Restaurant restaurant,
        INotificationService notificationService,
        ReservationCollection reservations)
    {
        ArgumentNullException.ThrowIfNull(restaurant);
        ArgumentNullException.ThrowIfNull(notificationService);
        ArgumentNullException.ThrowIfNull(reservations);

        _restaurant = restaurant;
        _notificationService = notificationService;
        _reservations = reservations;
    }

    /// <summary>
    /// Координує бронювання: знаходить столик, оновлює його стан, зберігає бронь і надсилає підтвердження.
    /// </summary>
    /// <param name="customer">Клієнт, для якого створюється бронювання.</param>
    /// <param name="guestCount">Кількість гостей.</param>
    /// <param name="startTime">Час початку бронювання.</param>
    /// <returns>Створене бронювання або <see langword="null"/>, якщо вільного столика немає.</returns>
    public Reservation? CreateReservation(Customer customer, int guestCount, DateTime startTime)
    {
        ArgumentNullException.ThrowIfNull(customer);
        if (guestCount < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(guestCount), "Кількість гостей має бути більшою за нуль.");
        }

        Console.WriteLine("[ReservationMediator] Координація запиту між рестораном, колекцією бронювань і службою сповіщень.");
        Table? table = _restaurant.GetAvailableTable(guestCount, startTime);
        if (table is null)
        {
            Console.WriteLine("[ReservationMediator] Відмовлено: вільного столика потрібної місткості немає.");
            return null;
        }

        Reservation reservation = new()
        {
            Id = _reservations.GetNextId(),
            Client = customer,
            ReservedTable = table,
            StartTime = startTime,
            Duration = TimeSpan.FromHours(2)
        };

        table.MarkAsReserved();
        _reservations.Add(reservation);
        _notificationService.SendConfirmation(customer, reservation);
        Console.WriteLine($"[ReservationMediator] Бронювання №{reservation.Id} створено та передано учасникам процесу.");
        return reservation;
    }
}
