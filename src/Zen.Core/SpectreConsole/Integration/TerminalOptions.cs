using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace Zen.Core.SpectreConsole
{
    public static class TerminalOptions
    {
        internal static ProgressColumn[] ProgressColumns
        {
            get
            {
                var columns = new List<ProgressColumn>();
                columns.Add(new TaskDescriptionColumn());
                columns.Add(new ProgressBarColumn());
                columns.Add(new PercentageColumn());
                columns.Add(new SpinnerColumn(TerminalOptions.SpinnerStyle));
                return columns.ToArray();
            }
        }

        internal static Spinner SpinnerStyle = Spinner.Known.Ascii;
        internal static TableBorder TableBorder = TableBorder.Markdown;
    }
}