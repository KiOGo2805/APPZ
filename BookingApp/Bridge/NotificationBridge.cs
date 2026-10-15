using BookingApp.Models;

namespace BookingApp.Bridge;

// --- ІЄРАРХІЯ РЕАЛІЗАЦІЇ (Канали зв'язку) ---

/// <summary>
/// Інтерфейс реалізатора (Implementation).
/// </summary>
public interface IMessageSender
{
    /// <summary>
    /// Надсилає текст повідомлення за вказаною контактною інформацією.
    /// </summary>
    /// <param name="contactInfo">Контактні дані одержувача.</param>
    /// <param name="text">Текст повідомлення.</param>
    void SendMessage(string contactInfo, string text);
}

/// <summary>
/// Реалізатор для надсилання повідомлень електронною поштою.
/// </summary>
public class EmailSender : IMessageSender
{
    /// <summary>
    /// Надсилає повідомлення на адресу електронної пошти.
    /// </summary>
    /// <param name="contactInfo">Адреса електронної пошти одержувача.</param>
    /// <param name="text">Текст повідомлення.</param>
    public void SendMessage(string contactInfo, string text)
    {
        Console.WriteLine($"[EmailSender] Відправка листа на {contactInfo}: {text}");
    }
}

/// <summary>
/// Реалізатор для надсилання SMS-повідомлень.
/// </summary>
public class SmsSender : IMessageSender
{
    /// <summary>
    /// Надсилає SMS на вказаний номер телефону.
    /// </summary>
    /// <param name="contactInfo">Номер телефону одержувача.</param>
    /// <param name="text">Текст повідомлення.</param>
    public void SendMessage(string contactInfo, string text)
    {
        Console.WriteLine($"[SmsSender] Відправка SMS на номер {contactInfo}: {text}");
    }
}

// --- ІЄРАРХІЯ АБСТРАКЦІЇ (Типи повідомлень) ---

/// <summary>
/// Базова абстракція (Abstraction), яка містить посилання на реалізатора.
/// </summary>
public abstract class NotificationMessage
{
    /// <summary>
    /// Реалізатор каналу надсилання повідомлення.
    /// </summary>
    protected readonly IMessageSender _sender;

    /// <summary>
    /// Ініціалізує повідомлення з реалізатором каналу надсилання.
    /// </summary>
    /// <param name="sender">Реалізатор, який надсилатиме повідомлення.</param>
    protected NotificationMessage(IMessageSender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Обробляє сповіщення для клієнта щодо його бронювання.
    /// </summary>
    /// <param name="customer">Клієнт, якому надсилається сповіщення.</param>
    /// <param name="reservation">Дані про бронювання.</param>
    public abstract void ProcessNotification(Customer customer, Reservation reservation);
}

/// <summary>
/// Розширена абстракція 1: Повідомлення про підтвердження.
/// </summary>
public class ConfirmationMessage : NotificationMessage
{
    /// <summary>
    /// Ініціалізує повідомлення про підтвердження бронювання.
    /// </summary>
    /// <param name="sender">Реалізатор каналу надсилання.</param>
    public ConfirmationMessage(IMessageSender sender) : base(sender) { }

    /// <summary>
    /// Формує та надсилає клієнту підтвердження бронювання.
    /// </summary>
    /// <param name="customer">Клієнт, якому надсилається підтвердження.</param>
    /// <param name="reservation">Дані про бронювання.</param>
    public override void ProcessNotification(Customer customer, Reservation reservation)
    {
        string text = $"Підтвердження: Столик {reservation.ReservedTable?.Id} на {reservation.StartTime:HH:mm}.";
        Console.WriteLine("[ConfirmationMessage] Формування тексту підтвердження...");
        _sender.SendMessage(customer.PhoneNumber, text);
    }
}

/// <summary>
/// Розширена абстракція 2: Повідомлення про скасування.
/// </summary>
public class CancellationMessage : NotificationMessage
{
    /// <summary>
    /// Ініціалізує повідомлення про скасування бронювання.
    /// </summary>
    /// <param name="sender">Реалізатор каналу надсилання.</param>
    public CancellationMessage(IMessageSender sender) : base(sender) { }

    /// <summary>
    /// Формує та надсилає клієнту повідомлення про скасування бронювання.
    /// </summary>
    /// <param name="customer">Клієнт, якому надсилається повідомлення.</param>
    /// <param name="reservation">Дані про скасоване бронювання.</param>
    public override void ProcessNotification(Customer customer, Reservation reservation)
    {
        string text = $"Скасування: Бронювання столика {reservation.ReservedTable?.Id} було скасовано.";
        Console.WriteLine("[CancellationMessage] Формування тексту скасування...");
        _sender.SendMessage(customer.PhoneNumber, text);
    }
}