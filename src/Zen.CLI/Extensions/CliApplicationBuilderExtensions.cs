using System;
using System.Reflection;
using System.Threading.Tasks;
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
            builder
                .AddCommandsFromThisAssembly()
                .UseTypeActivator(services.GetRequiredService);
            return builder;
        }

        public static ValueTask<int> BuildAndRunAsync(this CliApplicationBuilder builder) => builder.Build().RunAsync();
    }
}