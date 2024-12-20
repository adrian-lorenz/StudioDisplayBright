using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using StudioDisplayBright.HID;

namespace StudioDisplayBright.Commands
{
    public static class ListDisplaysCommand
    {
        public static Command Create()
        {
            var command = new Command("list", "List all connected Apple Studio Displays with InterfaceNumber 7");

            command.Handler = CommandHandler.Create(() =>
            {
                var displays = ListDisplay.GetStudioDisplays();
                if (!displays.Any())
                {
                    Console.WriteLine("No Apple Studio Displays with InterfaceNumber 7 found.");
                    return;
                }

                Console.WriteLine("Available Apple Studio Displays with InterfaceNumber 7:");
                foreach (var display in displays)
                {
                    Console.WriteLine($"- Path: {display.DevicePath}");

                    Console.WriteLine("  Serial: (unknown)");

                }
            });

            return command;
        }
    }
}