using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextCopy;
using Zen.Cli.Commands.Generator;
using Zen.Cli.Commands.Information;
using Zen.Core.Serializers;

namespace Zen.Cli
{
    public delegate void CommandGroup(string name, IConfigurator<CommandSettings> configurator);
    public class Startup : BaseStartup, ISpectreConfiguration
    {
        public void ConfigureCommandApp(in IConfigurator configurator)
        {
            configurator.SetApplicationName("zen");
            configurator.CaseSensitivity(CaseSensitivity.None);
            configurator.ValidateExamples();
            var branches = new IZenCommandGroup[]
            {
                new InformationCommandGroup(),
                new GeneratorCommandGroup()
            };

            foreach (var branch in branches)
            {
                configurator.AddBranch<ZenCommandSetting>(branch.Name,branch.ConfigureCommandApp);
            }
        }

        public override void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.InjectClipboard();
            FlurlHttp.Configure(setting =>
            {
                setting.JsonSerializer = new SystemTextJsonSerialzier();
            });
        }
    }
}