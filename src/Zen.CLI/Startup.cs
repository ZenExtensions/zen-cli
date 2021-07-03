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
using Zen.Core.Services.Anime;
using Zen.SpectreConsole.Extensions;

namespace Zen.CLI
{
    public class Startup : BaseStartup
    {
        public override void ConfigureCommandApp(in IConfigurator configurator)
        {
            configurator.SetApplicationName("zen");
            configurator.CaseSensitivity(CaseSensitivity.None);
            configurator.AddCommand<MainCommand>("logo")
                .WithDescription("Displays cli logo");
            configurator.AddCommand<GenerateGuidCommand>("uuidgen")
                    .WithDescription("Generates Guid and copies to clipboard")
                    .WithAliases("guid", "uuid","guidgen")
                    .WithExample("uuidgen")
                    .WithExample("guidgen")
                    .WithExample("guid")
                    .WithExample("uuid");
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

            configurator.AddCommand<GitIgnoreCommand>("gitignore")
                .WithDescription("Utility for gitignore.io")
                .WithAlias("giio")
                .WithExample("gitignore")
                .WithExample("giio")
                .WithExample("giio", "-q","visual")
                .WithExample("giio", "--query","visual","--destination", "/home/user/projects/my-app/")
                .WithExample("giio", "--query","visual");

            configurator.AddBranch("misc", options => 
            {
                options.SetDescription("Miscalaneous commands");
                options.AddCommand<GenerateMD5Command>("md5")
                    .WithDescription("Generates MD5 hash value")
                    .WithExample("misc", "md5", "\"Hello World\"");
                options.AddCommand<AnimeQuoteCommand>("anime-quote")
                    .WithDescription("Displays a random anime quote")
                    .IsHidden()
                    .WithExample("misc", "anime-quote")
                    .WithExample("misc", "anime-quote", "-q", "naruto, one piece");
                
                options.AddCommand<GenerateInsultCommand>("insult")
                    .WithDescription("Displays a random insult")
                    .WithAliases("generate-insult","insult-me")
                    .IsHidden()
                    .WithExample("misc", "insult")
                    .WithExample("misc", "generate-insult");
                options.AddCommand<GenerateAdviseCommand>("advise")
                    .WithDescription("Displays a random advise")
                    .WithAliases("generate-advise","advise-me")
                    .IsHidden()
                    .WithExample("misc", "advise")
                    .WithExample("misc", "generate-advise");
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
            services.AddSingleton<IAnimeChanService, AnimeChanService>();
            services.AddSingleton<MiscApiService>();
        }
    }
}