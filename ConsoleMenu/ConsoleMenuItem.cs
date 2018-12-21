namespace ConsoleMenu
{
    using System;

    public class ConsoleMenuItem
    {
        private const string DefaultHelpText = "There is no help text associated with this command.";

        public ConsoleMenuItem(string command, string description, Action<string[]> callback) : this(command, description, DefaultHelpText, callback, 0)
        {

        }

        public ConsoleMenuItem(string command, string description, Action<string[]> callback, int expectedNumberOfParameters) : this(command, description, DefaultHelpText, callback, expectedNumberOfParameters)
        {

        }

        public ConsoleMenuItem(string command, string description, string helpText, Action<string[]> callback, int expectedNumberOfParameters)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("Command cannot be null, empty or whitespace.", nameof(command));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Command cannot be null, empty or whitespace.", nameof(description));
            }

            this.Command = command.ToLowerInvariant();
            this.Description = description;
            this.HelpText = helpText;
            this.Callback = callback ?? throw new ArgumentNullException(nameof(callback));
            this.ExpectedNumberOfParameters = expectedNumberOfParameters;
        }

        public string Command { get; }

        public string Description { get; }

        public string HelpText { get; }

        public Action<string[]> Callback { get; }

        public int ExpectedNumberOfParameters { get; }

        internal ConsoleCommandValidationStatus ValidateCommand(ConsoleCommand command)
        {
            if (command.IsHelp)
            {
                return ConsoleCommandValidationStatus.ShowHelpText;
            }

            if (command.Parameters != null && this.ExpectedNumberOfParameters > 0
                                           && (this.ExpectedNumberOfParameters != command.Parameters.Length))
            {
                return ConsoleCommandValidationStatus.NotEnoughParameters;
            }

            return ConsoleCommandValidationStatus.Ok;
        }
    }
}