using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;
using Zen.Core.Services;

namespace Zen.CLI.Commands.Git
{
    public class GitSearchCommand : AsyncCommand<GitSearchCommand.GitSearchCommandSettings>
    {
        public override Task<int> ExecuteAsync(CommandContext context, GitSearchCommandSettings settings)
        {
            var directory = new DirectoryInfo(settings.Directory);
            if(!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory {directory.FullName} not found");
            }
            var tree = AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .Start<Tree>("Building git directory context...", ctx => 
                {
                    var info = new GitDirectoryInfo(directory);
                    ctx.Status("Building tree...");
                    return BuildTreeFromDirectoryStructure(info);
                });
            AnsiConsole.Render(tree);
            return Task.FromResult(0);
        }

        Tree BuildTreeFromDirectoryStructure(GitDirectoryInfo directory)
        {
            if(directory.IsGitDirectory)
                return new Tree($"[blue]{directory.Name}[/]");
            Tree root = new Tree(directory.Name);
            foreach (var dir in directory.SubDirectories)
            {
                var node = BuildTreeFromDirectoryStructure(dir);
                root.AddNode(node);
            }
            return root;
        }

        public class GitSearchCommandSettings : CommandSettings
        {
            [Description("Directory to search git repositories for")]
            [CommandArgument(0, "<directory>")]
            public string Directory { get; set; }
        }
    }
}