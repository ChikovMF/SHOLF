using SHOLF.Manager.UserInterface;

namespace SHOLF.Manager.Command
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand(IUserInterface ui) : base(ui) { }

        protected override bool InternalCommand()
        {
            Interface.WriteMessage("Commands:");

            Interface.WriteMessage("\thelp (h)");
            Interface.WriteMessage("\tquit (q)");
            Interface.WriteMessage("\thash");

            Interface.WriteMessage("\n");

            return true;
        }
    }
}