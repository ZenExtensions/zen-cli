using CliFx;

namespace Zen.CLI.Extensions
{
    public static class CliFxHost
    {
        public static CliApplicationBuilder CreateDefaultBuilder()
        {
            var builder = new CliApplicationBuilder();
            
            return builder;
        }
    }
}