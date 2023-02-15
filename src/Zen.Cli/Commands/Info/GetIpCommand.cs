using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Refit;
using TextCopy;

namespace Zen.Cli.Commands;
public class GetIpCommand : ZenAsyncCommand
{
    private readonly IClipboard clipboard;
    public GetIpCommand(IClipboard clipboard)
    {
        this.clipboard = clipboard;
    }
    public override async Task OnExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        await Terminal.StatusAsync("Getting IP address", async _ =>
        {
            var api = RestService.For<IIpApi>("https://ifconfig.me");
            var response = await api.GetIpAsync(cancellationToken);
            Terminal.WriteInfo($"IP address: {response.IpAddr}");
            await clipboard.SetTextAsync(response.IpAddr);
        });
    }
}

#region Apis
public interface IIpApi
{
    class IfConfigResponse
    {
        [JsonPropertyName("ip_addr")]
        public string IpAddr { get; set; } = default!;
    }
    [Get("/all.json")]
    Task<IfConfigResponse> GetIpAsync(CancellationToken cancellationToken);
}
#endregion