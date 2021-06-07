using System.Threading;
using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace Zen.CLI.Commands
{
    [Command]
    public class MainCommand : BaseCommand
    {
        public override async ValueTask ExecuteCommandAsync(IConsole console, CancellationToken cancellationToken)
        {
            await console.Output.WriteLineAsync("Hello World, Zen is currently in development");
        }
    }
}