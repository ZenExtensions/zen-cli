using CliFx;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zen.CLI
{
    public abstract class BaseStartup
    {

        internal IServiceCollection Configure()
        {
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            ConfigureAppConfiguration(configurationBuilder);
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfigurationRoot>(configurationBuilder.Build());
            ConfigureServices(services);
            return services;
        }
        public virtual void ConfigureAppConfiguration(IConfigurationBuilder configuration)
        {
            configuration.AddEnvironmentVariables()
                .AddJsonFile("appsettings.json");
        }
        public abstract void ConfigureServices(IServiceCollection services);
    }
}