using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using TextCopy;

namespace Zen.CLI.Commands
{
    public class GenerateGuidCommand : AsyncCommand
    {
        private readonly IClipboard clipboard;
        public GenerateGuidCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }

        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            var guid = Guid.NewGuid();
            await clipboard.SetTextAsync(text: guid.ToString());
            AnsiConsole.WriteLine($"Generated GUID: {guid}");
            return 0;
        }
    }
}