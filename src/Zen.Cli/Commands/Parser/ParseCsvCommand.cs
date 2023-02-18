using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zen.Cli.Commands
{
    public class ParseCsvCommandSettings : ZenCommandSettings
    {   
        [Description("Csv file to parse")]
        [CommandOption("-f|--file")]
        public string? FileName { get; set; }

        public override ValidationResult Validate()
        {   
            if (string.IsNullOrWhiteSpace(FileName))
                return ValidationResult.Error(message: "Please enter a valid file name");
            var fileInfo = new FileInfo(FileName);
            if (!fileInfo.Exists)
                return ValidationResult.Error(message: "Please enter a valid file name");
            if (fileInfo.Extension != ".csv")
                return ValidationResult.Error(message: "Please enter a valid csv file");
            return ValidationResult.Success();
        }
    }
    public class ParseCsvCommand : ZenAsyncCommand<ParseCsvCommandSettings>
    {
        public override Task OnExecuteAsync(CommandContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}