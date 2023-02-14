using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;
using TextCopy;

namespace Zen.Cli
{
    public class Startup : BaseStartup
    {
        public override void ConfigureCommands(in IConfigurator configurator)
        {
            configurator.AddBranch("gen", options => {
                options.SetDescription("Generate different things from cli.");
                options.AddCommand<GenerateGuidCommand>("uuid")
                    .WithDescription("Generates Guid and copies to clipboard")
                    .WithAlias("guid")
                    .WithExample(new [] {"gen", "uuid"});
                options.AddCommand<GeneratePasswordCommand>("password")
                    .WithDescription("Generates password and copies to clipboard")
                    .WithExample(new [] {"gen", "password"});
            });
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            services.InjectClipboard();
            services.AddSingleton<GeneratePasswordCommandSetting>();
        }
    }
}