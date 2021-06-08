using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using TextCopy;
using Zen.Core.Serializers;

namespace Zen.CLI
{
    public class Startup : BaseStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.InjectClipboard();
            FlurlHttp.Configure(setting => 
            {
                setting.JsonSerializer = new SystemTextJsonSerialzier();
            });
        }
    }
}