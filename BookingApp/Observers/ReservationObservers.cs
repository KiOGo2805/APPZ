using BookingApp.Models;

namespace BookingApp.Observers;

/// <summary>
/// Тип події, про яку система бронювання сповіщає підписників.
/// </summary>
public enum ReservationEventType
{
    /// <summary>
    /// Бронювання успішно створено.
    /// </summary>
    Created,

    /// <summary>
    /// Бронювання скасовано.
    /// </summary>
    Cancelled,

    /// <summary>
    /// Стан бронювання відновлено зі знімка.
    /// </summary>
    Restored
}

/// <summary>
/// Дані про подію, передані спостерігачам.
/// </summary>
public sealed class ReservationEvent
{
    /// <summary>
    /// Тип події бронювання.
    /// </summary>
    public ReservationEventType Type { get; }

    /// <summary>
    /// Бронювання, якого стосується подія.
    /// </summary>
    public Reservation Reservation { get; }

    /// <summary>
    /// Час виникнення події.
    /// </summary>
    public DateTime OccurredAt { get; }

    /// <summary>
    /// Створює повідомлення про подію бронювання.
    /// </summary>
    /// <param name="type">Тип події.</param>
    /// <param name="reservation">Пов'язане бронювання.</param>
    public ReservationEvent(ReservationEventType type, Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        Type = type;
        Reservation = reservation;
        OccurredAt = DateTime.Now;
    }
}

/// <summary>
/// Контракт об'єкта, який отримує сповіщення про події бронювання.
/// </summary>
public interface IReservationObserver
{
    /// <summary>
    /// Обробляє подію бронювання, опубліковану посередником сповіщень.
    /// </summary>
    /// <param name="reservationEvent">Дані події.</param>
    void OnReservationEvent(ReservationEvent reservationEvent);
}

/// <summary>
/// Суб'єкт Observer, що керує підписниками та розсилає їм події бронювання.
/// </summary>
public sealed class ReservationEventPublisher
{
    private readonly List<IReservationObserver> _observers = [];

    /// <summary>
    /// Кількість активних підписників.
    /// </summary>
    public int ObserverCount => _observers.Count;

    /// <summary>
    /// Додає спостерігача до списку підписників.
    /// </summary>
    /// <param name="observer">Спостерігач, якого потрібно підписати.</param>
    public void Subscribe(IReservationObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            Console.WriteLine($"[ReservationEventPublisher.Subscribe] Підписано {observer.GetType().Name}.");
        }
    }

    /// <summary>
    /// Видаляє спостерігача зі списку підписників.
    /// </summary>
    /// <param name="observer">Спостерігач, якого потрібно відписати.</param>
    /// <returns><see langword="true"/>, якщо спостерігача було відписано.</returns>
    public bool Unsubscribe(IReservationObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        bool removed = _observers.Remove(observer);
        Console.WriteLine($"[ReservationEventPublisher.Unsubscribe] {observer.GetType().Name}: {(removed ? "відписано" : "не був підписаний")}.");
        return removed;
    }

    /// <summary>
    /// Розсилає подію всім підписаним спостерігачам.
    /// </summary>
    /// <param name="reservationEvent">Подія для розсилання.</param>
    public void Publish(ReservationEvent reservationEvent)
    {
        ArgumentNullException.ThrowIfNull(reservationEvent);
        Console.WriteLine($"[ReservationEventPublisher.Publish] Сповіщення {_observers.Count} спостерігачів: {reservationEvent.Type}.");
        foreach (IReservationObserver observer in _observers.ToArray())
        {
            observer.OnReservationEvent(reservationEvent);
        }
    }
}

/// <summary>
/// Спостерігач, який виводить події бронювання до журналу консолі.
/// </summary>
public sealed class ConsoleReservationObserver : IReservationObserver
{
    /// <inheritdoc />
    public void OnReservationEvent(ReservationEvent reservationEvent)
    {
        Console.WriteLine(
            $"[ConsoleReservationObserver] Подія {reservationEvent.Type}: бронювання №{reservationEvent.Reservation.Id}, " +
            $"клієнт {reservationEvent.Reservation.Client?.FullName}, час {reservationEvent.OccurredAt:HH:mm:ss}.");
    }
}
