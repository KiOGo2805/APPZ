using System;

namespace BookingApp.Factories
{
    /// <summary>
    /// Абстрактний продукт А: інтерфейс для конфігурації та сервірування столика.
    /// </summary>
    public interface ITableSetup
    {
        /// <summary>
        /// Виконує підготовку столика до приходу гостей.
        /// </summary>
        /// <param name="tableNumber">Номер столика в системі.</param>
        void Setup(int tableNumber);
    }

    /// <summary>
    /// Абстрактний продукт B: інтерфейс для надання відповідного меню гостям.
    /// </summary>
    public interface IMenuPlan
    {
        /// <summary>
        /// Презентує та надає меню гостям закладу.
        /// </summary>
        /// <param name="guestsCount">Кількість гостей за столиком.</param>
        void PresentMenu(int guestsCount);
    }

    /// <summary>
    /// Конкретний продукт А1: стандартне сервірування столика у загальній залі.
    /// </summary>
    public class StandardTableSetup : ITableSetup
    {
        /// <summary>
        /// Готує звичайний столик зі стандартним набором приборів.
        /// </summary>
        /// <param name="tableNumber">Номер столика.</param>
        public void Setup(int tableNumber)
        {
            Console.WriteLine($"[StandardTableSetup.Setup] Підготовка стандартного столика №{tableNumber}. Використано базове сервірування.");
        }
    }

    /// <summary>
    /// Конкретний продукт B1: основне меню закладу для стандартної зали.
    /// </summary>
    public class StandardMenuPlan : IMenuPlan
    {
        /// <summary>
        /// Надає стандартне меню для вказаної кількості гостей.
        /// </summary>
        /// <param name="guestsCount">Кількість гостей.</param>
        public void PresentMenu(int guestsCount)
        {
            Console.WriteLine($"[StandardMenuPlan.PresentMenu] Надання стандартного меню для {guestsCount} гостей.");
        }
    }

    /// <summary>
    /// Конкретний продукт А2: преміальне оформлення та сервірування VIP-столика.
    /// </summary>
    public class VipTableSetup : ITableSetup
    {
        /// <summary>
        /// Готує VIP-столик із додатковим декором та преміум-сервіровкою.
        /// </summary>
        /// <param name="tableNumber">Номер столика.</param>
        public void Setup(int tableNumber)
        {
            Console.WriteLine($"[VipTableSetup.Setup] Підготовка VIP-столика №{tableNumber}. Встановлено авторський декор, квіти та резервний супровід.");
        }
    }

    /// <summary>
    /// Конкретний продукт B2: ексклюзивне дегустаційне меню від шеф-кухаря.
    /// </summary>
    public class VipMenuPlan : IMenuPlan
    {
        /// <summary>
        /// Надає VIP-меню для гостей.
        /// </summary>
        /// <param name="guestsCount">Кількість гостей.</param>
        public void PresentMenu(int guestsCount)
        {
            Console.WriteLine($"[VipMenuPlan.PresentMenu] Надання персонального авторського меню від шеф-кухаря для {guestsCount} гостей.");
        }
    }

    /// <summary>
    /// Абстрактна фабрика, що декларує методи для створення сімейства продуктів ресторанної зони.
    /// </summary>
    public interface IRestaurantZoneFactory
    {
        /// <summary>
        /// Створює сервіс підготовки столика для конкретної зони.
        /// </summary>
        /// <returns>Об'єкт, що реалізує <see cref="ITableSetup"/>.</returns>
        ITableSetup CreateTableSetup();

        /// <summary>
        /// Створює план надання меню для конкретної зони.
        /// </summary>
        /// <returns>Об'єкт, що реалізує <see cref="IMenuPlan"/>.</returns>
        IMenuPlan CreateMenuPlan();
    }

    /// <summary>
    /// Конкретна фабрика для створення сімейства об'єктів стандартної зали ресторану.
    /// </summary>
    public class StandardZoneFactory : IRestaurantZoneFactory
    {
        /// <summary>
        /// Створює стандартне сервірування столика.
        /// </summary>
        /// <returns>Новий екземпляр <see cref="StandardTableSetup"/>.</returns>
        public ITableSetup CreateTableSetup() => new StandardTableSetup();

        /// <summary>
        /// Створює стандартне меню.
        /// </summary>
        /// <returns>Новий екземпляр <see cref="StandardMenuPlan"/>.</returns>
        public IMenuPlan CreateMenuPlan() => new StandardMenuPlan();
    }

    /// <summary>
    /// Конкретна фабрика для створення сімейства об'єктів VIP-зони ресторану.
    /// </summary>
    public class VipZoneFactory : IRestaurantZoneFactory
    {
        /// <summary>
        /// Створює преміальне сервірування столика.
        /// </summary>
        /// <returns>Новий екземпляр <see cref="VipTableSetup"/>.</returns>
        public ITableSetup CreateTableSetup() => new VipTableSetup();

        /// <summary>
        /// Створює ексклюзивне VIP-меню.
        /// </summary>
        /// <returns>Новий екземпляр <see cref="VipMenuPlan"/>.</returns>
        public IMenuPlan CreateMenuPlan() => new VipMenuPlan();
    }
}