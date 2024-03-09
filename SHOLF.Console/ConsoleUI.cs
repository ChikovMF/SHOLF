using SHOLF.Manager.UserInterface;

namespace SHOLF.Console
{
    public class ConsoleUI : IUserInterface
    {
        public string ReadValue(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write(message);
            return System.Console.ReadLine() ?? string.Empty;
        }

        public void WriteMessage(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(message);
        }

        public void WriteWarning(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine(message);
        }
    }
}