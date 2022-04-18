using System;
using Microsoft.Extensions.DependencyInjection;

namespace Zen.Core.SpectreConsole
{
    public class StartupUtil
    {
        public static IServiceCollection GetServiceCollectionFrom<TStartup>(string[] args = null) where TStartup : BaseStartup, new()
        {
            var startup = new TStartup();
            return startup.Configure(args ?? new string[0]);
        }

        public static IServiceCollection GetServiceCollectionFrom<TStartup>(TStartup startup, string[] args = null) where TStartup : BaseStartup
        {
            return startup.Configure(args ?? new string[0]);
        }

        public static IServiceProvider GetServiceProviderFrom<TStartup>(string[] args = null) where TStartup : BaseStartup, new()
        {
            var services = GetServiceCollectionFrom<TStartup>(args);
            return services.BuildServiceProvider();
        }

        public static IServiceProvider GetServiceProviderFrom<TStartup>(TStartup startup, string[] args = null) where TStartup : BaseStartup
        {
            var services = GetServiceCollectionFrom<TStartup>(startup, args);
            return services.BuildServiceProvider();
        }
    }
}