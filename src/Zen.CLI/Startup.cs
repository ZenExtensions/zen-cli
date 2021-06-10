using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using TextCopy;
using Zen.Core.Configuration;
using Zen.Core.Serializers;
using Zen.Core.Services;

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
            services.AddSingleton<IGitIgnoreService, GitIgnoreService>();
        }
    }
}