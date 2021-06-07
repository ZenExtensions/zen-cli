using System;
using System.Threading.Tasks;
using CliFx;
using Zen.CLI.Extensions;

namespace Zen.CLI
{
    class Program
    {
        public static async Task<int> Main() => 
            await new CliApplicationBuilder()
                .SetTitle("Zen CLI")
                .SetDescription("Automate boring stuff 🤖")
                .UseStartup<Startup>()
                .Build()
                .RunAsync();
    }
}
