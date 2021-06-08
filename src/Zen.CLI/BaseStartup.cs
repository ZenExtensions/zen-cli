using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CliFx;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zen.CLI
{
    public abstract class BaseStartup
    {
        internal IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json",optional: false);
            ConfigureAppConfiguration(configurationBuilder);
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfigurationRoot>(configurationBuilder.Build());
            ConfigureServices(services);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            ConfigureContainer(containerBuilder);
            return new AutofacServiceProvider(containerBuilder.Build());
        }
        public virtual void ConfigureAppConfiguration(IConfigurationBuilder configuration)
        {
        }
        public abstract void ConfigureServices(IServiceCollection services);

        public virtual void ConfigureContainer(ContainerBuilder container)
        {
        }
    }
}