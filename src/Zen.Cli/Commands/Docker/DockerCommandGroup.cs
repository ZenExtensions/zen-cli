using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zen.Cli.Commands.Docker
{
    public class DockerCommandGroup : IZenCommandGroup
    {
        public string Name => "docker";

        public void ConfigureCommandApp(IConfigurator<ZenCommandSetting> options)
        {
            options.SetDescription("Additional Operations to perform with docker cli");
            options.AddCommand<RemovedUnusedImagesCommand>("remove-unused-images")
                .WithDescription("Removes unused docker images with no tag");
            options.AddCommand<RemoveExitedContainersCommand>("remove-exited-containers")
                .WithDescription("Removes exited containers");
        }
    }
}