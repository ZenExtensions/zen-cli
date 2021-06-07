using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using Refit;
using TextCopy;
using Zen.CLI.Apis;
using Zen.CLI.Constants;

namespace Zen.CLI.Commands
{
    [Command(name: "my-ip")]
    public class GetIPCommand : BaseCommand
    {
        private readonly IClipboard clipboard;

        public GetIPCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }
        public override async ValueTask ExecuteCommandAsync(IConsole console, CancellationToken cancellationToken)
        {
            var api = RestService.For<IIfConfigApi>(DefaultUrls.IFCONFIG_URL);
            await console.Output.WriteLineAsync("Getting ip...");
            var response = await api.GetInfoAsync();
            if(response.StatusCode != HttpStatusCode.OK)
                throw new CommandException(message: $"Error {response.StatusCode} when getting ip: {response.Error.Content}");
            
            await clipboard.SetTextAsync(text: response.Content.IpAddr, cancellationToken);
            await console.Output.WriteLineAsync($"IP is {response.Content.IpAddr}");
        }
    }
}