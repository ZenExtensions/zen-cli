using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextCopy;
using Zen.CLI.Commands;

namespace Zen.CLI
{
    public class Startup : BaseStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainCommand>();
            services.AddSingleton<GetIPCommand>();
            services.InjectClipboard();
        }
    }
}