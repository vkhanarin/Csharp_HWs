# ДЗ 9. Сериализация, потоки, чтение и запись

## Задача 1

**а)** Результат - `{"Name":"Alex","Score":100}`.

- `Name` и `Score` - публичные свойства с геттерами, поэтому сериализуются.
- `secretCode` - приватное поле, поэтому по умолчанию пропускается.
- `IsAlive` - публичное поле, а поля по умолчанию пропускаются.

**б)** В .NET 8 можно добавить к приватному полю атрибут `JsonInclude`. При обычном вызове `JsonSerializer.Serialize(p)` поле попадёт в JSON под именем `secretCode`.

```csharp
using System.Text.Json.Serialization;

// Внутри класса Player
[JsonInclude]
private int secretCode = 42;
```

**в)** `IsAlive` пропускается по умолчанию, потому что объявлено как поле. Можно оставить поле и пометить его атрибутом `[JsonInclude]` или включить сериализацию публичных полей через настройки:

```csharp
JsonSerializerOptions options = new JsonSerializerOptions();
options.IncludeFields = true;
string json = JsonSerializer.Serialize(p, options);
```

## Задача 2

**а)**

```csharp
public class Game
{
    public string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; } = new string[0];
    public bool IsMultiplayer { get; set; }
}
```

**б)** По умолчанию имена свойств сопоставляются с учётом регистра. Ключ `title` не совпадает с `Title`, и свойство сохраняет исходное значение `null`. Чтобы принимать оба написания, можно передать настройки:

```csharp
JsonSerializerOptions options = new JsonSerializerOptions();
options.PropertyNameCaseInsensitive = true;
Game game = JsonSerializer.Deserialize<Game>(json, options);
```

**в)** Это валидный JSON: элементы массива могут иметь разные типы. Его можно десериализовать в `JsonElement[]`. Свойство `ValueKind` покажет тип каждого элемента, включая `Null`.

## Задача 3

1. **File.** `File.ReadAllText` сразу прочитает небольшой файл в строку для вывода.
2. **File.** Если сохранение небольшое, достаточно сериализовать прогресс в JSON и записать его через `File.WriteAllText`. При следующем сохранении файл будет перезаписан.
3. **FileStream.** Поток с `FileMode.Append` можно держать открытым и дописывать строки через `StreamWriter`. Размер уже записанной части журнала не влияет на объём данных, который нужно прочитать для дописывания.
4. **File.** `File.Exists` проверит наличие файла, а `File.ReadAllText` прочитает его целиком.
5. **FileStream.** Можно открыть файл, перейти к нужной позиции через `Seek` и записать 10 байт поверх старых.

## Задачи 4 и 5

Из корня репозитория:

```bash
dotnet run --project Homework9/CharacterSave
```

Программа выводит JSON и данные после десериализации строки, затем сохраняет `save.json` рядом с приложением и выводит персонажа, загруженного из файла.

## Задача 6

```bash
dotnet run --project Homework9/GameSettings
```

При первом запуске программа запрашивает данные и создаёт `settings.json` рядом с приложением. При повторном запуске она выводит сохранённые настройки.
