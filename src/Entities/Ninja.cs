using SkyRpg.Domain;

namespace SkyRpg.Entities
{
    public class Ninja : Hero
    {
        private const int BASIC_DAMAGE = 75;
        private const int MAX_BONUS = 5;
        private const int MAX_LEVEL = 150;
        private const int START_HP = 125;
        private const int START_MANA = 125;
        private int[] _manaRanges = { 0, 25, 50, 75, 100, 150 };
        private string[] _attackNames = { "PUNCH", "KICK", "SHURIKEN", "KUNAI", "KATANA", "NINJUTSU" };

        protected override float HpEvolveFactor => 0.15f;

        protected override float ManaEvolveFactor => 0.25f;

        public Ninja(string name) : base(name, "Ninja", MAX_LEVEL, START_HP, START_MANA, MAX_BONUS)
        {

        }

        protected override HitInfo CalculateHitStats(int bonus)
        {
            HeroBasicHelper helper = new HeroBasicHelper(this);
            return helper.CommonHitStats(bonus, BASIC_DAMAGE, 0.5f, this._manaRanges, this._attackNames);
        }
    }
}