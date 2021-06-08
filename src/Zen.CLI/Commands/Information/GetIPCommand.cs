using System.Threading;
using System.Threading.Tasks;
using TextCopy;
using Flurl.Http;
using System.Text.Json.Serialization;
using Zen.Core.Constants;
using System;
using Zen.CLI.Commands.Attributes;
using Spectre.Console.Cli;
using Spectre.Console;

namespace Zen.CLI.Commands.Information
{
    [Command(name: "getinfo ip", Description = "Used to get public ip. This command needs 'xsel' to be installed in linux based systems")]
    public class GetIPCommand : AsyncCommand
    {
        private readonly IClipboard clipboard;

        public GetIPCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }

        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            AnsiConsole.Console.WriteLine("Getting ip...");
            var response = await DefaultUrls.IFCONFIG_URL
                    .GetJsonAsync<IfConfigResponse>();
            try
            {

                await clipboard.SetTextAsync(text: response.IpAddr);
            }
            catch (Exception ex)
            {
                AnsiConsole.Console.WriteLine($"Unable to set clipboard: {ex.Message}");
            }
            AnsiConsole.Console.WriteLine($"Your public ip is {response.IpAddr}");
            return 0;
        }

        public class IfConfigResponse
        {
            [JsonPropertyName("ip_addr")]
            public string IpAddr { get; set; }
        }
    }
}