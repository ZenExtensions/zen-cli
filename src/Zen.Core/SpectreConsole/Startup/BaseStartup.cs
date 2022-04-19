using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zen.Core.SpectreConsole
{
    public abstract class BaseStartup
    {
        internal IServiceCollection Configure(string[] args)
        {
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables();
            ConfigureAppConfiguration(configurationBuilder);
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfigurationRoot>(configurationBuilder.Build());
            ConfigureServices(services, configuration);
            return services;
        }
        public virtual void ConfigureAppConfiguration(IConfigurationBuilder configuration)
        {
        }

        public abstract void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration);
    }
}