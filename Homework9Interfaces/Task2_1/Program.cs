using System;

public interface ICombatant
{
    string Name { get; }
    int HP { get; set; }
    bool IsDead { get; }
    int Damage { get; }
    void Attack(ICombatant target);
    void TakeDamage(int amount);
}

public class Knight : ICombatant
{
    public string Name { get; private set; }
    public int HP { get; set; }
    public int Damage { get; private set; }
    public bool IsDead
    {
        get { return HP <= 0; }
    }

    public Knight(string name)
    {
        Name = name;
        HP = 150;
        Damage = 25;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine("Рыцарь {0} мёртв и не может атаковать.", Name);
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine("Рыцарь {0} не может атаковать мёртвую цель.", Name);
            return;
        }

        Console.WriteLine("Рыцарь {0} атакует!", Name);
        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine("Рыцарь {0} получил {1} урона. Осталось HP: {2}", Name, amount, HP);
    }
}

public class Mage : ICombatant
{
    public string Name { get; private set; }
    public int HP { get; set; }
    public int Damage { get; private set; }
    public bool IsDead
    {
        get { return HP <= 0; }
    }

    public Mage(string name)
    {
        Name = name;
        HP = 80;
        Damage = 40;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine("Маг {0} мёртв и не может атаковать.", Name);
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine("Маг {0} не может атаковать мёртвую цель", Name);
            return;
        }

        Console.WriteLine("Маг {0} атакует!", Name);
        target.TakeDamage(Damage);

        if (!target.IsDead)
        {
            target.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine("Маг {0} получил {1} урона. Осталось HP: {2}", Name, amount, HP);
    }
}

class Program
{
    static void Main()
    {
        Knight knight = new Knight("Артур");
        Mage mage = new Mage("Мерлин");

        while (!knight.IsDead && !mage.IsDead)
        {
            knight.Attack(mage);

            if (!mage.IsDead)
            {
                mage.Attack(knight);
            }
        }

        if (knight.IsDead)
        {
            Console.WriteLine("Победил Маг {0}!", mage.Name);
        }
        else
        {
            Console.WriteLine("Победил Рыцарь {0}!", knight.Name);
        }
    }
}
