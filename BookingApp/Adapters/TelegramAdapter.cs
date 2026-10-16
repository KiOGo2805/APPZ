using BookingApp.Interfaces;
using BookingApp.Models;

namespace BookingApp.Adapters;

/// <summary>
/// Сторонній сервіс (Adaptee), який має несумісний інтерфейс.
/// </summary>
public class ExternalTelegramApi
{
    /// <summary>
    /// Надсилає повідомлення користувачу Telegram.
    /// </summary>
    /// <param name="username">Ім'я користувача Telegram.</param>
    /// <param name="message">Текст повідомлення.</param>
    public void SendTelegramMessage(string username, string message)
    {
        Console.WriteLine($"[ExternalTelegramApi] Надсилання повідомлення в Telegram користувачу @{username}: {message}");
    }
}

/// <summary>
/// Адаптер, який робить ExternalTelegramApi сумісним з INotificationService.
/// </summary>
public class TelegramNotificationAdapter : INotificationService
{
    /// <summary>
    /// Сторонній API для надсилання повідомлень у Telegram.
    /// </summary>
    private readonly ExternalTelegramApi _telegramApi;

    /// <summary>
    /// Ініціалізує адаптер для стороннього Telegram API.
    /// </summary>
    /// <param name="telegramApi">Сторонній API для надсилання повідомлень.</param>
    public TelegramNotificationAdapter(ExternalTelegramApi telegramApi)
    {
        _telegramApi = telegramApi;
    }

    /// <summary>
    /// Формує та надсилає клієнту підтвердження бронювання через Telegram.
    /// </summary>
    /// <param name="customer">Клієнт, якому надсилається підтвердження.</param>
    /// <param name="reservation">Дані про бронювання.</param>
    public void SendConfirmation(Customer customer, Reservation reservation)
    {
        string username = customer.FullName.Replace(" ", "").ToLower();
        string tableInfo = reservation.ReservedTable != null ? reservation.ReservedTable.Id.ToString() : "Невідомо";

        string message = $"Ваш столик №{tableInfo} заброньовано на {reservation.StartTime}. Тривалість: {reservation.Duration.TotalHours} год.";

        Console.WriteLine("[TelegramNotificationAdapter] Адаптація даних клієнта та броні для Telegram API...");

        _telegramApi.SendTelegramMessage(username, message);
    }
}