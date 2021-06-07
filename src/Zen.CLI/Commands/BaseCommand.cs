using System;
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
            try
            {
                await ValidateAsync();
            }
            catch (NotImplementedException) { }
            try
            {
                await ExecuteCommandAsync(console);
                await console.Output.WriteLineAsync();
            }
            catch (TaskCanceledException ex)
            {
                await console.Error.WriteLineAsync(ex.Message);
            }
        }

        public abstract ValueTask ExecuteCommandAsync(IConsole console);

        public virtual ValueTask ValidateAsync()
        {
            return default;
        }
    }
}