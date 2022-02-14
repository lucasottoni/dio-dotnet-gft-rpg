using SkyRpg.Entities;

namespace SkyRpg.Domain
{
    public class Battle
    {

        private Hero leftHero;
        private Hero rightHero;
        private int rounds;
        private int currentRound;
        private int totalRounds;

        public Battle(Hero leftHero, Hero rightHero, int rounds)
        {
            this.leftHero = leftHero;
            this.rightHero = rightHero;
            this.rounds = rounds;
            this.currentRound = 1;
            this.totalRounds = 0;
        }

        public int CurrentRound => this.currentRound;

        public Hero? DoAttack(Hero attacker, Hero defender, int bonus)
        {
            if (!defender.IsAlive)
            {
                Console.WriteLine("'{0}' is not alive", defender.Name);
            }
            attacker.Attack(defender, bonus);
            this.currentRound++;

            Hero winner = attacker.Hp > defender.Hp ? attacker : defender;
            Hero defeated = attacker.Hp > defender.Hp ? defender : attacker;

            if (currentRound > this.rounds || !defeated.IsAlive)
            {
                this.PrintBattleStatus();

                Console.WriteLine("\n============ FINAL STATUS ===========");
                if (currentRound > this.rounds)
                {
                    Console.WriteLine("Reached maximun number of rounds {0}.", this.rounds);                    
                }
                Console.WriteLine("'{0}' was defeated by '{1}'.", defeated.Name, winner.Name);
                winner.Evolve();
                Console.WriteLine("=======================================");
                return winner;
            }
            return null;
        }

        public void ChangeHero(Hero defeated, Hero newHero)
        {
            if (leftHero == defeated) leftHero = newHero;
            else rightHero = newHero;

            this.currentRound = 1;
        }

        public ValueTuple<Hero, Hero> NextRound()
        {
            Hero attacker = totalRounds % 2 == 0 ? leftHero : rightHero;
            Hero defender = totalRounds % 2 == 0 ? rightHero : leftHero;

            if (this.currentRound == 1)
            {
                Console.WriteLine("\n====================================================");
                Console.WriteLine("Starting a battle with '{0}' L {1}, against '{2}' L {3}.", attacker.Name, attacker.Level, defender.Name, defender.Level);
            }

            this.totalRounds++;

            return new(attacker, defender);
        }

        public void Catchup(Hero a, Hero b)
        {
            if (a.Level == b.Level) return;

            Hero weaker = a.Level < b.Level ? a : b;
            Hero stronger = a.Level < b.Level ? b : a;

            weaker.EvolveTo(stronger.Level);
        }

        public void PrintBattleStatus()
        {
            Console.WriteLine("\n============================================================");
            Console.WriteLine("|{0,-20}|{1,-10}|{2,5}|{3,14}|{4,14}|", "NAME", "CLASS", "LEVEL", "HP", "MANA");
            Console.WriteLine("|{0,-20}|{1,-10}|{2,5}|{3,14}|{4,14}|", leftHero.Name, leftHero.Class, leftHero.Level, leftHero.Hp + "/" + leftHero.MaxHp, leftHero.Mana + "/" + leftHero.MaxMana);
            Console.WriteLine("|{0,-20}|{1,-10}|{2,5}|{3,14}|{4,14}|", rightHero.Name, rightHero.Class, rightHero.Level, rightHero.Hp + "/" + rightHero.MaxHp, rightHero.Mana + "/" + rightHero.MaxMana);
            Console.WriteLine("==============================================================");
        }
    }
}