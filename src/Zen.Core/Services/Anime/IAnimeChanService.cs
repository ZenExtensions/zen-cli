using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Zen.Core.Constants;

namespace Zen.Core.Services.Anime
{
    public class AnimeQuote
    {
        public string Anime { get; set; }
        public string Quote { get; set; }
        public string Character { get; set; }
    }
    public interface IAnimeChanService
    {
        Task<AnimeQuote> GetRandomQuoteAsync(string anime = null);
        Task<List<string>> GetAnimeListAsync(string query);
    }

    public class AnimeChanService : IAnimeChanService
    {
        public async Task<List<string>> GetAnimeListAsync(string query)
        {
            var animes = await DefaultUrls.ANIME_CHAN_API
                .AppendPathSegment("api")
                .AppendPathSegment("available")
                .AppendPathSegment("anime")
                .GetJsonAsync<List<string>>();

            var animeQueries = query.Split(',')
                .Select(value => value.Trim());
            return animes
                .Where(type => {
                    return animeQueries.Any(a=> type.StartsWith(a, ignoreCase: true, CultureInfo.InvariantCulture));
                }).ToList();
        }

        public async Task<AnimeQuote> GetRandomQuoteAsync(string anime = null)
        {
            AnimeQuote quote = null;
            if(anime is null)
            {
                quote = await DefaultUrls.ANIME_CHAN_API
                    .AppendPathSegment("api")
                    .AppendPathSegment("random")
                    .GetJsonAsync<AnimeQuote>();
            }
            else
            {
                var quotes = await DefaultUrls.ANIME_CHAN_API
                    .AppendPathSegment("api")
                    .AppendPathSegment("quotes")
                    .AppendPathSegment("anime")
                    .SetQueryParam(name: "title", value: anime)
                    .GetJsonAsync<List<AnimeQuote>>();
                if(!quotes.Any())
                    throw new Exception("No quote found");
                var random = new Random();
                var index = random.Next(0, quotes.Count);
                quote = quotes[index];
            }
            return quote;
        }
    }
}