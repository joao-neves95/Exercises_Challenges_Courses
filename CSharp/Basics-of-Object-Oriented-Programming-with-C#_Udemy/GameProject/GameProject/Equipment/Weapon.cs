using GameProject.Enums;

namespace GameProject.Equipment
{
    class Weapon
    {
        private const int HERO_STARTING_DAMAGE = 10;
        private const int VILLAIN_STARTING_DAMAGE = 10;

        private int damage;

        public int Damage
        {
            get
            {
                return damage;
            }
        }

        public Weapon(Faction faction)
        {
            switch (faction)
            {
                case Faction.Hero:
                    damage = HERO_STARTING_DAMAGE;
                    break;
                case Faction.Villain:
                    damage = VILLAIN_STARTING_DAMAGE;
                    break;
                default:
                    break;
            }
        }
    }
}
