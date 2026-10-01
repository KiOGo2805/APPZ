using System;

namespace BookingApp.Builders
{
    /// <summary>
    /// Продукт: Складне банкетне бронювання, що містить багато параметрів.
    /// </summary>
    public class BanquetReservation
    {
        public string CustomerName { get; set; } = string.Empty;
        public int GuestsCount { get; set; }
        public string Services { get; set; } = string.Empty;

        /// <summary>
        /// Виводить інформацію про сформоване банкетне бронювання.
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"[BanquetReservation] Клієнт: {CustomerName}, Гостей: {GuestsCount}. Замовлені послуги: {(string.IsNullOrEmpty(Services) ? "Немає" : Services)}");
        }
    }

    /// <summary>
    /// Інтерфейс будівельника для покрокового створення бронювання.
    /// </summary>
    public interface IReservationBuilder
    {
        IReservationBuilder SetCustomer(string name);
        IReservationBuilder SetGuests(int count);
        IReservationBuilder AddCatering();
        IReservationBuilder AddMusicBand();
        BanquetReservation Build();
    }

    /// <summary>
    /// Конкретний будівельник для VIP-банкетів із зручним ланцюжком викликів (Fluent API).
    /// </summary>
    public class VipBanquetBuilder : IReservationBuilder
    {
        private BanquetReservation _reservation = new BanquetReservation();

        public IReservationBuilder SetCustomer(string name)
        {
            _reservation.CustomerName = name;
            Console.WriteLine($"[Builder] Встановлено клієнта: {name}");
            return this;
        }

        public IReservationBuilder SetGuests(int count)
        {
            _reservation.GuestsCount = count;
            Console.WriteLine($"[Builder] Встановлено кількість гостей: {count}");
            return this;
        }

        public IReservationBuilder AddCatering()
        {
            _reservation.Services += "Преміум-кейтеринг; ";
            Console.WriteLine("[Builder] Додано послугу: Преміум-кейтеринг");
            return this;
        }

        public IReservationBuilder AddMusicBand()
        {
            _reservation.Services += "Жива музика; ";
            Console.WriteLine("[Builder] Додано послугу: Жива музика");
            return this;
        }

        /// <summary>
        /// Повертає готовий об'єкт і скидає стан будівельника.
        /// </summary>
        public BanquetReservation Build()
        {
            var result = _reservation;
            _reservation = new BanquetReservation(); // Скидання для наступного використання
            return result;
        }
    }
}