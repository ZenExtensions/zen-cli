using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zen.Cli.Commands.Generator
{
    public class GenerateMd5Command : ZenCommand<GenerateMd5CommandSetting>
    {
        public override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Setting.Text));
                var uuid = new Guid(hash);
                var hashValue = BitConverter.ToString(hash);
                Terminal.WriteLine($"MD5 hash generated from text is '{hashValue}'");
                Terminal.WriteLine($"UUID generated from text is '{uuid}'");
            }
            return Task.CompletedTask;
        }
    }
}