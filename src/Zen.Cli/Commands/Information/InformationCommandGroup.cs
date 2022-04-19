using Spectre.Console.Cli;

namespace Zen.Cli.Commands.Information
{
    public class InformationCommandGroup : IZenCommandGroup
    {
        public string Name => "getinfo";

        public void ConfigureCommandApp(IConfigurator<ZenCommandSetting> options)
        {
            options.SetDescription("Get information about things.");
            options.AddCommand<EndOfLifeCommand>("eol")
                    .WithDescription("Gets information about end of life for a tool or product")
                    .WithAliases("getinfo", "end-of-life", "endoflife")
                    .WithExample("getinfo", "eol")
                    .WithExample("getinfo", "eol", "--query", "dotnet");
            options.AddCommand<GetIpCommand>("ip")
                    .WithDescription("Gets public ip of the system")
                    .WithAliases("myip", "public-ip")
                    .WithExample("getinfo", "ip")
                    .WithExample("getinfo", "myip")
                    .WithExample("getinfo", "public-ip");
        }
    }
}