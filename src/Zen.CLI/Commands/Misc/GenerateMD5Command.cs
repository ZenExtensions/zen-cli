using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Spectre.Console.Cli;
using static Spectre.Console.AnsiConsole;

namespace Zen.CLI.Commands.Misc
{
    public class GenerateMD5Command : Command<GenerateMD5Command.MD5CommandSetting>
    {

        public class MD5CommandSetting : CommandSettings
        {
            [Required]
            [CommandArgument(position: 0, template: "[text]")]
            public string Text { get; set; }


        }

        public override int Execute([NotNull] CommandContext context, [NotNull] MD5CommandSetting settings)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(settings.Text));
                var uuid = new Guid(hash);
                var hashValue = BitConverter.ToString(hash);
                WriteLine($"MD5 hash generated from text is '{hashValue}'");
                WriteLine($"UUID generated from text is '{uuid}'");
            }
            return 0;
        }
    }
}