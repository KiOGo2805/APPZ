using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Flyweight;

/// <summary>
/// Легковаговик (Flyweight) - зберігає внутрішній стан, спільний для багатьох столиків (наприклад, матеріал, зона розташування, опис).
/// </summary>
public class TableType
{
    public string Material { get; }
    public string Zone { get; }
    public string Description { get; }

    public TableType(string material, string zone, string description)
    {
        Material = material;
        Zone = zone;
        Description = description;
    }

    /// <summary>
    /// Виводить спільні характеристики столика.
    /// </summary>
    public void Display()
    {
        Console.WriteLine($"Зона: {Zone}, Матеріал: {Material}, Опис: {Description}");
    }
}

/// <summary>
/// Фабрика Легковаговиків - керує створенням та кешуванням об'єктів TableType.
/// </summary>
public class TableFactory
{
    private readonly List<TableType> _tableTypes = new List<TableType>();

    /// <summary>
    /// Повертає існуючий тип столика або створює новий.
    /// </summary>
    public TableType GetTableType(string material, string zone, string description)
    {
        Console.WriteLine($"[TableFactory.GetTableType] Запит на кешований тип: {zone}, {material}...");
        
        var type = _tableTypes.FirstOrDefault(t => 
            t.Material == material && t.Zone == zone && t.Description == description);

        if (type == null)
        {
            Console.WriteLine($"[TableFactory.GetTableType] Створення НОВОГО об'єкта характеристик та додавання до кешу.");
            type = new TableType(material, zone, description);
            _tableTypes.Add(type);
        }
        else
        {
            Console.WriteLine($"[TableFactory.GetTableType] ВИКОРИСТАНО КЕШОВАНИЙ об'єкт характеристик.");
        }

        return type;
    }

    /// <summary>
    /// Виводить загальну кількість унікальних об'єктів у пам'яті (для демонстрації економії пам'яті на захисті).
    /// </summary>
    public void DisplayCacheStats()
    {
        Console.WriteLine($"[TableFactory] Всього унікальних об'єктів TableType в пам'яті: {_tableTypes.Count}");
    }
}