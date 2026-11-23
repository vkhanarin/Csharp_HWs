using System;

public interface IDamageCalculator
{
    int CalculateDamage(int baseDamage, int targetArmor);
}

public class PhysicalDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        return Math.Max(1, baseDamage - targetArmor / 2);
    }
}

public class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 4);

        if (targetArmor == 0)
        {
            damage *= 2;
        }

        return damage;
    }
}

public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 10);

        if (baseDamage > 50)
        {
            damage += 10;
        }

        return damage;
    }
}

public class Unit
{
    public int HP { get; set; }
    public int Armor { get; private set; }
    public string Name { get; private set; }

    public Unit(string name, int hp, int armor)
    {
        Name = name;
        HP = hp;
        Armor = armor;
    }
}

public class DamageSystem
{
    private readonly IDamageCalculator calculator;

    public DamageSystem(IDamageCalculator calculator)
    {
        this.calculator = calculator;
    }

    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = calculator.CalculateDamage(baseDamage, target.Armor);
        target.HP = Math.Max(0, target.HP - damage);
        Console.WriteLine("Юнит {0} получил {1} урона. Осталось HP: {2}", target.Name, damage, target.HP);
    }
}

class Program
{
    static void Main()
    {
        Unit target = new Unit("Страж", 180, 40);
        int baseDamage = 60;

        DamageSystem physical = new DamageSystem(new PhysicalDamageCalculator());
        DamageSystem magic = new DamageSystem(new MagicDamageCalculator());
        DamageSystem fire = new DamageSystem(new FireDamageCalculator());

        Console.WriteLine("Физический урон");
        physical.ApplyDamage(target, baseDamage);

        Console.WriteLine("Магический урон");
        magic.ApplyDamage(target, baseDamage);

        Console.WriteLine("Огненный урон");
        fire.ApplyDamage(target, baseDamage);
    }
}

// Для ледяного урона создадим класс IceDamageCalculator, который реализует
// IDamageCalculator, и зададим свою формулу в CalculateDamage.
// Передадим new IceDamageCalculator() в конструктор DamageSystem.
