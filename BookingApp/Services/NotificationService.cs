using System;
using BookingApp.Interfaces;
using BookingApp.Models;

namespace BookingApp.Services.Notifications
{
    /// <summary>
    /// Конкретний продукт А: Сервіс для відправки Email-сповіщень.
    /// </summary>
    public class EmailNotification : INotificationService
    {
        /// <summary>
        /// Реалізація методу інтерфейсу для відправки підтвердження через Email.
        /// </summary>
        /// <param name="customer">Клієнт, якому відправляється сповіщення.</param>
        /// <param name="reservation">Дані про бронювання.</param>
        public void SendConfirmation(Customer customer, Reservation reservation)
        {

            // Якщо в Customer є властивість FirstName, можеш написати {customer.FirstName}
            Console.WriteLine($"[EmailNotification] Відправка Email підтвердження клієнту. Деталі бронювання оброблені.");
        }
    }

    /// <summary>
    /// Конкретний продукт B: Сервіс для відправки SMS-сповіщень.
    /// </summary>
    public class SmsNotification : INotificationService
    {
        /// <summary>
        /// Реалізація методу інтерфейсу для відправки підтвердження через SMS.
        /// </summary>
        /// <param name="customer">Клієнт, якому відправляється сповіщення.</param>
        /// <param name="reservation">Дані про бронювання.</param>
        public void SendConfirmation(Customer customer, Reservation reservation)
        {
            Console.WriteLine($"[SmsNotification] Відправка SMS підтвердження клієнту. Деталі бронювання оброблені.");
        }
    }

    /// <summary>
    /// Абстрактний клас Creator, який містить фабричний метод для створення сервісів сповіщень.
    /// </summary>
    public abstract class NotificationCreator
    {
        /// <summary>
        /// Фабричний метод, який підкласи повинні реалізувати для створення конкретного типу сповіщення.
        /// </summary>
        /// <returns>Екземпляр сервісу, що реалізує INotificationService.</returns>
        public abstract INotificationService CreateNotificationService();

        /// <summary>
        /// Базовий метод, який використовує фабричний метод для відправки повідомлення.
        /// </summary>
        /// <param name="customer">Клієнт.</param>
        /// <param name="reservation">Бронювання.</param>
        public void Notify(Customer customer, Reservation reservation)
        {
            var service = CreateNotificationService();
            service.SendConfirmation(customer, reservation);
        }
    }

    /// <summary>
    /// Конкретний творець для створення Email-сповіщень.
    /// </summary>
    public class EmailNotificationCreator : NotificationCreator
    {
        /// <summary>
        /// Перевизначений фабричний метод, що повертає Email-сервіс.
        /// </summary>
        /// <returns>Новий екземпляр EmailNotification.</returns>
        public override INotificationService CreateNotificationService() => new EmailNotification();
    }

    /// <summary>
    /// Конкретний творець для створення SMS-сповіщень.
    /// </summary>
    public class SmsNotificationCreator : NotificationCreator
    {
        /// <summary>
        /// Перевизначений фабричний метод, що повертає SMS-сервіс.
        /// </summary>
        /// <returns>Новий екземпляр SmsNotification.</returns>
        public override INotificationService CreateNotificationService() => new SmsNotification();
    }
}