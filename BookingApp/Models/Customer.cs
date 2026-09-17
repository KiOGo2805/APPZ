namespace BookingApp.Models;

/// <summary>
/// Клас предметної області, що містить дані клієнта.
/// Information Expert для даних користувача.
/// </summary>
public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public void UpdateContactInfo(string newPhone)
    {
        Console.WriteLine($"[Customer.UpdateContactInfo] Викликано для клієнта '{FullName}'. Новий телефон: {newPhone}");
        PhoneNumber = newPhone;
    }
}