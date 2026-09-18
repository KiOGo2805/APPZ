namespace BookingApp.Models;

/// <summary>
/// Модель клієнта ресторану.
/// Цей клас відповідає за зберігання контактної інформації користувача та реалізує роль
/// <c>Information Expert</c>, оскільки саме він володіє даними про особу, її ідентифікатор
/// та контактні дані.
/// </summary>
public class Customer
{
    /// <summary>
    /// Унікальний ідентифікатор клієнта в системі бронювання.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Повне ім'я клієнта, яке використовується в повідомленнях про бронювання.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Контактний телефон клієнта, необхідний для надсилання підтвердження бронювання.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Оновлює номер телефону клієнта та фіксує цю дію в консолі.
    /// </summary>
    /// <param name="newPhone">Новий номер телефону клієнта.</param>
    public void UpdateContactInfo(string newPhone)
    {
        Console.WriteLine($"[Customer.UpdateContactInfo] Викликано для клієнта '{FullName}'. Новий телефон: {newPhone}");
        PhoneNumber = newPhone;
    }
}