using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Spectre.Console;
using Zen.Core.Constants;
using Zen.Core.Extensions;

namespace Zen.Cli.Commands.Information
{
    public class EndOfLifeCommand : ZenCommand<EndOfLifeCommandSetting>
    {
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var tools = await Terminal.NewStatus()
                .StartAsync("Checking...", async ctx =>
                {
                    var tools = await DefaultUrls.END_OF_LIFE_API
                        .AppendPathSegment("api")
                        .AppendPathSegment("all.json")
                        .GetJsonAsync<string[]>();
                    if(!string.IsNullOrWhiteSpace(Setting.Query))
                    {
                        tools = tools.Where(item => item.Contains(Setting.Query, System.StringComparison.InvariantCultureIgnoreCase))
                            .ToArray();
                    }
                    return tools;
                });
            if(!tools.Any())
            {
                Terminal.WriteWarning("No tools found with specified prefix");
                return;
            }
            string tool = null;
            if(tools.Length == 1)
            {
                tool = tools.First();
                Terminal.WriteLine($"Only one result found with matching critera ({tool})");
            }
            else
            {
                tool = await Terminal.SingleSelectionPromptAsync("Select the tool", tools, cancellationToken);
            }
            var data = await Terminal.NewStatus()
                .StartAsync("Checking...", ctx =>
                {
                    return DefaultUrls.END_OF_LIFE_API
                        .AppendPathSegment("api")
                        .AppendPathSegment($"{tool}.json")
                        .GetJsonAsync<EndOfLifeObject[]>();
                });
            var table = Terminal.NewTable($"{tool} EOL");
            table.AddColumns("Cycle","Release","End Of Life","Support","Latest Version","Is LTS");
            Terminal.MarkupLine($"--[bold italic]{tool}[/]--");
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
            Terminal.Write(table);
            Terminal.MarkupLine("[red]Red[/] means it has reached end of life or is no longer supported");
        }
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