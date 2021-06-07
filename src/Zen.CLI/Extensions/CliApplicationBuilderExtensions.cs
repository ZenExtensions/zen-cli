using System;
using System.Reflection;
using CliFx;
using Microsoft.Extensions.DependencyInjection;

namespace Zen.CLI.Extensions
{
    public static class CliApplicationBuilderExtensions
    {
        public static CliApplicationBuilder UseStartup<TStartup>(this CliApplicationBuilder builder) where TStartup :  BaseStartup, new()
        {
            TStartup startup = new TStartup();
            var services = startup.Configure();
            var serviceProvider = services.BuildServiceProvider();
            builder.UseTypeActivator(serviceProvider.GetRequiredService);
            return builder;
        }
    }
}