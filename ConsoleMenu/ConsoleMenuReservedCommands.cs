namespace ConsoleMenu
{
    using System.Linq;

    internal class ConsoleMenuReservedCommands
    {
        public const string ExitCommand = "x";

        public const string DisplayCommand = "d";

        public const string ClearScreenCommand = "c";

        public static string[] ReservedCommands = { ExitCommand, DisplayCommand, ClearScreenCommand };

        public static bool IsReservedCommand(string command)
        {
            return ReservedCommands.Contains(command.ToLowerInvariant());
        }
    }
}