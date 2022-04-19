using System.ComponentModel.DataAnnotations;

namespace Zen.Cli.Commands.Generator.Settings
{
    public class GenerateMd5CommandSetting : ZenCommandSetting
    {
        [Required]
        [CommandArgument(position: 0, template: "[text]")]
        public string Text { get; set; }
    }
}