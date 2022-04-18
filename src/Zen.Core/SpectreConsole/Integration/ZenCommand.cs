using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Zen.Core.SpectreConsole.Integration
{
    public abstract class ZenCommand<TSetting> : AsyncCommand<TSetting> where TSetting : ZenCommandSetting
    {
        protected TSetting Setting { get; private set; }
        public override async Task<int> ExecuteAsync(CommandContext context, TSetting settings)
        {
            Setting = settings;
            var cancellationTokenSource = new CancellationTokenSource();
            System.Console.CancelKeyPress += delegate (object sender, System.ConsoleCancelEventArgs args)
            {
                cancellationTokenSource.Cancel();
                args.Cancel = true;
            };
            await ExecuteAsync(cancellationTokenSource.Token);
            return 0;
        }

        public abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}