using Spectre.Console;
using Spectre.Console.Cli;

namespace Zen.Cli.Commands
{
    public class MainCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            AnsiConsole.Write(
                new FigletText("ZEN CLI")
                    .LeftAligned()
                    .Color(Color.Aqua)
            );
            return 0;
        }
    }
}