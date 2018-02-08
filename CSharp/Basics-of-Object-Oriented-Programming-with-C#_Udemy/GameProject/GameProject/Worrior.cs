using System;
using GameProject.Enums;
using GameProject.Equipment;

namespace GameProject
{
    class Worrior
    {
        private const int HERO_STARTING_HEALTH = 100;
        private const int VILLAIN_STARTING_HEATH = 100;

        private readonly Faction FACTION;

        private int health;
        private string name;
        private bool isAlive;

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
        }

        private Weapon weapon;
        private Armor armor;

        public Worrior(string name, Faction faction)
        {
            this.name = name;
            this.FACTION = faction;
            this.isAlive = true;

            switch (faction)
            {
                case Faction.Hero:
                    weapon = new Weapon(faction);
                    armor = new Armor(faction);
                    health = HERO_STARTING_HEALTH;
                    break;
                case Faction.Villain:
                    weapon = new Weapon(faction);
                    armor = new Armor(faction);
                    health = VILLAIN_STARTING_HEATH;
                    break;
                default:
                    break;
            }
        }

        public void Attack(Worrior enemy)
        {
            int damage = weapon.Damage / enemy.armor.Defence;
            enemy.health -= damage;

            AttackControlResult(enemy, damage);
        }

        private void AttackControlResult(Worrior enemy, int damage)
        {
            if (enemy.health <= 0)
            {
                enemy.isAlive = false;
                Utilities.ColorfulWriteLine($"{enemy.name} is dead!", ConsoleColor.Red);
                Utilities.ColorfulWriteLine($"{this.name} has won!", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine($"{this.name} atacked {enemy.name}. {damage} damage was inflicted to {enemy.name}, remaining heath of {enemy.name} is {enemy.health}.");
            }
        }
    }
}
