using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using Zen.Core.Services;
using static Spectre.Console.AnsiConsole;

namespace Zen.CLI.Commands
{
    public class GitIgnoreCommand : AsyncCommand<GitIgnoreCommand.GitIgnoreSetting>
    {
        public class GitIgnoreSetting : CommandSettings
        {
            [Description("Operating Systems, IDEs, or Programming Languages (use comma to separate values)")]
            [CommandOption("-q|--query")]
            public string Query { get; set; }

            [Description("Destination for storing gitignore file")]
            [DefaultValue("./")]
            [CommandOption("-d|--destination")]
            public string Destination { get; set; }

            public override ValidationResult Validate()
            {
                var dir = new DirectoryInfo(Destination);
                if(!dir.Exists)
                    return ValidationResult.Error(message: "Please enter a valid destination");
                return ValidationResult.Success();
            }
        }

        private readonly IGitIgnoreService gitignoreService;
        public GitIgnoreCommand(IGitIgnoreService gitignoreService)
        {
            this.gitignoreService = gitignoreService;
        }
        public override async Task<int> ExecuteAsync(CommandContext context, [NotNull] GitIgnoreSetting setting)
        {
            if(string.IsNullOrWhiteSpace(setting.Query))
                setting.Query = Ask<string>("Enter Operating Systems, IDEs, or Programming Languages (use comma to separate values)");
            List<string> types = new List<string>();
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots6)
                .StartAsync("Querying...", async ctx =>
                {
                    types = await gitignoreService.ListTypesAsync(setting.Query);
                });
            if(!types.Any())
            {
                Markup($"[yellow]No gitignore found against query [/]");
                return 0;
            }
            var choices = Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Select choices for which you want to generate gitignore file")
                    .Required()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a type, " + 
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoices(types)
            );
            string content = string.Empty;
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots6)
                .StartAsync("Generating file...", async ctx =>
                {
                    content = await gitignoreService.DownloadAsync(choices);
                });
            var fullPath = Path.Combine(setting.Destination, ".gitignore");
            await File.WriteAllTextAsync(fullPath, content, Encoding.UTF8);
            WriteLine($"Gitignore file successfully generated for {string.Join(", ", choices)}");
            return 0;
        }
    }
}