using SHOLF.Manager.UserInterface;

namespace SHOLF.Manager.Command
{
    public class UnknownCommand : BaseCommand
    {
        public UnknownCommand(IUserInterface ui) : base(ui) { }

        protected override bool InternalCommand()
        {
            Interface.WriteWarning("Unknown command. Type 'help' for help.");

            return true;
        }
    }
}