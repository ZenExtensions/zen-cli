using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zen.CLI
{
    public abstract class BaseStartup
    {
        internal IServiceCollection Configure()
        {
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json",optional: false);
            ConfigureAppConfiguration(configurationBuilder);
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfigurationRoot>(configurationBuilder.Build());
            ConfigureServices(services);
            return services;
        }
        public virtual void ConfigureAppConfiguration(IConfigurationBuilder configuration)
        {
        }
        public abstract void ConfigureServices(IServiceCollection services);
    }
}