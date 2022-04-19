using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Zen.Cli.Commands.Generator.Settings
{
    public class GeneratePasswordCommandSetting : ZenCommandSetting
    {
        [CommandOption("-l|--length")]
        [Description("The length of the password to generate")]
        [DefaultValue(12)]
        public int Length { get; set; }
        
        
    }
}