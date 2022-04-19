using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Zen.Core.SpectreConsole
{
    public class SpectreConsoleHost<TCommand>
        where TCommand : class, ICommand
    {
        private readonly CommandApp<TCommand> app;
        public SpectreConsoleHost(CommandApp<TCommand> app)
        {
            this.app = app;
        }
        public static SpectreConsoleHost<TCommand> WithStartup<TStartup>(string[] args = default) where TStartup : BaseStartup, new()
        {
            var services= StartupUtil.GetServiceCollectionFrom<TStartup>(args);
            var registrar = new TypeRegistrar(services);
            var app = new CommandApp<TCommand>(registrar);
            return new SpectreConsoleHost<TCommand>(app);
        }

        public SpectreConsoleHost<TCommand> UseConfigurator<TConfigurator>() where TConfigurator : class, ISpectreConfiguration, new()
        {
            TConfigurator configurator = new TConfigurator();
            app.Configure(options => 
            {
                configurator.ConfigureCommandApp(in options);
            });
            return this;
        }

        public Task<int> RunAsync(string[] args)
        {
            return app.RunAsync(args);
        }
    }
}