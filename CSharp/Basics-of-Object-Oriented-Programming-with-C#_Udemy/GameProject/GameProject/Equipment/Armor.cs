using GameProject.Enums;

namespace GameProject.Equipment
{
    class Armor
    {
        private const int HERO_STARTING_DEFENCE = 5;
        private const int VILLAIN_STARTING_DEFENCE = 5;

        private int defence;

        public int Defence
        {
            get
            {
                return defence;
            }
        }

        public Armor(Faction faction)
        {
            switch (faction)
            {
                case Faction.Hero:
                    defence = HERO_STARTING_DEFENCE;
                    break;
                case Faction.Villain:
                    defence = VILLAIN_STARTING_DEFENCE;
                    break;
                default:
                    break;
            }
        }
    }
}
