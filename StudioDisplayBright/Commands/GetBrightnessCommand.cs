using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using StudioDisplayBright.HID;

namespace StudioDisplayBright.Commands
{
    public static class GetBrightnessCommand
    {
        public static Command Create()
        {
            var command = new Command("get", "Get the current brightness percentage")
            {
                new Option<string>("--serial", "Serial number of the display")
            };

            command.Handler = CommandHandler.Create<string>((serial) =>
            {
                var display =
                    ListDisplay.GetStudioDisplays().FirstOrDefault();


                if (display == null)
                {
                    Console.WriteLine("No display found.");
                    return;
                }

                var brightness = BrightnessController.GetBrightnessPercent(display);
                Console.WriteLine($"Current brightness: {brightness}%");
            });

            return command;
        }
    }
}