namespace ConsoleMenu
{
    using System.Linq;

    internal class ConsoleCommand
    {
        internal ConsoleCommand(string commandLine)
        {
            var commandLineTokens = commandLine.Split(' ');

            if (commandLineTokens.Length > 0)
            {
                this.Command = commandLineTokens[0].ToLowerInvariant();
                this.Parameters = commandLineTokens.GetParameters();
            }
        }

        public string Command { get; }

        public string[] Parameters { get; }

        public bool IsEmpty => string.IsNullOrWhiteSpace(this.Command);

        public bool IsHelp => this.Parameters.Any(p => p == "?" || p.ToLowerInvariant() == "help");
    }
}