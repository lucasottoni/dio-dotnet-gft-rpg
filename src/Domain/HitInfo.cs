namespace SkyRpg.Domain
{
    public class HitInfo
    {
        private readonly int _manaUsage;
        private readonly string _name;
        private readonly int _damage;
        private readonly int _bonus;

        public HitInfo(string name, int damage, int manaUsage, int bonus)
        {
            this._name = name;
            this._damage = damage;
            this._manaUsage = manaUsage;
            this._bonus = bonus;
        }

        public string Name => this._name;
        public int Damage => this._damage;
        public int ManaUsage => this._manaUsage;
        public int Bonus => this._bonus;
    }
}