using BookingApp.Controllers;
using BookingApp.Models;
using BookingApp.Proxies;

namespace BookingApp.Commands;

/// <summary>
/// Команда, що інкапсулює окрему дію системи бронювання.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Виконує закладену в команду дію.
    /// </summary>
    void Execute();
}

/// <summary>
/// Команда створення бронювання; контролер виступає її одержувачем.
/// </summary>
public sealed class MakeReservationCommand : ICommand
{
    private readonly ReservationController _controller;
    private readonly Customer _customer;
    private readonly int _guestCount;
    private readonly DateTime _startTime;

    /// <summary>
    /// Створює команду бронювання з усіма параметрами, потрібними одержувачу.
    /// </summary>
    /// <param name="controller">Контролер, який створює бронювання.</param>
    /// <param name="customer">Клієнт бронювання.</param>
    /// <param name="guestCount">Кількість гостей.</param>
    /// <param name="startTime">Час початку бронювання.</param>
    public MakeReservationCommand(
        ReservationController controller,
        Customer customer,
        int guestCount,
        DateTime startTime)
    {
        ArgumentNullException.ThrowIfNull(controller);
        ArgumentNullException.ThrowIfNull(customer);
        _controller = controller;
        _customer = customer;
        _guestCount = guestCount;
        _startTime = startTime;
    }

    /// <inheritdoc />
    public void Execute()
    {
        Console.WriteLine("[MakeReservationCommand.Execute] Передавання команди контролеру.");
        _controller.MakeReservation(_customer, _guestCount, _startTime);
    }
}

/// <summary>
/// Команда скасування бронювання через заданий менеджер бронювань.
/// </summary>
public sealed class CancelReservationCommand : ICommand
{
    private readonly IReservationManager _manager;
    private readonly int _reservationId;

    /// <summary>
    /// Створює команду скасування бронювання.
    /// </summary>
    /// <param name="manager">Одержувач команди, який обробляє скасування.</param>
    /// <param name="reservationId">Ідентифікатор бронювання.</param>
    public CancelReservationCommand(IReservationManager manager, int reservationId)
    {
        ArgumentNullException.ThrowIfNull(manager);
        _manager = manager;
        _reservationId = reservationId;
    }

    /// <inheritdoc />
    public void Execute()
    {
        Console.WriteLine("[CancelReservationCommand.Execute] Передавання команди менеджеру.");
        _manager.CancelReservation(_reservationId);
    }
}

/// <summary>
/// Ініціатор шаблону Command, який запускає передану команду.
/// </summary>
public sealed class ReservationCommandInvoker
{
    /// <summary>
    /// Виконує команду, не залежачи від конкретного типу її одержувача.
    /// </summary>
    /// <param name="command">Команда для виконання.</param>
    public void Execute(ICommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        Console.WriteLine($"[ReservationCommandInvoker] Виконується {command.GetType().Name}.");
        command.Execute();
    }
}
