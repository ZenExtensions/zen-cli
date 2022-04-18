using Spectre.Console.Cli;

namespace Zen.Core.SpectreConsole
{
    public interface ISpectreConfiguration
    {
        void ConfigureCommandApp(in IConfigurator configurator);
    }
}