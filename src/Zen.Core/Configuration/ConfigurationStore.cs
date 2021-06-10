using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Zen.Core.Serializers;

namespace Zen.Core.Configuration
{
    public class ConfigurationStore<TConfiguration> : IConfigurationStore<TConfiguration>
    {
        private readonly string fileName;

        public ConfigurationStore(string name)
        {
            var dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".zen");
            var dir = new DirectoryInfo(dirPath);
            if(!dir.Exists)
                dir.Create();
            fileName = Path.Combine(Path.Combine(dirPath, $"{name}.json"));
                
        }

        /// <inheritdoc/>
        public async Task<TConfiguration> RetrieveAsync()
        {
            var configurationContent = await File.ReadAllTextAsync(fileName);
            TConfiguration configuration;
            try
            {
                configuration = configurationContent.Deserialize<TConfiguration>();
            }
            catch (JsonException)
            {
                throw new CorruptConfigurationException();
            }
            return configuration;
        }

        public Task StoreAsync(TConfiguration configuration)
        {
            var json = configuration.Serialize();
            return File.WriteAllTextAsync(fileName, json, Encoding.UTF8);
        }
    }
}