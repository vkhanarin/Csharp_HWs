using System;
using System.Collections.Generic;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public class Player
{
    public int HP;
    public int Damage;
    public string Name;

    public Player(string name, int hp, int damage)
    {
        Name = name;
        HP = hp;
        Damage = damage;
    }

    public void Heal(int amount)
    {
        HP += amount;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }
}

public class HealthPotion : IUsableItem
{
    public string Name { get { return "Зелье здоровья"; } }
    public string Description { get { return "Восстанавливает 50 HP."; } }
    public Rarity Rarity { get { return Rarity.Common; } }

    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine("Вы выпили зелье и восстановили 50 HP.");
    }
}

public class Sword : IUsableItem
{
    public string Name { get { return "Меч"; } }
    public string Description { get { return "Увеличивает урон на 10."; } }
    public Rarity Rarity { get { return Rarity.Rare; } }

    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine("Вы взяли меч. Урон увеличился на 10.");
    }
}

public class AncientScroll : IUsableItem
{
    public string Name { get { return "Древний свиток"; } }
    public string Description { get { return "Отнимает 20 HP и увеличивает урон на 30."; } }
    public Rarity Rarity { get { return Rarity.Legendary; } }

    public void Use(Player player)
    {
        player.HP -= 20;
        player.IncreaseDamage(30);
        Console.WriteLine("Вы прочитали свиток, потеряли 20 HP и увеличили урон на 30.");
    }
}

class Program
{
    static void Main()
    {
        Player player = new Player("Артур", 100, 25);
        List<IUsableItem> items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        foreach (IUsableItem item in items)
        {
            item.Use(player);
        }

        Console.WriteLine("Игрок: {0}", player.Name);
        Console.WriteLine("HP: {0}", player.HP);
        Console.WriteLine("Урон: {0}", player.Damage);

        if (player.HP > 0)
        {
            Console.WriteLine("Состояние: жив");
        }
        else
        {
            Console.WriteLine("Состояние: мёртв");
        }
    }
}
