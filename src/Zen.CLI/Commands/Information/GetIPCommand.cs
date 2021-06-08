using System.Threading;
using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using TextCopy;
using Flurl.Http;
using System.Text.Json.Serialization;
using Zen.Core.Constants;
using System;

namespace Zen.CLI.Commands.Information
{
    [Command(name: "getinfo ip", Description = "Used to get public ip. This command needs 'xsel' to be installed in linux based systems")]
    public class GetIPCommand : BaseCommand
    {
        private readonly IClipboard clipboard;

        public GetIPCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }
        public override async ValueTask ExecuteCommandAsync(IConsole console, CancellationToken cancellationToken)
        {
            await console.Output.WriteLineAsync("Getting ip...");
            try
            {
                var response = await DefaultUrls.IFCONFIG_URL
                    .GetJsonAsync<IfConfigResponse>(cancellationToken);
                try
                {
                    
                    await clipboard.SetTextAsync(text: response.IpAddr, cancellationToken);   
                }
                catch (Exception ex)
                {
                    await console.Error.WriteLineAsync($"Unable to set clipboard: {ex.Message}");
                }
                await console.Output.WriteLineAsync($"Your public ip is {response.IpAddr}");
            }
            catch (FlurlHttpException ex)
            {
                throw new CommandException(message: $"Error {ex.StatusCode} when getting ip: {ex.Message}");
            }
        }

        public class IfConfigResponse
        {
            [JsonPropertyName("ip_addr")]
            public string IpAddr { get; set; }
        }
    }
}