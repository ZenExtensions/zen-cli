using Spectre.Console.Cli;
using Zen.CLI.Commands;
using Zen.CLI.Commands.Information;
using Zen.CLI.Commands.Misc;
using Zen.CLI.Infrastructure;

namespace Zen.CLI.Extensions
{
    public static class CommandAppHost
    {
        public static CommandApp WithStartup<TStartup>() where TStartup : BaseStartup, new()
        {
            TStartup startup = new TStartup();
            var services = startup.Configure();
            var registrar = new TypeRegistrar(services);
            var app = new CommandApp(registrar);
            return app;
        }

        public static ICommandConfigurator WithExample(this ICommandConfigurator builder, params string[] args)
        {
            return builder.WithExample(args);
        }

        public static IConfigurator ConfigureCommands(this IConfigurator configurator)
        {
            configurator.AddCommand<MainCommand>("info")
                .WithDescription("Displays cli logo");
            configurator.AddBranch("getinfo", options =>
            {
                options.SetDescription("Gets information about various things");
                options.AddCommand<GetIPCommand>("ip")
                    .WithDescription("Gets public ip of the system")
                    .WithAlias("myip")
                    .WithAlias("public-ip")
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
            return configurator;
        }
    }
}