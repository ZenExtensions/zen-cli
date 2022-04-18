using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;

namespace Zen.Core.SpectreConsole.Integration
{
    public static class Terminal
    {
        public static void WriteBulletPoint(string text)
        {
            AnsiConsole.MarkupLine($":backhand_index_pointing_right: {text}");
        }

        public static void WriteError(string message)
        {
            AnsiConsole.MarkupLine($":cross_mark: [red]{message}[/]");
        }
        public static void WriteSuccess(string message)
        {
            AnsiConsole.MarkupLine($":check_mark_button: [green1]{message}[/]");
        }
        public static void WriteWarning(string message)
        {
            AnsiConsole.MarkupLine($":warning: [yellow]{message}[/]");
        }

        public static void WriteInfo(string message)
        {
            AnsiConsole.MarkupLine($":check_mark: [grey69]{message}[/]");
        }

        public static Task<bool> ConfirmAsync(string message, CancellationToken cancellationToken = default)
        {
            return new ConfirmationPrompt(message)
            {
                DefaultValue = false
            }.ShowAsync(AnsiConsole.Console, cancellationToken);
        }

        public static Task<bool> ConfirmAsync(string message, bool defaultValue = false, CancellationToken cancellationToken = default)
        {
            return new ConfirmationPrompt(message)
            {
                DefaultValue = defaultValue
            }.ShowAsync(AnsiConsole.Console, cancellationToken);
        }

        public static Table CreateTable(string title)
        {
            return new Table()
                .Title(title)
                .Border(TerminalOptions.TableBorder);
        }

        public static Progress CreateProgress()
        {
            return AnsiConsole.Progress()
                .Columns(TerminalOptions.ProgressColumns);
        }

        public static Task<T> CrreateProgressTaskAsync<T>(string description, double maxCount, Func<ProgressTask, Task<T>> func)
        {
            return Terminal.CreateProgress()
                .StartAsync<T>(context =>
                {
                    var progressTask = context.AddTask(description, maxValue: maxCount);
                    return func(progressTask);
                });
        }

        public static Task CrreateProgressTaskAsync(string description, double maxCount, Func<ProgressTask, Task> func)
        {
            return Terminal.CreateProgress()
                .StartAsync(context =>
                {
                    var progressTask = context.AddTask(description, maxValue: maxCount);
                    return func(progressTask);
                });
        }

        public static Status NewStatus()
        {
            return AnsiConsole.Status()
                .Spinner(TerminalOptions.SpinnerStyle);
        }
    }
}