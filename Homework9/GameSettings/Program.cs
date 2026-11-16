using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameSettings saved = JsonSerializer.Deserialize<GameSettings>(json);

            if (saved == null)
            {
                Console.WriteLine("В файле нет настроек.");
                return;
            }

            Console.WriteLine($"Имя игрока: {saved.PlayerName}");
            Console.WriteLine($"Сложность: {saved.Difficulty}");
            Console.WriteLine($"Громкость музыки: {saved.MusicVolume}");
            return;
        }

        Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");
        Console.Write("Имя игрока: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Имя игрока должно быть заполнено.");
            return;
        }

        int difficulty;
        while (true)
        {
            Console.Write("Сложность (1 - легко, 2 - нормально, 3 - сложно): ");
            string input = Console.ReadLine();
            if (input == null)
            {
                return;
            }

            if (int.TryParse(input, out difficulty) && difficulty >= 1 && difficulty <= 3)
            {
                break;
            }

            Console.WriteLine("Введите целое число от 1 до 3.");
        }

        int volume;
        while (true)
        {
            Console.Write("Громкость музыки (0-100): ");
            string input = Console.ReadLine();
            if (input == null)
            {
                return;
            }

            if (int.TryParse(input, out volume) && volume >= 0 && volume <= 100)
            {
                break;
            }

            Console.WriteLine("Введите целое число от 0 до 100.");
        }

        GameSettings settings = new GameSettings();
        settings.PlayerName = name.Trim();
        settings.Difficulty = difficulty;
        settings.MusicVolume = volume;

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;

        string settingsJson = JsonSerializer.Serialize(settings, options);
        File.WriteAllText(path, settingsJson);
        Console.WriteLine("Настройки сохранены.");
    }
}
