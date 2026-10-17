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

    /// <summary>
    /// Повертає базовий опис бронювання без жодних додаткових послуг.
    /// </summary>
    /// <returns>Рядок з описом базового бронювання.</returns>
    public string GetDescription()
    {
        Console.WriteLine("[BaseReservation] Отримання базового опису бронювання.");
        return "Базове бронювання столика";
    }

    /// <summary>
    /// Повертає базову вартість бронювання столика.
    /// </summary>
    /// <returns>Базова вартість у вигляді десяткового числа.</returns>
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
    /// <summary>
    /// Вкладений компонент, до якого додаються нові обов'язки.
    /// </summary>
    protected readonly IReservationComponent _wrappee;

    /// <summary>
    /// Ініціалізує базовий декоратор, зберігаючи посилання на обгорнутий об'єкт.
    /// </summary>
    /// <param name="wrappee">Компонент, який потрібно обгорнути (декорувати).</param>
    protected ReservationDecorator(IReservationComponent wrappee)
    {
        _wrappee = wrappee;
    }

    /// <summary>
    /// Делегує отримання опису вкладеному компоненту.
    /// </summary>
    /// <returns>Опис від вкладеного компонента.</returns>
    public virtual string GetDescription()
    {
        return _wrappee.GetDescription();
    }

    /// <summary>
    /// Делегує отримання вартості вкладеному компоненту.
    /// </summary>
    /// <returns>Вартість від вкладеного компонента.</returns>
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
    /// <summary>
    /// Ініціалізує декоратор для додавання послуги прикрашання столу.
    /// </summary>
    /// <param name="wrappee">Компонент, до якого додається послуга.</param>
    public TableDecorationDecorator(IReservationComponent wrappee) : base(wrappee) { }

    /// <summary>
    /// Додає інформацію про святкове прикрашання до опису бронювання.
    /// </summary>
    /// <returns>Оновлений опис бронювання.</returns>
    public override string GetDescription()
    {
        Console.WriteLine("[TableDecorationDecorator] Додавання опису про прикрашання столу.");
        return $"{base.GetDescription()} + Святкове прикрашання";
    }

    /// <summary>
    /// Додає вартість прикрашання столу до загального чека.
    /// </summary>
    /// <returns>Оновлена загальна вартість.</returns>
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
    /// <summary>
    /// Ініціалізує декоратор для додавання послуги живої музики.
    /// </summary>
    /// <param name="wrappee">Компонент, до якого додається послуга.</param>
    public LiveMusicDecorator(IReservationComponent wrappee) : base(wrappee) { }

    /// <summary>
    /// Додає інформацію про замовлення живої музики до опису бронювання.
    /// </summary>
    /// <returns>Оновлений опис бронювання.</returns>
    public override string GetDescription()
    {
        Console.WriteLine("[LiveMusicDecorator] Додавання опису про живу музику.");
        return $"{base.GetDescription()} + Замовлення живої музики";
    }

    /// <summary>
    /// Додає вартість замовлення живої музики до загального чека.
    /// </summary>
    /// <returns>Оновлена загальна вартість.</returns>
    public override decimal GetCost()
    {
        Console.WriteLine("[LiveMusicDecorator] Додавання вартості живої музики (300 грн).");
        return base.GetCost() + 300m;
    }
}