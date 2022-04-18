using System.Threading.Tasks;
using Zen.Core.SpectreConsole;
using Zen.Cli;

await SpectreConsoleHost
    .WithStartup<Startup>(args)
    .UseConfigurator<Startup>()
    .RunAsync(args);