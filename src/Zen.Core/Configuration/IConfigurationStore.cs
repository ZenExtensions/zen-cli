using System.Threading.Tasks;

namespace Zen.Core.Configuration
{
    public interface IConfigurationStore<TConfiguration>
    {
        /// <summary>
        /// Retrives configuration from file
        /// </summary>
        /// <exception cref="CorruptConfigurationException">Thrown if configuration stored is invalid</exception>
        Task<TConfiguration> RetrieveAsync();
        Task StoreAsync(TConfiguration configuration);
    }
}