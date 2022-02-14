using SkyRpg;
using SkyRpg.Entities;
using SkyRpg.Domain;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Start fight!");

Console.WriteLine("Setup the first fighter:");
Hero left = SetupFighter();

Console.WriteLine("Setup the next fighter:");
Hero right = SetupFighter();

int rounds = ConsoleHelper.AskIntMinMax("How many rounds there will be?", 1, 10);

Battle battle = new Battle(left, right, rounds);
bool _proceed;
do
{
    var heroes = battle.NextRound();

    Console.WriteLine("\nROUND {0}", battle.CurrentRound);
    int bonus = ConsoleHelper.AskIntMinMax(String.Format("What level of bonus '{0}' will use?", heroes.Item1.Name), 0, heroes.Item1.MaxBonus);

    Hero? winner = battle.DoAttack(heroes.Item1, heroes.Item2, bonus);

    if (winner != null)
    {
        _proceed = CheckNextFighter(winner, heroes.Item1 == winner ? heroes.Item2 : heroes.Item1);
    } else {
        battle.PrintBattleStatus();
        _proceed = true;
    }
} while (_proceed);

Hero SetupFighter()
{
    string _class;
    string _name;

    _class = ConsoleHelper.AskOptions("\tClass:", new Dictionary<string, string> { { "1", "Knight" }, { "2", "Ninja" }, { "X", "Exit" } });
    if (_class.ToUpper() == "X")
    {
        Environment.Exit(0);
        return null;
    }

    _name = ConsoleHelper.AskString("\tName: ", false);

    switch (_class)
    {
        case "1": return new Knight(_name!);
        case "2": return new Ninja(_name!);
        default:
            Console.WriteLine("Invalid options!");
            Environment.Exit(0);
            return null;
    }
}

bool CheckNextFighter(Hero winner, Hero defeated)
{
    bool nextFighter = ConsoleHelper.AskBoolean(String.Format("Do you want to replace '{0}' by another fighter?", defeated.Name));
    if (!nextFighter)
    {
        Console.WriteLine("'{0}' won the championship! See you next battle!", winner.Name);
        return false;
    }
    Hero newFighter = SetupFighter();
    battle.ChangeHero(defeated, newFighter);

    if (newFighter.Level < winner.Level)
    {
        bool catchup = ConsoleHelper.AskBoolean(String.Format("Do you want to catchup '{0}' with the same level of '{1}'?", newFighter.Name, winner.Name));
        if (catchup)
        {
            battle.Catchup(newFighter, winner);
        }
    }
    return true;
}
