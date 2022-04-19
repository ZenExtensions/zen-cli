using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Zen.Core.Constants;

namespace Zen.Cli.Commands.Generator
{
    public class GenerateGitIgnoreCommand : ZenCommand<GenerateGitIgnoreCommandSetting>
    {
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(Setting.Query))
                Setting.Query = await Terminal.AskAsync<string>("Enter Operating Systems, IDEs, or Programming Languages (use comma to separate values)", cancellationToken);
            
            var types = await Terminal.NewStatus()
                .StartAsync("Querying...", async ctx =>
                {
                    var response = await DefaultUrls.GITIGNORE_IO_URL
                        .AppendPathSegment("api")
                        .AppendPathSegment("list")
                        .GetStringAsync();
                    var types = response.Replace(',','\n').Split('\n').ToList();
                    var queryTypes = Setting.Query.Split(',')
                        .Select(value => value.Trim());
                    return types
                        .Where(type => {
                            return queryTypes.Any(a=> type.StartsWith(a, ignoreCase: true, CultureInfo.InvariantCulture));
                        }).ToList();
                });
            if(!types.Any())
            {
                Terminal.Markup($"[yellow]No gitignore found against query [/]");
                return;
            }
            var choices = await Terminal.MultiSelectionPromptAsync(
                "Select gitignore types",
                types,
                cancellationToken);
            
            await Terminal.NewStatus()
                .StartAsync("Downloading...", async ctx =>
                {
                    var cotnent = await DefaultUrls.GITIGNORE_IO_URL
                        .AppendPathSegment("api")
                        .AppendPathSegment(string.Join(',', choices))
                        .GetStringAsync();
                    var fullPath = Path.Combine(Setting.Destination, ".gitignore");
                    await File.WriteAllTextAsync(fullPath, cotnent, Encoding.UTF8);
                });
            Terminal.WriteSuccess($"Gitignore file successfully generated for {string.Join(", ", choices)}");
        }
    }
}