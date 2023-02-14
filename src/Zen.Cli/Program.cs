using Zen.Cli;

await new CliHost("zen")
    .Configure(c => {
        c.Spinner = Spinner.Known.Arc;
        c.TableBorder = TableBorder.Markdown;
    })
    .RunAsync<Startup>(args);