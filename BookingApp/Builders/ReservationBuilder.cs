using System;

namespace BookingApp.Builders
{
    /// <summary>
    /// Представляє складне банкетне бронювання, яке містить основні параметри замовлення.
    /// </summary>
    public class BanquetReservation
    {
        /// <summary>
        /// Отримує або задає ім'я клієнта.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Отримує або задає загальну кількість гостей.
        /// </summary>
        public int GuestsCount { get; set; }

        /// <summary>
        /// Отримує або задає перелік доданих послуг.
        /// </summary>
        public string Services { get; set; } = string.Empty;

        /// <summary>
        /// Виводить інформацію про сформоване банкетне бронювання в консоль.
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"[BanquetReservation] Клієнт: {CustomerName}, Гостей: {GuestsCount}. Замовлені послуги: {(string.IsNullOrEmpty(Services) ? "Немає" : Services)}");
        }
    }

    /// <summary>
    /// Визначає контракт будівельника для покрокового створення бронювання.
    /// </summary>
    public interface IReservationBuilder
    {
        /// <summary>
        /// Встановлює ім'я клієнта для поточного бронювання.
        /// </summary>
        /// <param name="name">Ім'я клієнта.</param>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        IReservationBuilder SetCustomer(string name);

        /// <summary>
        /// Встановлює кількість гостей для поточного бронювання.
        /// </summary>
        /// <param name="count">Кількість гостей.</param>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        IReservationBuilder SetGuests(int count);

        /// <summary>
        /// Додає послугу кейтерингу до поточного бронювання.
        /// </summary>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        IReservationBuilder AddCatering();

        /// <summary>
        /// Додає музику до поточного бронювання.
        /// </summary>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        IReservationBuilder AddMusicBand();

        /// <summary>
        /// Повертає готове банкетне бронювання і скидає стан будівельника.
        /// </summary>
        /// <returns>Готовий об'єкт банкетного бронювання.</returns>
        BanquetReservation Build();
    }

    /// <summary>
    /// Конкретний будівельник для VIP-банкетів із ланцюжком викликів у стилі Fluent API.
    /// </summary>
    public class VipBanquetBuilder : IReservationBuilder
    {
        private BanquetReservation _reservation = new BanquetReservation();

        /// <summary>
        /// Встановлює ім'я клієнта для поточного бронювання.
        /// </summary>
        /// <param name="name">Ім'я клієнта.</param>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        public IReservationBuilder SetCustomer(string name)
        {
            _reservation.CustomerName = name;
            Console.WriteLine($"[Builder] Встановлено клієнта: {name}");
            return this;
        }

        /// <summary>
        /// Встановлює кількість гостей для поточного бронювання.
        /// </summary>
        /// <param name="count">Кількість гостей.</param>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        public IReservationBuilder SetGuests(int count)
        {
            _reservation.GuestsCount = count;
            Console.WriteLine($"[Builder] Встановлено кількість гостей: {count}");
            return this;
        }

        /// <summary>
        /// Додає послугу кейтерингу до поточного бронювання.
        /// </summary>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        public IReservationBuilder AddCatering()
        {
            _reservation.Services += "Преміум-кейтеринг; ";
            Console.WriteLine("[Builder] Додано послугу: Преміум-кейтеринг");
            return this;
        }

        /// <summary>
        /// Додає музику до поточного бронювання.
        /// </summary>
        /// <returns>Поточний будівельник для ланцюжка викликів.</returns>
        public IReservationBuilder AddMusicBand()
        {
            _reservation.Services += "Жива музика; ";
            Console.WriteLine("[Builder] Додано послугу: Жива музика");
            return this;
        }

        /// <summary>
        /// Повертає готове банкетне бронювання і скидає стан будівельника.
        /// </summary>
        /// <returns>Готовий об'єкт банкетного бронювання.</returns>
        public BanquetReservation Build()
        {
            var result = _reservation;
            _reservation = new BanquetReservation();
            return result;
        }
    }
}