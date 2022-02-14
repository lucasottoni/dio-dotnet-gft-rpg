using SkyRpg.Domain;

namespace SkyRpg.Entities
{

    public abstract class Hero : Character
    {
        protected string _clazz;
        protected int _level = 1;
        protected int _maxlevel;
        protected int _mana;
        protected int _hp;
        protected int _maxMana;
        protected int _maxHp;
        protected int _maxBonus;
        private bool _alive;
        protected Hero(string name, string clazz, int maxLevel, int startHp, int startMana, int maxBonus) : base(name)
        {
            this._clazz = clazz;
            this._level = 1;
            this._maxHp = startHp;
            this._maxMana = startMana;
            this._maxlevel = maxLevel;
            this._maxBonus = maxBonus;

            this._hp = this._maxHp;
            this._mana = this._maxMana;
            this._alive = true;
        }

        protected abstract float HpEvolveFactor { get; }
        protected abstract float ManaEvolveFactor { get; }
        protected abstract HitInfo CalculateHitStats(int bonus);

        public string Class => this._clazz;
        public int Hp => this._hp;
        public int Mana => this._mana;
        public int MaxHp => this._maxHp;
        public int MaxMana => this._maxMana;
        public bool IsAlive => this._alive;
        public int MaxBonus => this._maxBonus;
        public int Level => this._level;

        public void Attack(Character character, int bonus = 0)
        {
            if (!this._alive)
            {
                Console.WriteLine("{0} is not alive and cannot attack!", this.Name);
                return;
            }
            HitInfo hitInfo = CalculateHitStats(bonus);
            if (this._mana < hitInfo.ManaUsage)
            {
                Console.WriteLine("Insufficient mana for attack");
                return;
            }
            character.BeAttacked(hitInfo.Damage);
            this._mana -= hitInfo.ManaUsage;

            Console.WriteLine("'{0}' hit '{1}' with {2}(bonus {4} - {5} mana) causing {3} damage.", this.Name, character.Name, hitInfo.Name, hitInfo.Damage, hitInfo.Bonus, hitInfo.ManaUsage);
        }

        public override void BeAttacked(int damage)
        {
            this._hp -= damage;
            if (this._hp <= 0)
            {
                this._hp = 0;
                this._alive = false;
            }
        }

        public void Die()
        {
            this._alive = false;
        }

        public void Evolve()
        {
            if (this._alive && this._level < this._maxlevel)
            {
                InternalEvolve();
                Console.WriteLine("'{0}' has evolved to level {1}.", this.Name, this._level);
            }
        }

        public void EvolveTo(int level)
        {
            while (this._level < level)
            {
                InternalEvolve();
            }
        }

        private void InternalEvolve()
        {
            this._level++;
            this._maxHp = (int)(this._maxHp * (1 + this.HpEvolveFactor));
            this._maxMana = (int)(this._maxMana * (1 + this.ManaEvolveFactor));

            this._hp = this._maxHp;
            this._mana = this._maxMana;
        }
    }
}