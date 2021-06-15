using System.ComponentModel;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using Zen.Core.Services;
using static Spectre.Console.AnsiConsole;

namespace Zen.CLI.Commands.Misc
{
    public class GenerateAdviseCommand : AsyncCommand
    {
        private readonly MiscApiService miscApi;

        public GenerateAdviseCommand(MiscApiService miscApi)
        {
            this.miscApi = miscApi;
        }
        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            var advise = await Status()
                .Spinner(Spinner.Known.Dots6)
                .StartAsync("Generating...", async ctx =>
                {
                    return await miscApi.GenerateAdviseAsync();
                });
            WriteLine(advise);
            return 0;
        }
    }
}