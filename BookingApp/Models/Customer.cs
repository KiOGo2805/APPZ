namespace BookingApp.Models;

/// <summary>
/// Представляє клієнта ресторану.
/// </summary>
/// <remarks>
/// Клас зберігає особисті та контактні дані клієнта: ім'я, номер телефону й адресу електронної пошти.
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
    /// Отримує або задає адресу електронної пошти клієнта.
    /// </summary>
    public string Email { get; set; } = string.Empty;

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