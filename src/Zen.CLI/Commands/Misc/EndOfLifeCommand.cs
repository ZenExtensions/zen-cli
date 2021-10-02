using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Spectre.Console;
using Spectre.Console.Cli;
using Zen.Core.Constants;
using Zen.Core.Extensions;

namespace Zen.CLI.Commands.Misc
{
    public class EndOfLifeCommand : AsyncCommand<EndOfLifeCommand.EndOfLifeCommandSetting>
    {
        public override async Task<int> ExecuteAsync(CommandContext context, EndOfLifeCommandSetting settings)
        {
            var tools = await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .StartAsync("Checking...", ctx =>
                {
                    return DefaultUrls.END_OF_LIFE_API
                        .AppendPathSegment("api")
                        .AppendPathSegment("all.json")
                        .GetJsonAsync<string[]>();
                });
            if(!string.IsNullOrWhiteSpace(settings.Query))
            {
                tools = tools.Where(item => item.Contains(settings.Query, System.StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();
            }
            if(!tools.Any())
            {
                AnsiConsole.WriteLine("No tools found with specified prefix");
                return 0;
            }
            string tool = null;
            if(tools.Length == 1)
            {
                AnsiConsole.WriteLine("Only one result found with matching critera");
                tool = tools.First();
            }
            else
            {
                tool = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                            .Title("Which tool do you want to look up?")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more anime)[/]")
                            .AddChoices(tools)
                   );
            }
            var data = await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .StartAsync("Checking...", ctx =>
                {
                    return DefaultUrls.END_OF_LIFE_API
                        .AppendPathSegment("api")
                        .AppendPathSegment($"{tool}.json")
                        .GetJsonAsync<EndOfLifeObject[]>();
                });
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumns("Cycle","Release","End Of Life","Support","Latest Version","Is LTS");
            AnsiConsole.MarkupLine($"--[bold italic]{tool}[/]--");
            foreach (var item in data)
            {
                var color = item.HasReachedEndOfLife ? "red": "blue";
                table.AddRow(new []
                {
                    $"[{color}]{item.Cycle}[/]",
                    $"[{color}]{item.Release?.GetDateTime().GetDateText()}[/]",
                    $"[{color}]{item.Eol?.GetPossibleYesNoValue()}[/]",
                    $"[{color}]{item.Support?.GetPossibleYesNoValue()}[/]",
                    $"[{color}]{item.Latest}[/]",
                    $"[{color}]{item.Lts.GetBooleanText()}[/]"
                });
            }
            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine("[red]Red[/] means it has reached end of life or is no longer supported");


            return 0;
        }

        public class EndOfLifeCommandSetting : CommandSettings
        {
            [CommandOption("-q|--query")]
            public string Query { get; set; }
        }

        class EndOfLifeObject
        {
            public string Cycle { get; set; }
            public string Release { get; set; }
            public string Eol { get; set; }
            public string Latest { get; set; }
            public string Support { get; set; }
            public bool Lts { get; set; }

            public bool HasReachedEndOfLife
            {
                get
                {
                    if(bool.TryParse(Support, out var val))
                    {
                        return !val;
                    }
                    var date = Eol.GetDateTime();
                    if(date is null)
                        return false;
                    return date.Value < DateTime.Today;
                }
            }
        }
    }
}