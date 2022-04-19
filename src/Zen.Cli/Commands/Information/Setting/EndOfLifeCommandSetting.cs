using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zen.Cli.Commands.Information.Setting
{
    public class EndOfLifeCommandSetting : ZenCommandSetting
    {
        [CommandOption("-q|--query")]
        public string Query { get; set; }
    }
}