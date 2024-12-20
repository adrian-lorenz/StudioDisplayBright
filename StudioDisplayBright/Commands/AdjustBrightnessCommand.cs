using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using StudioDisplayBright.HID;

namespace StudioDisplayBright.Commands
{
    public static class AdjustBrightnessCommand
    {
        public static Command Create(string direction)
        {
            var command = new Command(direction, $"{direction} brightness")
            {
                new Option<byte>("--step", () => 10, "Step size in percent"),
                new Option<string>("--serial", "Serial number of the display")
            };

            command.Handler = CommandHandler.Create<byte, string>((step, serial) =>
            {
                var display
                    = ListDisplay.GetStudioDisplays().FirstOrDefault();


                if (display == null)
                {
                    Console.WriteLine("No display found.");
                    return;
                }

                var currentBrightness = BrightnessController.GetBrightnessPercent(display);
                var newBrightness = direction == "up"
                    ? Math.Min(100, currentBrightness + step)
                    : Math.Max(0, currentBrightness - step);

                BrightnessController.SetBrightnessPercent(display, (byte)newBrightness);
                Console.WriteLine($"Brightness {direction} to {newBrightness}%");
            });

            return command;
        }
    }
}