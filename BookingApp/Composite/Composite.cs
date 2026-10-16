using System;
using System.Collections.Generic;

namespace BookingApp.Composite;

/// <summary>
/// Компонент (Component) - загальний інтерфейс для всіх елементів меню.
/// </summary>
public interface IMenuItem
{
    /// <summary>
    /// Отримує вартість елемента меню.
    /// </summary>
    /// <returns>Вартість у вигляді десяткового числа.</returns>
    decimal GetPrice();

    /// <summary>
    /// Виводить інформацію про елемент меню на консоль.
    /// </summary>
    /// <param name="depth">Глибина вкладеності для візуального форматування (відступи).</param>
    void Display(int depth = 0);
}

/// <summary>
/// Листок (Leaf) - окрема страва, яка не містить всередині інших під-елементів.
/// </summary>
public class Dish : IMenuItem
{
    private readonly string _name;
    private readonly decimal _price;

    /// <summary>
    /// Ініціалізує нову страву із заданою назвою та ціною.
    /// </summary>
    /// <param name="name">Назва страви.</param>
    /// <param name="price">Ціна страви.</param>
    public Dish(string name, decimal price)
    {
        _name = name;
        _price = price;
    }

    /// <summary>
    /// Повертає ціну конкретної страви.
    /// </summary>
    public decimal GetPrice()
    {
        Console.WriteLine($"[Dish.GetPrice] Отримання ціни для окремої страви '{_name}': {_price} грн.");
        return _price;
    }

    /// <summary>
    /// Виводить назву та ціну страви.
    /// </summary>
    public void Display(int depth = 0)
    {
        string indent = new string('-', depth);
        Console.WriteLine($"{indent} Страва: {_name} ({_price} грн)");
    }
}

/// <summary>
/// Компонувальник (Composite) - комплексне меню або комбо-набір, що містить список інших елементів (як окремих страв, так і інших комбо).
/// </summary>
public class ComboMeal : IMenuItem
{
    private readonly string _name;
    private readonly List<IMenuItem> _items = new List<IMenuItem>();

    /// <summary>
    /// Ініціалізує новий комбо-набір.
    /// </summary>
    /// <param name="name">Назва комбо-набору.</param>
    public ComboMeal(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Додає новий елемент (страву або інший набір) до цього комбо-набору.
    /// </summary>
    /// <param name="item">Елемент меню для додавання.</param>
    public void Add(IMenuItem item)
    {
        Console.WriteLine($"[ComboMeal.Add] Додавання елемента до набору '{_name}'.");
        _items.Add(item);
    }

    /// <summary>
    /// Видаляє існуючий елемент з комбо-набору.
    /// </summary>
    /// <param name="item">Елемент меню для видалення.</param>
    public void Remove(IMenuItem item)
    {
        Console.WriteLine($"[ComboMeal.Remove] Видалення елемента з набору '{_name}'.");
        _items.Remove(item);
    }

    /// <summary>
    /// Рекурсивно підраховує загальну вартість усіх вкладених елементів.
    /// </summary>
    public decimal GetPrice()
    {
        Console.WriteLine($"[ComboMeal.GetPrice] Початок підрахунку вартості для комбо '{_name}'...");
        decimal totalPrice = 0;
        
        foreach (var item in _items)
        {
            totalPrice += item.GetPrice();
        }
        
        Console.WriteLine($"[ComboMeal.GetPrice] Загальна вартість комбо '{_name}' становить: {totalPrice} грн.");
        return totalPrice;
    }

    /// <summary>
    /// Виводить інформацію про комбо-набір та рекурсивно викликає вивід для всіх вкладених елементів.
    /// </summary>
    public void Display(int depth = 0)
    {
        string indent = new string('-', depth);
        Console.WriteLine($"{indent} Комбо-набір: {_name}");
        
        foreach (var item in _items)
        {
            item.Display(depth + 2);
        }
    }
}