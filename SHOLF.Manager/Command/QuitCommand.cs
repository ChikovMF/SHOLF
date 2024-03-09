using SHOLF.Manager.UserInterface;

namespace SHOLF.Manager.Command
{
    public class QuitCommand : BaseCommand
    {
        public QuitCommand(IUserInterface ui) : base(true, ui)
        {
        }

        protected override bool InternalCommand()
        {
            Interface.WriteMessage("Thank you for using SHOLF");

            return true;
        }
    }
}