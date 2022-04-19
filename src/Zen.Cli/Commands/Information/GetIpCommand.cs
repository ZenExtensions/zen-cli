using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using TextCopy;
using Zen.Core.Constants;

namespace Zen.Cli.Commands.Information
{
    public class GetIpCommand : ZenCommand
    {
        private readonly IClipboard clipboard;

        public GetIpCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Terminal.NewStatus()
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
                        Terminal.WriteWarning($"Unable to set clipboard: {ex.Message}");
                    }
                    Terminal.WriteSuccess($"Your public ip is {response.IpAddr}");
                });
        }
    }
    class IfConfigResponse
    {
        [JsonPropertyName("ip_addr")]
        public string IpAddr { get; set; }
    }
}