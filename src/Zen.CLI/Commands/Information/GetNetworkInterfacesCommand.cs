using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Zen.CLI.Commands.Information
{
    public class GetNetworkInterfacesCommand : AsyncCommand
    {
        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots6)
                .StartAsync("Collecting information...", ctx =>
                {
                    string GetPlatformSpecificInfo<TReturn>(NetworkInterface nic, Func<NetworkInterface, TReturn> func)
                    {
                        try
                        {   
                            var value = func(nic);
                            return value?.ToString();
                        }
                        catch (PlatformNotSupportedException)
                        {
                            return "Not Supported";
                        }
                    }
                    var nics = NetworkInterface.GetAllNetworkInterfaces();
                    var table = new Table();
                    table.Title = new TableTitle("Network Interfaces", new Style(Color.Aqua));
                    table.AddColumns(
                        new TableColumn("Id"),
                        new TableColumn("Name"),
                        new TableColumn("Supports Multicast?"),
                        new TableColumn("Operational Status"),
                        new TableColumn("Interface Type"),
                        new TableColumn("DNS Enabled?"),
                        new TableColumn("Gateway IP")
                    );

                    foreach (var nic in nics)
                    {
                        table.AddRow(
                            nic.Id,
                            nic.Name,
                            nic.SupportsMulticast.ToString(),
                            GetPlatformSpecificInfo(nic, nic => nic.OperationalStatus),
                            GetPlatformSpecificInfo(nic, nic => nic.NetworkInterfaceType),
                            nic.GetIPProperties().IsDnsEnabled.ToString(),
                            nic.GetIPProperties().GatewayAddresses?.FirstOrDefault()?.Address?.ToString() ?? string.Empty
                        );
                    }
                    table.Border(TableBorder.Rounded);
                    ctx.SpinnerStyle(Style.Parse("green"));
                    AnsiConsole.Render(table);
                    return Task.CompletedTask;
                });
            return 0;
        }
    }
}