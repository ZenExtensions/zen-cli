using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Zen.Core.SpectreConsole
{
    public interface IZenCommandGroup
    {
        string Name { get; }
        void ConfigureCommandApp(IConfigurator<ZenCommandSetting> options); 
    }
}