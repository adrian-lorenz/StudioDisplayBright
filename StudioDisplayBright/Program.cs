using System.CommandLine;
using StudioDisplayBright.Commands;


namespace StudioDisplayBright
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand("Tool to manage Apple Studio Display brightness")
            {
                GetBrightnessCommand.Create(),
                SetBrightnessCommand.Create(),
                AdjustBrightnessCommand.Create("up"),
                AdjustBrightnessCommand.Create("down"),
                ListDisplaysCommand.Create() // Hier den neuen list-Befehl hinzufügen
            };

            await rootCommand.InvokeAsync(args);
        }
    }
}