namespace ConsoleMenu
{
    using System;
    using System.Collections.Generic;

    public class ConsoleMenu
    {
        private readonly Dictionary<string, ConsoleMenuItem> consoleMenuItems;

        private readonly string title;

        public ConsoleMenu(string title, params ConsoleMenuItem[] consoleMenuItems)
        {
            this.title = title;

            this.consoleMenuItems = new Dictionary<string, ConsoleMenuItem>();
            foreach (var consoleMenuItem in consoleMenuItems)
            {
                if (ConsoleMenuReservedCommands.IsReservedCommand(consoleMenuItem.Command))
                {
                    throw new ConsoleMenuReservedCommandException(
                        $"The command, {consoleMenuItem.Command}, is reserved for use by ConsoleMenu.");
                }

                this.consoleMenuItems.Add(consoleMenuItem.Command, consoleMenuItem);
            }

            this.consoleMenuItems.Add(
                ConsoleMenuReservedCommands.DisplayCommand,
                new ConsoleMenuItem(ConsoleMenuReservedCommands.DisplayCommand, "Display menu", x => this.Display()));
            this.consoleMenuItems.Add(
                ConsoleMenuReservedCommands.ClearScreenCommand,
                new ConsoleMenuItem(
                    ConsoleMenuReservedCommands.ClearScreenCommand,
                    "Clear screen",
                    x => this.ClearScreen()));
            this.consoleMenuItems.Add(
                ConsoleMenuReservedCommands.ExitCommand,
                new ConsoleMenuItem(ConsoleMenuReservedCommands.ExitCommand, "Exit", x => { this.Finished = true; }));
        }

        private bool Finished { get; set; }

        private void Display()
        {
            if (!string.IsNullOrWhiteSpace(this.title))
            {
                Console.WriteLine(this.title);
                Console.WriteLine(new string('-', this.title.Length));
            }

            foreach (var consoleMenuItem in this.consoleMenuItems)
            {
                Console.WriteLine($"{consoleMenuItem.Value.Command}\t{consoleMenuItem.Value.Description}");
            }
        }

        public void Run()
        {
            this.Display();

            while (!this.Finished)
            {
                this.HandleCommand();
            }
        }

        private ConsoleMenuItem GetConsoleMenuItem(string command)
        {
            if (this.consoleMenuItems.ContainsKey(command))
            {
                return this.consoleMenuItems[command];
            }

            return null;
        }

        private void HandleCommand()
        {
            Console.WriteLine();
            Console.Write("Select a menu option: ");

            var commandLine = Console.ReadLine();

            var consoleCommand = new ConsoleCommand(commandLine);
            if (consoleCommand.IsEmpty)
            {
                Console.WriteLine("Please select a menu option.");
            }
            else
            {
                var consoleMenuItem = this.GetConsoleMenuItem(consoleCommand.Command);

                if (consoleMenuItem != null)
                {
                    switch (consoleMenuItem.ValidateCommand(consoleCommand))
                    {
                        case ConsoleCommandValidationStatus.NotEnoughParameters:
                            Console.WriteLine(
                                $"The number of parameters supplied does not match the number of parameters expected.{Environment.NewLine}Use the help or ? parameter to get help on the command.");
                            break;
                        case ConsoleCommandValidationStatus.ShowHelpText:
                            Console.WriteLine(consoleMenuItem.HelpText);
                            break;
                        case ConsoleCommandValidationStatus.Ok:
                            try
                            {
                                this.consoleMenuItems[consoleCommand.Command].Callback(consoleCommand.Parameters);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("An error occurred whilst processing your command:");
                                Console.WriteLine(e.Message);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a valid menu option.");
                }
            }
        }

        private void ClearScreen()
        {
            Console.Clear();
            this.Display();
        }
    }
}