namespace ConsoleMenu
{
    using System;

    public class ConsoleMenuItem
    {
        public ConsoleMenuItem(string command, string description, Action callback)
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
            this.Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public string Command { get; }

        public string Description { get; }

        public Action Callback { get; }
    }
}