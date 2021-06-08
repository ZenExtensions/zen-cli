using System.Threading;
using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace Zen.CLI.Commands.Information
{
    [Command("getinfo", Description = "This command is used to get information about various things")]
    public class GetInfoCommand : BaseCommand
    {
        public override ValueTask ExecuteCommandAsync(IConsole console, CancellationToken cancellationToken)
        {
            return ShowCommandHelpAsync();
        }
    }
}