using System.Threading.Tasks;
using Spectre.Console.Cli;
using Zen.CLI.Extensions;

namespace Zen.CLI
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var app = CommandAppHost.WithStartup<Startup>();
            app.Configure(options => 
            {
                options.SetApplicationName("zen");
                options.CaseSensitivity(CaseSensitivity.None);
                options.ConfigureCommands();
            });
            return await app.RunAsync(args);
        }
    }
}
