using System;

namespace BookingApp.Proxies;

/// <summary>
/// Суб'єкт (Subject) - спільний інтерфейс для реального об'єкта та заступника.
/// </summary>
public interface IReservationManager
{
    /// <summary>
    /// Скасовує бронювання за його ідентифікатором.
    /// </summary>
    /// <param name="reservationId">ID бронювання.</param>
    void CancelReservation(int reservationId);
}

/// <summary>
/// Реальний суб'єкт (Real Subject) - клас, який виконує реальну бізнес-логіку скасування.
/// </summary>
public class RealReservationManager : IReservationManager
{
    public void CancelReservation(int reservationId)
    {
        Console.WriteLine($"[RealReservationManager] УСПІХ: Бронювання №{reservationId} було остаточно скасовано в базі даних.");
    }
}

/// <summary>
/// Заступник (Proxy) - контролює доступ до RealReservationManager, перевіряючи права доступу та ведучи логування.
/// </summary>
public class ReservationManagerProxy : IReservationManager
{
    private RealReservationManager _realManager;
    private readonly string _currentUserRole;

    /// <summary>
    /// Ініціалізує проксі із зазначенням ролі поточного користувача.
    /// </summary>
    /// <param name="userRole">Роль користувача, який намагається виконати дію.</param>
    public ReservationManagerProxy(string userRole)
    {
        _currentUserRole = userRole;
    }

    /// <summary>
    /// Перехоплює запит на скасування, перевіряє права і, за потреби, передає виклик реальному об'єкту.
    /// </summary>
    public void CancelReservation(int reservationId)
    {
        Console.WriteLine($"[ReservationManagerProxy] Перехоплено запит на скасування броні №{reservationId}. Користувач: {_currentUserRole}.");

        if (CheckAccess())
        {
            // Лінива ініціалізація реального об'єкта
            if (_realManager == null)
            {
                Console.WriteLine("[ReservationManagerProxy] Ініціалізація реального менеджера бронювань...");
                _realManager = new RealReservationManager();
            }

            _realManager.CancelReservation(reservationId);
            LogAccess(reservationId);
        }
        else
        {
            Console.WriteLine($"[ReservationManagerProxy] ВІДМОВА В ДОСТУПІ: Роль '{_currentUserRole}' не має прав для скасування бронювань!");
        }
    }

    /// <summary>
    /// Внутрішній метод перевірки прав доступу.
    /// </summary>
    private bool CheckAccess()
    {
        Console.WriteLine("[ReservationManagerProxy] Перевірка прав доступу...");
        // Тільки адміністратор або менеджер можуть скасовувати
        return _currentUserRole == "Admin" || _currentUserRole == "Manager";
    }

    /// <summary>
    /// Внутрішній метод логування дій.
    /// </summary>
    private void LogAccess(int reservationId)
    {
        Console.WriteLine($"[ReservationManagerProxy] ЛОГ: Користувач {_currentUserRole} скасував броню №{reservationId} о {DateTime.Now:HH:mm:ss}.");
    }
}