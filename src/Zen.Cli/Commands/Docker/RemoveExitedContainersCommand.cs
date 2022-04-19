using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Humanizer;

namespace Zen.Cli.Commands.Docker
{
    public class RemoveExitedContainersCommand : ZenCommand
    {
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var client = new DockerClientConfiguration()
                .CreateClient();
            var containers = await Terminal.NewStatus()
                .StartAsync("Retrieving containers...", async ctx => 
                {
                    var containers = await client.Containers.ListContainersAsync(new ContainersListParameters
                    {
                        All = true
                    }, cancellationToken);
                    return containers.Where(item => item.State.Equals("exited"));
                });
            if(!containers.Any())
            {
                Terminal.WriteInfo("No exited containers found");
                return;
            }
            foreach (var container in containers)
            {
                Terminal.WriteBulletPoint($"{container.ID} ({container.State})");
            }
            var prompt = await Terminal.ConfirmAsync($"Are you sure you want to remove these {containers.Count()} containers?", cancellationToken);
            if(!prompt)
                return;
            await Terminal.CreateProgressTaskAsync("Deleting unused containers...", containers.Count(), async progress =>
            {
                foreach (var container in containers)
                {
                    await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters
                    {
                        Force = true
                    }, cancellationToken);
                    progress.Increment(1);
                    Terminal.WriteInfo($"Deleted container {container.ID}");
                }
            });
        }
    }
}