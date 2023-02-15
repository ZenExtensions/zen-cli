namespace Zen.Cli.Commands;

public class GenerateGitIgnoreCommandSettings : ZenCommandSettings
{
    
}

public class GenerateGitIgnoreCommand : ZenAsyncCommand<GenerateGitIgnoreCommandSettings>
{
    public override Task OnExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}