using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using Zen.Core.Services;
using static Spectre.Console.AnsiConsole;

namespace Zen.CLI.Commands.Misc
{
    public class GenerateInsultCommand : AsyncCommand
    {
        private readonly MiscApiService miscApi;

        public GenerateInsultCommand(MiscApiService miscApi)
        {
            this.miscApi = miscApi;
        }
        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            var insult = await Status()
                .Spinner(Spinner.Known.Dots)
                .StartAsync("Generating...", async ctx =>
                {
                    return await miscApi.GenerateInsultAsync();
                });
            WriteLine(insult);
            return 0;
        }
    }
}