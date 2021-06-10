using Spectre.Console.Cli;
using Zen.CLI.Commands;
using Zen.CLI.Commands.Information;
using Zen.CLI.Commands.Misc;
using Zen.CLI.Infrastructure;

namespace Zen.CLI.Extensions
{
    public static class CommandAppHost
    {
        public static CommandApp<MainCommand> WithStartup<TStartup>() where TStartup : BaseStartup, new()
        {
            TStartup startup = new TStartup();
            var services = startup.Configure();
            var registrar = new TypeRegistrar(services);
            var app = new CommandApp<MainCommand>(registrar);

            return app;
        }

        public static IConfigurator ConfigureCommands(this IConfigurator configurator)
        {
            configurator.AddBranch("getinfo", options =>
            {
                options.AddCommand<GetIPCommand>("ip")
                    .WithDescription("Gets public ip of the system")
                    .WithAlias("myip")
                    .WithAlias("public-ip")
                    .WithExample(new[]
                    {
                        "getinfo",
                        "ip"
                    });
                options.AddCommand<GetNetworkInterfacesCommand>("nic")
                    .WithDescription("Gets list of network interfaces")
                    .WithAlias("network-interfaces")
                    .WithExample(new[]
                    {
                        "getinfo",
                        "net-interfaces"
                    });
            });

            configurator.AddBranch("misc", options => 
            {
                options.AddCommand<GenerateMD5Command>("md5")
                    .WithDescription("Generates MD5 hash value")
                    .WithExample(new []
                    {
                        "misc",
                        "md5",
                        "\"Hello World\""
                    });
            });
            return configurator;
        }
    }
}