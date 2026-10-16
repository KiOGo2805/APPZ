using System;

namespace BookingApp.Decorators;

/// <summary>
/// Компонент (Component) - загальний інтерфейс для бронювання.
/// </summary>
public interface IReservationComponent
{
    /// <summary>
    /// Повертає опис поточного бронювання з усіма доданими послугами.
    /// </summary>
    string GetDescription();

    /// <summary>
    /// Повертає загальну вартість бронювання.
    /// </summary>
    decimal GetCost();
}

/// <summary>
/// Конкретний компонент (Concrete Component) - базове бронювання столика без додаткових опцій.
/// </summary>
public class BaseReservation : IReservationComponent
{
    private readonly decimal _basePrice;

    /// <summary>
    /// Ініціалізує базове бронювання із заданою ціною (наприклад, депозит за стіл).
    /// </summary>
    /// <param name="basePrice">Базова вартість бронювання.</param>
    public BaseReservation(decimal basePrice = 200m)
    {
        _basePrice = basePrice;
    }

    public string GetDescription()
    {
        Console.WriteLine("[BaseReservation] Отримання базового опису бронювання.");
        return "Базове бронювання столика";
    }

    public decimal GetCost()
    {
        Console.WriteLine($"[BaseReservation] Отримання базової вартості: {_basePrice} грн.");
        return _basePrice;
    }
}

/// <summary>
/// Базовий Декоратор (Decorator) - містить посилання на вкладений компонент і делегує йому виклики.
/// </summary>
public abstract class ReservationDecorator : IReservationComponent
{
    protected readonly IReservationComponent _wrappee;

    /// <summary>
    /// Ініціалізує декоратор, обгортаючи існуючий компонент бронювання.
    /// </summary>
    /// <param name="wrappee">Компонент, який потрібно обгорнути (декорувати).</param>
    protected ReservationDecorator(IReservationComponent wrappee)
    {
        _wrappee = wrappee;
    }

    public virtual string GetDescription()
    {
        return _wrappee.GetDescription();
    }

    public virtual decimal GetCost()
    {
        return _wrappee.GetCost();
    }
}

/// <summary>
/// Конкретний декоратор 1 - додає послугу "Прикрашання столу".
/// </summary>
public class TableDecorationDecorator : ReservationDecorator
{
    public TableDecorationDecorator(IReservationComponent wrappee) : base(wrappee) { }

    public override string GetDescription()
    {
        Console.WriteLine("[TableDecorationDecorator] Додавання опису про прикрашання столу.");
        return $"{base.GetDescription()} + Святкове прикрашання";
    }

    public override decimal GetCost()
    {
        Console.WriteLine("[TableDecorationDecorator] Додавання вартості прикрашання (150 грн).");
        return base.GetCost() + 150m;
    }
}

/// <summary>
/// Конкретний декоратор 2 - додає послугу "Жива музика (замовлення пісні)".
/// </summary>
public class LiveMusicDecorator : ReservationDecorator
{
    public LiveMusicDecorator(IReservationComponent wrappee) : base(wrappee) { }

    public override string GetDescription()
    {
        Console.WriteLine("[LiveMusicDecorator] Додавання опису про живу музику.");
        return $"{base.GetDescription()} + Замовлення живої музики";
    }

    public override decimal GetCost()
    {
        Console.WriteLine("[LiveMusicDecorator] Додавання вартості живої музики (300 грн).");
        return base.GetCost() + 300m;
    }
}