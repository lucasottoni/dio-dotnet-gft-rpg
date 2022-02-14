namespace SkyRpg.Entities
{

    public abstract class Character
    {
        protected string _name;

        protected Character(string name)
        {
            this._name = name;
        }

        public abstract void BeAttacked(int damage);

        public string Name => this._name;
    }
}