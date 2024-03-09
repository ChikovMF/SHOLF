using SHOLF.Manager.Command;
using SHOLF.Manager.UserInterface;

namespace SHOLF.Console
{
    interface IClientService
    {
        void Run();
    }

    public class ClientService : IClientService
    {
        private readonly IUserInterface _ui;

        public ClientService(IUserInterface ui)
        {
            _ui = ui;
        }

        public void Run()
        {
            Greeting();

            var response = GetCommand("?").RunCommand();

            while (!response.shouldQuit)
            {
                var input = _ui.ReadValue("> ").ToLower();
                var command = GetCommand(input);

                response = command.RunCommand();

                if (!response.wasSuccessful)
                {
                    _ui.WriteMessage("Enter 'help' to view options.");
                }
            }
        }

        private void Greeting()
        {
            _ui.WriteMessage("Welcome to SHOLF!");
        }

        public BaseCommand GetCommand(string input)
        {
            switch (input)
            {
                case "q":
                case "quit":
                    return new QuitCommand(_ui);
                case "hash":
                    return new HashCommand(_ui);
                case "h":
                case "help":
                    return new HelpCommand(_ui);
                default:
                    return new UnknownCommand(_ui);
            }
        }
    }
}