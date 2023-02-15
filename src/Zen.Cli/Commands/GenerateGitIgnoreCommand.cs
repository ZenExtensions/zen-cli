using System.ComponentModel;
using System.Globalization;
using Refit;

namespace Zen.Cli.Commands;

public class GenerateGitIgnoreCommandSettings : ZenCommandSettings
{
    [Description("Operating Systems, IDEs, or Programming Languages (use comma to separate values)")]
    [CommandOption("-q|--query")]
    public string? Query { get; set; }

    [Description("Destination for storing gitignore file")]
    [DefaultValue("./")]
    [CommandOption("-d|--destination")]
    public string Destination { get; set; } = default!;

    public override ValidationResult Validate()
    {
        var dir = new DirectoryInfo(Destination);
        if (!dir.Exists)
            return ValidationResult.Error(message: "Please enter a valid destination");
        return ValidationResult.Success();
    }
}

public class GenerateGitIgnoreCommand : ZenAsyncCommand<GenerateGitIgnoreCommandSettings>
{
    public override async Task OnExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(Settings.Query))
            Settings.Query = await Terminal.AskAsync<string>("Enter Operating Systems, IDEs, or Programming Languages (use comma to separate values)", cancellationToken);
        var api = RestService.For<IGitIgnoreApi>("https://www.gitignore.io");
        var types = await Terminal.StatusAsync(
            "Querying...", async _ =>
            {
                var response = await api.ListChoicesAsync(cancellationToken);
                var types = response.Replace(',', '\n').Split('\n').ToList();
                var queryTypes = Settings.Query.Split(',')
                    .Select(value => value.Trim());
                return types
                    .Where(type =>
                    {
                        return queryTypes.Any(a => type.StartsWith(a, ignoreCase: true, CultureInfo.InvariantCulture));
                    }).ToList();
            }
        );
        if (!types.Any())
        {
            Terminal.WriteWarning("No gitignore found against query");
            return;
        }
        var choices = await Terminal.MultiSelectionPromptAsync(
            "Select gitignore types",
            types,
            cancellationToken);
        await Terminal.StatusAsync(
            "Generating...",
            async _ =>
            {
                var content = await api.GetAsync(string.Join(",", choices), cancellationToken);
                var file = new FileInfo(Path.Combine(Settings.Destination, ".gitignore"));
                await File.WriteAllTextAsync(file.FullName, content, cancellationToken);
            }
        );

        Terminal.WriteSuccess($"Gitignore file successfully generated for {string.Join(", ", choices)}");
    }
}

#region Apis
public interface IGitIgnoreApi
{
    [Get("/api/list")]
    Task<string> ListChoicesAsync(CancellationToken cancellationToken);

    [Get("/api/{types}")]
    Task<string> GetAsync(string types, CancellationToken cancellationToken);
}
#endregion