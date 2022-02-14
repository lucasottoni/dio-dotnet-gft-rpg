using SkyRpg.Domain;

namespace SkyRpg.Entities
{
    public class HeroBasicHelper
    {
        private Hero hero;
        private Random _random = new Random();

        public HeroBasicHelper(Hero hero)
        {
            this.hero = hero;
        }


        public HitInfo CommonHitStats(int bonus, int basicDamage, double dmgMultiplier, int[] manaRanges, string[] attackNames)
        {
            if (manaRanges.Length != attackNames.Length)
            {
                throw new ArgumentException("Mana and Attack arrays must be the same length!");
            }
            if (manaRanges.Length != hero.MaxBonus + 1)
            {
                throw new ArgumentException("Mana and Attack arrays must have " + (hero.MaxBonus + 1) + " elements!");
            }

            int manaUsage = 0;
            if (bonus < 0) bonus = 0;
            if (bonus >= manaRanges.Length) bonus = manaRanges.Length - 1;

            //if (bonus > this._level)
            //{
            //    Console.WriteLine("{0} cannot use {1} bonus yet! So it is capped to {2}", this.Name, bonus, this._level);
            //    bonus = this._level;
            //}

            bonus = FindCurrentMaxBonus(bonus, manaRanges);
            if (bonus > 0)
            {

                int min = manaRanges[bonus - 1];
                int max = manaRanges[bonus];
                if (max > hero.Mana) max = hero.Mana;
                if (min > max) min = max;

                manaUsage = _random.Next(min, max + 1);
            }

            double multiplier = 1;
            if (manaUsage > 0 && hero.Mana >= manaUsage)
            {
                double maxManaUsage = manaRanges.Last();
                multiplier += (manaUsage / maxManaUsage);
            }

            int maxDamage = (int)(BasicDamageFromLevel(basicDamage, hero.Level, dmgMultiplier) * multiplier);
            int minDamage = maxDamage / 5;

            int damage = _random.Next(minDamage, maxDamage);
            //Console.WriteLine("Min Damage {0}, Max {1}, DMG {2}, MULT {3}", minDamage, maxDamage, damage, multiplier);

            return new HitInfo(attackNames[bonus], damage, manaUsage, bonus);
        }

        private int BasicDamageFromLevel(int basicDamage, int level, double dmgMultiplier)
        {
            double dmg = basicDamage;
            for (int i = 1; i < level; ++i)
            {
                dmg += (dmg * dmgMultiplier);
            }

            return (int)dmg;
        }

        private int FindCurrentMaxBonus(int bonus, int[] manaRanges)
        {
            if (bonus > hero.Level * 2)
                bonus = hero.Level * 2;
            for (int i = bonus; i >= 0; --i)
            {
                if (manaRanges[i] <= hero.Mana) return i;
            }
            return 0;
        }
    }
}