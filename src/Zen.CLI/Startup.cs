using Autofac;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using TextCopy;
using Zen.CLI.Commands;
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

        public override void ConfigureContainer(ContainerBuilder container)
        {
            container.RegisterAssemblyTypes(typeof(BaseCommand).Assembly)
                .Where(a => a.IsSubclassOf(typeof(BaseCommand)));
        }
    }
}