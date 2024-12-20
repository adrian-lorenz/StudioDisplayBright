using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using StudioDisplayBright.HID;

namespace StudioDisplayBright.Commands
{
    public static class SetBrightnessCommand
    {
        public static Command Create()
        {
            var command = new Command("set", "Set the brightness percentage")
            {
                new Option<byte>("--brightness", "Brightness percentage (0-100)") { IsRequired = true },

            };

            command.Handler = CommandHandler.Create<byte, string>((brightness, serial) =>
            {
                var display =
                    ListDisplay.GetStudioDisplays();

                foreach (var mon in display)
                {
                    BrightnessController.SetBrightnessPercent(mon, brightness);
                    Console.WriteLine($"Brightness set to {brightness}%");
                }


            });

            return command;
        }
    }
}