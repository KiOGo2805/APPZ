namespace BookingApp.Models;

/// <summary>
/// Представляє клієнта ресторану.
/// </summary>
/// <remarks>
/// Клас зберігає контактні дані користувача та є інформаційним експертом для даних клієнта.
/// </remarks>
public class Customer
{
    /// <summary>
    /// Отримує або задає унікальний ідентифікатор клієнта.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Отримує або задає повне ім'я клієнта.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або задає контактний номер телефону клієнта.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Оновлює номер телефону клієнта.
    /// </summary>
    /// <param name="newPhone">Новий номер телефону.</param>
    public void UpdateContactInfo(string newPhone)
    {
        Console.WriteLine($"[Customer.UpdateContactInfo] Викликано для клієнта '{FullName}'. Новий телефон: {newPhone}");
        PhoneNumber = newPhone;
    }
}