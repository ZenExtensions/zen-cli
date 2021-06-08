using System;
using System.Threading;
using System.Threading.Tasks;
using CliFx;
using CliFx.Exceptions;
using CliFx.Infrastructure;

namespace Zen.CLI.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public async ValueTask ExecuteAsync(IConsole console)
        {
            var cancellationToken = console.RegisterCancellationHandler();
            try
            {
                await ValidateAsync(cancellationToken);
            }
            catch (NotImplementedException) { }
            try
            {
                await ExecuteCommandAsync(console, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                await console.Error.WriteLineAsync(ex.Message);
            }
        }

        public ValueTask ShowCommandHelpAsync()
        {
            throw new CommandException("Please define a follow up command", showHelp: true);
        }

        public abstract ValueTask ExecuteCommandAsync(IConsole console, CancellationToken cancellationToken);

        public virtual ValueTask ValidateAsync(CancellationToken cancellationToken)
        {
            return default;
        }
    }
}