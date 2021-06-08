using Spectre.Console;
using Spectre.Console.Cli;

namespace Zen.CLI.Commands
{
    public class MainCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            AnsiConsole.Console.WriteLine("Hello World, Zen is currently in development");
            return 0;
        }
    }
}