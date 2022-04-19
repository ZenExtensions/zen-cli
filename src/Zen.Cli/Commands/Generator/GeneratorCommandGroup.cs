using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Zen.Cli.Commands.Generator
{
    public class GeneratorCommandGroup : IZenCommandGroup
    {
        public string Name => "gen";

        public void ConfigureCommandApp(IConfigurator<ZenCommandSetting> options)
        {
            options.SetDescription("Generate different things from cli.");
            options.AddCommand<GenerateGuidCommand>("uuidgen")
                    .WithDescription("Generates Guid and copies to clipboard")
                    .WithAliases("guid", "uuid", "guidgen")
                    .WithExample("gen","uuidgen")
                    .WithExample("gen","guidgen")
                    .WithExample("gen","guid")
                    .WithExample("gen","uuid");
            options.AddCommand<GenerateMd5Command>("md5")
                    .WithDescription("Generates MD5 hash value")
                    .WithExample("generate", "md5", "\"Hello World\"");
            options.AddCommand<GenerateGitIgnoreCommand>("gitignore")
                .WithDescription("Utility for gitignore.io")
                .WithAlias("giio")
                .WithExample("gen", "gitignore")
                .WithExample("gen", "giio")
                .WithExample("gen", "giio", "-q", "visual")
                .WithExample("gen", "giio", "--query", "visual", "--destination", "/home/user/projects/my-app/")
                .WithExample("gen", "giio", "--query", "visual");
            options.AddCommand<GeneratePasswordCommand>("password")
                .WithDescription("Generates a random password")
                .WithAlias("pwd");
        }
    }
}