using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using Zen.Core.Services.Anime;
using static Spectre.Console.AnsiConsole;

namespace Zen.CLI.Commands.Misc
{
    public class AnimeQuoteCommand : AsyncCommand<AnimeQuoteCommand.AnimeQuoteCommandSetting>
    {
        private readonly IAnimeChanService animeService;

        public AnimeQuoteCommand(IAnimeChanService animeService)
        {
            this.animeService = animeService;
        }
        public override async Task<int> ExecuteAsync(CommandContext context, AnimeQuoteCommandSetting settings)
        {
            string anime = null;
            if (!string.IsNullOrWhiteSpace(settings.Query))
            {
                var animes = await AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots6)
                    .StartAsync("Querying...", async ctx =>
                    {
                        return await animeService.GetAnimeListAsync(query: settings.Query);
                    });
                if(!animes.Any())
                {
                    
                    Markup($"[yellow]No anime found against query [/]");
                    return 0;
                }
                if(animes.Count == 1)
                {
                    anime = animes.First();
                }
                else
                {
                    anime = Prompt(
                        new SelectionPrompt<string>()
                            .Title("Which anime do you want quote from?")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more anime)[/]")
                            .AddChoices(animes)
                    );
                }
            }
            var spinnerMessage = anime is null ? "Getting quote..." : $"Getting quote for {anime}..."; 
            var quote = await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots6)
                .StartAsync(spinnerMessage, async ctx =>
                {
                    return await animeService.GetRandomQuoteAsync(anime: anime);

                });
            WriteLine($"{quote.Character}: {quote.Quote}\n({quote.Anime})");
            return 0;
        }

        public class AnimeQuoteCommandSetting : CommandSettings
        {
            [Description("Search for anime (use comma to separate values)")]
            [CommandOption("-q|--query")]
            public string Query { get; set; }
        }
    }
}