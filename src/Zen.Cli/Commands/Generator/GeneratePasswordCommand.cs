using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using System.Text.Json.Serialization;
using TextCopy;

namespace Zen.Cli.Commands.Generator
{
    public class GeneratePasswordCommand : ZenCommand<GeneratePasswordCommandSetting>
    {
        private readonly IClipboard clipboard;

        public GeneratePasswordCommand(IClipboard clipboard)
        {
            this.clipboard = clipboard;
        }
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var password = await Terminal.NewStatus()
                .StartAsync("Generating password...", async ctx => 
                {
                    var response = await DefaultUrls.GENERATE_PASSWORD_API
                        .SetQueryParam("num", true)
                        .SetQueryParam("char", true)
                        .SetQueryParam("caps", true)
                        .SetQueryParam("len", Setting.Length)
                        .GetJsonAsync<GeneratePasswordResponse>();
                    return response.Data;
                });
            await clipboard.SetTextAsync(password, cancellationToken);
            Terminal.WriteInfo("Generated password and copied to the clipboard");
        }
    }
    class GeneratePasswordResponse
    {
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}