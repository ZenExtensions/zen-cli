using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace Zen.Cli.Commands.Generator
{
    public class GenerateGuidCommand : ZenCommand
    {
        private readonly IClipboard clipboard;
        public GenerateGuidCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var guid = Guid.NewGuid();
            await clipboard.SetTextAsync(text: guid.ToString());
            Terminal.WriteInfo($"Generated GUID: {guid}");
        }
    }
}