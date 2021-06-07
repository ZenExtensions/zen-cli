using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Refit;

namespace Zen.CLI.Apis
{
    public interface IIfConfigApi
    {
        [Get("/all.json")]
        Task<ApiResponse<IfConfigResponse>> GetInfoAsync();
    }

    public class IfConfigResponse
    {
        [JsonPropertyName("ip_addr")]
        public string IpAddr { get; set; }

        [JsonPropertyName("remote_host")]
        public string RemoteHost { get; set; }

        [JsonPropertyName("user_agent")]
        public string UserAgent { get; set; }

        [JsonPropertyName("port")]
        public long Port { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("encoding")]
        public string Encoding { get; set; }

        [JsonPropertyName("mime")]
        public string Mime { get; set; }

        [JsonPropertyName("via")]
        public string Via { get; set; }

        [JsonPropertyName("forwarded")]
        public string Forwarded { get; set; }
    }
}