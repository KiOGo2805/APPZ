using BookingApp.Models;
using BookingApp.Services;

namespace BookingApp.ChainOfResponsibility;

/// <summary>
/// Контекст запиту на бронювання, який послідовно перевіряють обробники.
/// </summary>
public sealed class ReservationRequest
{
    /// <summary>
    /// Клієнт, для якого створюється бронювання.
    /// </summary>
    public Customer? Customer { get; }

    /// <summary>
    /// Кількість гостей, для яких потрібен столик.
    /// </summary>
    public int GuestCount { get; }

    /// <summary>
    /// Бажаний час початку бронювання.
    /// </summary>
    public DateTime StartTime { get; }

    /// <summary>
    /// Столик, знайдений перевіркою доступності.
    /// </summary>
    public Table? AvailableTable { get; internal set; }

    /// <summary>
    /// Створює контекст запиту з його основними параметрами.
    /// </summary>
    /// <param name="customer">Клієнт запиту.</param>
    /// <param name="guestCount">Кількість гостей.</param>
    /// <param name="startTime">Бажаний час початку бронювання.</param>
    public ReservationRequest(Customer? customer, int guestCount, DateTime startTime)
    {
        Customer = customer;
        GuestCount = guestCount;
        StartTime = startTime;
    }
}

/// <summary>
/// Результат проходження запиту через ланцюжок перевірок.
/// </summary>
public sealed class ReservationValidationResult
{
    /// <summary>
    /// Показує, чи пройшов запит усі перевірки.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Пояснює причину відмови або успішного проходження перевірок.
    /// </summary>
    public string Message { get; }

    private ReservationValidationResult(bool isValid, string message)
    {
        IsValid = isValid;
        Message = message;
    }

    /// <summary>
    /// Створює результат успішної перевірки.
    /// </summary>
    public static ReservationValidationResult Success() =>
        new(true, "Запит пройшов усі перевірки.");

    /// <summary>
    /// Створює результат із причиною відхилення запиту.
    /// </summary>
    /// <param name="message">Причина відхилення.</param>
    public static ReservationValidationResult Failure(string message) =>
        new(false, message);
}

/// <summary>
/// Контракт обробника у ланцюжку перевірок бронювання.
/// </summary>
public interface IReservationValidationHandler
{
    /// <summary>
    /// Встановлює наступного обробника та повертає його для побудови ланцюжка.
    /// </summary>
    /// <param name="nextHandler">Наступний обробник перевірки.</param>
    /// <returns>Встановлений наступний обробник.</returns>
    IReservationValidationHandler SetNext(IReservationValidationHandler nextHandler);

    /// <summary>
    /// Перевіряє запит і передає його далі, якщо поточну перевірку пройдено.
    /// </summary>
    /// <param name="request">Контекст запиту.</param>
    /// <returns>Результат проходження ланцюжка.</returns>
    ReservationValidationResult Handle(ReservationRequest request);
}

/// <summary>
/// Базовий обробник, який реалізує передавання запиту наступній ланці.
/// </summary>
public abstract class ReservationValidationHandler : IReservationValidationHandler
{
    private IReservationValidationHandler? _nextHandler;

    /// <inheritdoc />
    public IReservationValidationHandler SetNext(IReservationValidationHandler nextHandler)
    {
        ArgumentNullException.ThrowIfNull(nextHandler);
        _nextHandler = nextHandler;
        return nextHandler;
    }

    /// <inheritdoc />
    public ReservationValidationResult Handle(ReservationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ReservationValidationResult result = ValidateRequest(request);
        if (!result.IsValid)
        {
            Console.WriteLine($"[{GetType().Name}] Запит відхилено: {result.Message}");
            return result;
        }

        Console.WriteLine($"[{GetType().Name}] Перевірку пройдено.");
        return _nextHandler?.Handle(request) ?? ReservationValidationResult.Success();
    }

    /// <summary>
    /// Виконує специфічну для конкретного обробника перевірку.
    /// </summary>
    /// <param name="request">Контекст запиту.</param>
    /// <returns>Успішний результат або причина відхилення.</returns>
    protected abstract ReservationValidationResult ValidateRequest(ReservationRequest request);
}

/// <summary>
/// Перевіряє, чи вказано клієнта з непорожнім ім'ям.
/// </summary>
public sealed class CustomerValidationHandler : ReservationValidationHandler
{
    /// <inheritdoc />
    protected override ReservationValidationResult ValidateRequest(ReservationRequest request)
    {
        return request.Customer is null || string.IsNullOrWhiteSpace(request.Customer.FullName)
            ? ReservationValidationResult.Failure("Необхідно вказати клієнта з ім'ям.")
            : ReservationValidationResult.Success();
    }
}

/// <summary>
/// Перевіряє допустимість кількості гостей.
/// </summary>
public sealed class GuestCountValidationHandler : ReservationValidationHandler
{
    /// <inheritdoc />
    protected override ReservationValidationResult ValidateRequest(ReservationRequest request)
    {
        return request.GuestCount < 1
            ? ReservationValidationResult.Failure("Кількість гостей має бути більшою за нуль.")
            : ReservationValidationResult.Success();
    }
}

/// <summary>
/// Перевіряє, що бронювання заплановано на майбутній час.
/// </summary>
public sealed class ReservationTimeValidationHandler : ReservationValidationHandler
{
    /// <inheritdoc />
    protected override ReservationValidationResult ValidateRequest(ReservationRequest request)
    {
        return request.StartTime <= DateTime.Now
            ? ReservationValidationResult.Failure("Час бронювання має бути в майбутньому.")
            : ReservationValidationResult.Success();
    }
}

/// <summary>
/// Знаходить вільний столик відповідної місткості або відхиляє запит.
/// </summary>
public sealed class TableAvailabilityValidationHandler : ReservationValidationHandler
{
    private readonly Restaurant _restaurant;

    /// <summary>
    /// Створює перевірку доступності для заданого ресторану.
    /// </summary>
    /// <param name="restaurant">Ресторан, у якому шукають столик.</param>
    public TableAvailabilityValidationHandler(Restaurant restaurant)
    {
        ArgumentNullException.ThrowIfNull(restaurant);
        _restaurant = restaurant;
    }

    /// <inheritdoc />
    protected override ReservationValidationResult ValidateRequest(ReservationRequest request)
    {
        request.AvailableTable = _restaurant.GetAvailableTable(request.GuestCount, request.StartTime);
        return request.AvailableTable is null
            ? ReservationValidationResult.Failure("Немає вільного столика потрібної місткості.")
            : ReservationValidationResult.Success();
    }
}
