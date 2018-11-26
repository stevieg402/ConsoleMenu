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
                new ConsoleMenuItem(ConsoleMenuReservedCommands.DisplayCommand, "Display menu", () => this.Display()));
            this.consoleMenuItems.Add(
                ConsoleMenuReservedCommands.ClearScreenCommand,
                new ConsoleMenuItem(
                    ConsoleMenuReservedCommands.ClearScreenCommand,
                    "Clear screen",
                    () => this.ClearScreen()));
            this.consoleMenuItems.Add(
                ConsoleMenuReservedCommands.ExitCommand,
                new ConsoleMenuItem(ConsoleMenuReservedCommands.ExitCommand, "Exit", () => { this.Finished = true; }));
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

        private void HandleCommand()
        {
            Console.WriteLine();
            Console.Write("Select a menu option: ");
            var command = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(command))
            {
                Console.WriteLine("Please select a menu option.");
            }
            else
            {
                var commandKey = command.ToLowerInvariant();
                if (this.consoleMenuItems.ContainsKey(commandKey))
                {
                    this.consoleMenuItems[commandKey].Callback();
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