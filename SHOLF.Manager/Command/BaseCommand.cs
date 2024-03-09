using SHOLF.Manager.UserInterface;

namespace SHOLF.Manager.Command
{
    public abstract class BaseCommand
    {
        private readonly bool _isClosingCommand;
        protected IUserInterface Interface { get; }

        public (bool wasSuccessful, bool shouldQuit) RunCommand()
        {
            if (this is IParameterisedCommand parameterisedCommand)
            {
                var allParametersCompleted = false;

                while (allParametersCompleted == false)
                {
                    allParametersCompleted = parameterisedCommand.GetParameters();
                }
            }

            return (InternalCommand(), _isClosingCommand);
        }

        protected abstract bool InternalCommand();

        protected string GetParameter(string parameterName)
        {
            return Interface.ReadValue($"Enter {parameterName}:");            
        }

        protected BaseCommand(bool isClosingCommand, IUserInterface ui)
        {
            _isClosingCommand = isClosingCommand;
            Interface = ui;
        }

        protected BaseCommand(IUserInterface userInterface)
        {
            _isClosingCommand = false;
            Interface = userInterface;
        }
    }
}