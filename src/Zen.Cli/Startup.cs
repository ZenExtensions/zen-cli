using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;
using TextCopy;

namespace Zen.Cli;
public class Startup : BaseStartup
{
    public override void ConfigureCommands(in IConfigurator configurator)
    {
        configurator.AddBranch("gen", options =>
        {
            options.SetDescription("Generate different things from cli.");
            options.AddCommand<GenerateGuidCommand>("uuid")
                .WithDescription("Generates Guid and copies to clipboard")
                .WithAlias("guid")
                .WithExample(new[] { "gen", "uuid" });
            options.AddCommand<GeneratePasswordCommand>("password")
                .WithDescription("Generates password and copies to clipboard")
                .WithExample(new[] { "gen", "password" });
            options.AddCommand<GenerateGitIgnoreCommand>("gitignore")
                .WithDescription("Generates gitignore file")
                .WithExample(new[] { "gen", "gitignore" });
        });

        configurator.AddBranch("getinfo", options => {
            options.SetDescription("Get information from cli.");
            options.AddCommand<GetIpCommand>("ip")
                .WithDescription("Get IP address")
                .WithExample(new[] { "getinfo", "ip" });
        });
    }

    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.InjectClipboard();
        services.AddSingleton<GeneratePasswordCommandSetting>();
        services.AddSingleton<GenerateGitIgnoreCommandSettings>();
    }
}