using SkyRpg.Domain;

namespace SkyRpg.Entities
{
    public class Knight : Hero
    {
        private const int BASIC_DAMAGE = 50;
        private const int MAX_BONUS = 5;
        private const int MAX_LEVEL = 100;
        private const int START_HP = 200;
        private const int START_MANA = 50;
        private readonly int[] _manaRanges = { 0, 15, 25, 40, 60, 80 };
        private readonly string[] _attackNames = { "PUNCH", "SWORD SLASH", "DOUBLE SLASH", "MEGA SLASH", "SWORD CYCLONE", "MEGA TORNADO" };

        protected override float HpEvolveFactor => 0.25f;
        protected override float ManaEvolveFactor => 0.15f;

        public Knight(string name) : base(name, "Knight", MAX_LEVEL, START_HP, START_MANA, MAX_BONUS)
        {

        }

        protected override HitInfo CalculateHitStats(int bonus)
        {
            HeroBasicHelper helper = new HeroBasicHelper(this);
            return helper.CommonHitStats(bonus, BASIC_DAMAGE, 0.25f, this._manaRanges, this._attackNames);
        }
    }
}