using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Zen.Core.SpectreConsole
{
    public static class Terminal
    {
        public static void WriteLine(string text) => AnsiConsole.WriteLine(text);
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

        public static Task<T> AskAsync<T>(string message, CancellationToken cancellationToken = default)
        {
            return new TextPrompt<T>(message)
                .ShowAsync(AnsiConsole.Console, cancellationToken);
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

        public static Table NewTable(string title)
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

        public static Task<T> CreateProgressTaskAsync<T>(string description, double maxCount, Func<ProgressTask, Task<T>> func)
        {
            return Terminal.CreateProgress()
                .StartAsync<T>(context =>
                {
                    var progressTask = context.AddTask(description, maxValue: maxCount);
                    return func(progressTask);
                });
        }

        public static Task CreateProgressTaskAsync(string description, double maxCount, Func<ProgressTask, Task> func)
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

        public static void Markup(string text) => AnsiConsole.Markup(text);
        public static void MarkupLine(string text) => AnsiConsole.MarkupLine(text);

        public static void Write(IRenderable renderable) => AnsiConsole.Write(renderable);
        public static void Render(IRenderable renderable) => AnsiConsole.Write(renderable);

        public static Task<List<T>> MultiSelectionPromptAsync<T>(string prompt, IEnumerable<T> choices, CancellationToken cancellationToken = default)
        {
            return new MultiSelectionPrompt<T>()
                .Title(prompt)
                .Required()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a type, " + 
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(choices)
                .ShowAsync(AnsiConsole.Console, cancellationToken);
        }

        public static Task<T> SingleSelectionPromptAsync<T>(string prompt, IEnumerable<T> choices, CancellationToken cancellationToken = default)
        {
            return new SelectionPrompt<T>()
                .Title(prompt)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(choices)
                .ShowAsync(AnsiConsole.Console, cancellationToken);
        }
    }
}