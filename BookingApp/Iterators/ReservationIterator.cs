using BookingApp.Models;

namespace BookingApp.Iterators;

/// <summary>
/// Визначає операції для послідовного обходу бронювань без відкриття внутрішньої колекції.
/// </summary>
public interface IReservationIterator
{
    /// <summary>
    /// Показує, чи залишилися елементи для обходу.
    /// </summary>
    bool HasNext();

    /// <summary>
    /// Повертає наступне бронювання та пересуває позицію ітератора.
    /// </summary>
    /// <returns>Наступне бронювання.</returns>
    /// <exception cref="InvalidOperationException">Якщо всі бронювання вже переглянуто.</exception>
    Reservation Next();
}

/// <summary>
/// Колекція бронювань, яка надає ітератор для послідовного обходу.
/// </summary>
public sealed class ReservationCollection
{
    private readonly List<Reservation> _reservations = [];

    /// <summary>
    /// Кількість збережених бронювань.
    /// </summary>
    public int Count => _reservations.Count;

    /// <summary>
    /// Додає бронювання до колекції.
    /// </summary>
    /// <param name="reservation">Бронювання для збереження.</param>
    public void Add(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        _reservations.Add(reservation);
    }

    /// <summary>
    /// Повертає наступний вільний числовий ідентифікатор бронювання.
    /// </summary>
    /// <returns>Ідентифікатор, більший за всі наявні ідентифікатори.</returns>
    public int GetNextId() =>
        _reservations.Count == 0 ? 1 : _reservations.Max(reservation => reservation.Id) + 1;

    /// <summary>
    /// Створює ітератор для обходу бронювань у порядку додавання.
    /// </summary>
    /// <returns>Новий ітератор, що починає обхід із першого елемента.</returns>
    public IReservationIterator CreateIterator() => new ConcreteReservationIterator(_reservations);
}

/// <summary>
/// Конкретний ітератор, який послідовно обходить бронювання колекції.
/// </summary>
internal sealed class ConcreteReservationIterator : IReservationIterator
{
    private readonly IReadOnlyList<Reservation> _reservations;
    private int _position;

    /// <summary>
    /// Ініціалізує ітератор для заданого списку бронювань.
    /// </summary>
    /// <param name="reservations">Список, який потрібно обійти.</param>
    public ConcreteReservationIterator(IReadOnlyList<Reservation> reservations)
    {
        _reservations = reservations;
    }

    /// <inheritdoc />
    public bool HasNext() => _position < _reservations.Count;

    /// <inheritdoc />
    public Reservation Next()
    {
        if (!HasNext())
        {
            throw new InvalidOperationException("У колекції більше немає бронювань для перегляду.");
        }

        return _reservations[_position++];
    }
}
