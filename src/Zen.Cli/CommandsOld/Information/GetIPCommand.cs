using System.Threading.Tasks;
using TextCopy;
using Flurl.Http;
using System.Text.Json.Serialization;
using Zen.Core.Constants;
using System;
using Spectre.Console.Cli;
using Spectre.Console;

namespace Zen.Cli.Commands.Information
{
    public class GetIPCommand : AsyncCommand
    {
        private readonly IClipboard clipboard;

        public GetIPCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }

        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .StartAsync("Getting ip...", async ctx =>
                {
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
                    ctx.SpinnerStyle(Style.Parse("green"));
                    AnsiConsole.WriteLine($"Your public ip is {response.IpAddr}");
                });
            return 0;
        }

        public class IfConfigResponse
        {
            [JsonPropertyName("ip_addr")]
            public string IpAddr { get; set; }
        }
    }
}