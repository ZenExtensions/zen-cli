using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using TextCopy;
using Zen.CLI.Commands;
using Zen.CLI.Commands.Information;
using Zen.CLI.Commands.Misc;
using Zen.Core.Serializers;
using Zen.Core.Services;
using Zen.SpectreConsole.Extensions;

namespace Zen.CLI
{
    public class Startup : BaseStartup
    {
        public override void ConfigureCommandApp(in IConfigurator configurator)
        {
            configurator.SetApplicationName("zen");
            configurator.CaseSensitivity(CaseSensitivity.None);
            configurator.AddCommand<MainCommand>("info")
                .WithDescription("Displays cli logo");
            configurator.AddBranch("getinfo", options =>
            {
                options.SetDescription("Gets information about various things");
                options.AddCommand<GetIPCommand>("ip")
                    .WithDescription("Gets public ip of the system")
                    .WithAliases("myip", "public-ip")
                    .WithExample("getinfo","ip")
                    .WithExample("getinfo","myip")
                    .WithExample("getinfo","public-ip");
                options.AddCommand<GetNetworkInterfacesCommand>("nic")
                    .WithDescription("Gets list of network interfaces")
                    .WithAlias("network-interfaces")
                    .WithExample("getinfo", "net-interfaces")
                    .WithExample("getinfo", "nic");
            });

            configurator.AddBranch("misc", options => 
            {
                options.SetDescription("Miscalaneous commands");
                options.AddCommand<GenerateMD5Command>("md5")
                    .WithDescription("Generates MD5 hash value")
                    .WithExample("misc", "md5", "\"Hello World\"");
                options.AddCommand<GitIgnoreCommand>("gitignore")
                    .WithDescription("Utility for gitignore.io")
                    .WithAlias("giio")
                    .WithExample("misc", "gitignore")
                    .WithExample("misc", "giio")
                    .WithExample("misc", "giio", "-q","visual")
                    .WithExample("misc", "giio", "--query","visual","--destination", "/home/user/projects/my-app/")
                    .WithExample("misc", "giio", "--query","visual");
            });
        }

        public override void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
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