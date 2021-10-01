using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Spectre.Console;
using Spectre.Console.Cli;
using Zen.Core.Constants;

namespace Zen.CLI.Commands.Misc
{
    public class EndOfLifeCommand : AsyncCommand<EndOfLifeCommand.EndOfLifeCommandSetting>
    {
        public override async Task<int> ExecuteAsync(CommandContext context, EndOfLifeCommandSetting settings)
        {
            var tools = await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots6)
                .StartAsync("Checking...", ctx =>
                {
                    return DefaultUrls.END_OF_LIFE_API
                        .AppendPathSegment("api")
                        .AppendPathSegment("all.json")
                        .GetJsonAsync<string[]>();
                });
            if(!string.IsNullOrWhiteSpace(settings.Prefix))
            {
                tools = tools.Where(item => item.Contains(settings.Prefix, System.StringComparison.InvariantCultureIgnoreCase))
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
                .Spinner(Spinner.Known.Dots6)
                .StartAsync("Checking...", ctx =>
                {
                    return DefaultUrls.END_OF_LIFE_API
                        .AppendPathSegment("api")
                        .AppendPathSegment($"{tool}.json")
                        .GetJsonAsync<EndOfLifeObject[]>();
                });
            var table = new Table();
            table.AddColumns("Cycle","Release","End Of Life","Latest Version","Is LTS");

            AnsiConsole.MarkupLine("[red]Red[/] color means its no longer supported");
            foreach (var item in data)
            {
                var eolDate = DateTime.Parse(item.Eol);
                string color = (eolDate < DateTime.Today) ? "red" : "blue";
                table.AddRow(new []
                {
                    $"[{color}]{item.Cycle}[/]",
                    $"[{color}]{item.Release}[/]",
                    $"[{color}]{item.Eol}[/]",
                    $"[{color}]{item.Latest}[/]",
                    $"[{color}]{item.Lts}[/]"
                });
            }
            AnsiConsole.Render(table);


            return 0;
        }

        public class EndOfLifeCommandSetting : CommandSettings
        {
            [CommandOption("--prefix")]
            public string Prefix { get; set; }
        }

        class EndOfLifeObject
        {
            public string Cycle { get; set; }
            public string Release { get; set; }
            public string Eol { get; set; }
            public string Latest { get; set; }
            public bool Lts { get; set; }
        }
    }
}