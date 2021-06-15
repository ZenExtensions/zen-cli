using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Zen.Core.Services
{
    public class MiscApiService
    {
        
        public async Task<string> GenerateInsultAsync()
        {
            var response = await "https://evilinsult.com/generate_insult.php?lang=en&type=json"
                .GetJsonAsync<InsultModel>();
            return response.Insult;
        }

        public async Task<string> GenerateAdviseAsync()
        {
            var response = await "https://api.adviceslip.com/advice"
                .GetJsonAsync<AdviseResponse>();
            return response.Slip?.Advice;
        }

        class InsultModel
        {
            public string Insult { get; set; }   
        }

        class AdviseResponse
        {
            public AdviseModel Slip { get; set; }
        }
        class AdviseModel
        {
            public string Advice { get; set; }
        }
    }
}