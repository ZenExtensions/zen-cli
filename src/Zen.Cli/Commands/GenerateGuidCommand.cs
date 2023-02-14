using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console.Cli;
using TextCopy;

namespace Zen.Cli.Commands
{
    public class GenerateGuidCommand : ZenAsyncCommand
    {
        private readonly IClipboard clipboard;
        public GenerateGuidCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }

        public override async Task OnExecuteAsync(CommandContext context, CancellationToken cancellationToken)
        {
            var guid = Guid.NewGuid();
            await clipboard.SetTextAsync(text: guid.ToString(), cancellationToken);
            Terminal.WriteInfo($"Generated Guid: {guid}");
        }
    }
}