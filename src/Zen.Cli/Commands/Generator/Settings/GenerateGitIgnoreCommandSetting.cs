using System.ComponentModel;
using System.IO;
using Spectre.Console;

namespace Zen.Cli.Commands.Generator.Settings
{
    public class GenerateGitIgnoreCommandSetting : ZenCommandSetting
    {
        [Description("Operating Systems, IDEs, or Programming Languages (use comma to separate values)")]
        [CommandOption("-q|--query")]
        public string Query { get; set; }

        [Description("Destination for storing gitignore file")]
        [DefaultValue("./")]
        [CommandOption("-d|--destination")]
        public string Destination { get; set; }

        public override ValidationResult Validate()
        {
            var dir = new DirectoryInfo(Destination);
            if(!dir.Exists)
                return ValidationResult.Error(message: "Please enter a valid destination");
            return ValidationResult.Success();
        }
    }
}