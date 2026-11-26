using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        GameCharacter character = new GameCharacter
        {
            Name = "Alex",
            Level = 5,
            Health = 87.5,
            Inventory = new string[] { "Sword", "Shield", "Potion" }
        };

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;

        string json = JsonSerializer.Serialize(character, options);
        Console.WriteLine(json);

        GameCharacter restored = JsonSerializer.Deserialize<GameCharacter>(json);
        Console.WriteLine($"Из JSON: {restored.Name}, предметов: {restored.Inventory.Length}");

        string path = Path.Combine(AppContext.BaseDirectory, "save.json");
        File.WriteAllText(path, json);

        string savedJson = File.ReadAllText(path);
        GameCharacter loaded = JsonSerializer.Deserialize<GameCharacter>(savedJson);

        Console.WriteLine();
        Console.WriteLine("=== Загруженный персонаж ===");
        Console.WriteLine($"Имя: {loaded.Name}");
        Console.WriteLine($"Уровень: {loaded.Level}");
        Console.WriteLine($"Здоровье: {loaded.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(", ", loaded.Inventory)}");
    }
}
