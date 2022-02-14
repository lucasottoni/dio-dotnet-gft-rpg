namespace SkyRpg
{
    public class ConsoleHelper
    {

        internal static string AskString(string question, bool canBeEmpty)
        {
            do
            {
                Console.Write(question + " ");
                string _line = Console.ReadLine() ?? "";
                if (!string.IsNullOrEmpty(_line) || canBeEmpty) return _line;
            } while (true);
        }

        internal static string AskOptions(string question, Dictionary<string, string> options)
        {
            question += " (";
            foreach (var op in options)
            {
                question += String.Format("{0} - {1}, ", op.Key, op.Value);
            }
            question += ") ";

            do
            {
                Console.Write(question);

                string? _line = Console.ReadLine();

                foreach (var op in options)
                {
                    if (op.Key.ToUpper() == _line?.ToUpper())
                    {
                        return op.Key;
                    }
                }
            } while (true);
        }

        internal static bool AskBoolean(string question)
        {
            bool ok;
            string? _line;

            do
            {
                Console.Write(question + " (Y/N) ");
                _line = Console.ReadLine() ?? "";
                ok = ("Y" == _line.ToUpper() || "N" == _line.ToUpper());
            } while (!ok);

            return "Y" == _line;
        }

        internal static int AskIntMinMax(string question, int min, int max)
        {
            int value = AskInt(String.Format(question + " ({0}-{1}) ", min, max));
            if (value < min) value = min;
            else if (value > max) value = max;
            return value;
        }

        internal static int AskInt(string question)
        {
            int value;
            bool ok;

            do
            {
                Console.Write(question);
                string? _line = Console.ReadLine();

                ok = Int32.TryParse(_line, out value);
                return value;
            } while (!ok);
        }
    }

}