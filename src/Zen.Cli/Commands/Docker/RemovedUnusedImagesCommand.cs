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
    public class RemovedUnusedImagesCommand : ZenCommand
    {
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var client = new DockerClientConfiguration()
                .CreateClient();
            var images = await Terminal.NewStatus()
                .StartAsync("Retrieving images...", async ctx => 
                {
                    var images = await client.Images.ListImagesAsync(new ImagesListParameters
                    {
                        All = false
                    }, cancellationToken);
                    return images.Where(item => item.RepoTags.Any(tag => tag.StartsWith("<none>")));
                });
            foreach (var image in images)
            {
                Terminal.WriteBulletPoint($"{image.ID} ({image.Size.Bits().Humanize()})");
            }
            var size = images.Select(item => item.Size).Sum()
                .Bits().Humanize();
            
            images.Select(item => item.Size).Sum();
            var prompt = await Terminal.ConfirmAsync($"Are you sure you want to remove these {images.Count()} images ({size})?", cancellationToken);
            if(!prompt)
                return;
            await Terminal.CreateProgressTaskAsync("Deleting unused images...", images.Count(), async progress =>
            {
                foreach (var image in images)
                {
                    await client.Images.DeleteImageAsync(image.ID, new ImageDeleteParameters
                    {
                        Force = true
                    }, cancellationToken);
                    progress.Increment(1);
                    Terminal.WriteInfo($"Deleted image {image.ID}");
                }
            });

        }
    }
}