using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Zen.Core.Constants;

namespace Zen.Core.Services
{
    public interface IGitIgnoreService
    {
        Task<List<string>> ListTypesAsync();
        Task<List<string>> ListTypesAsync(string query);
        Task<string> DownloadAsync(IEnumerable<string> types);
    }

    public class GitIgnoreService : IGitIgnoreService
    {
        public async Task<string> DownloadAsync(IEnumerable<string> types)
        {
            var response = await DefaultUrls.GITIGNORE_IO_URL
                .AppendPathSegment("api")
                .AppendPathSegment(string.Join(',', types))
                .GetStringAsync();
            return response;
        }

        public async Task<List<string>> ListTypesAsync()
        {
            var response = await DefaultUrls.GITIGNORE_IO_URL
                .AppendPathSegment("api")
                .AppendPathSegment("list")
                .GetStringAsync();
            var types = response.Replace(',','\n').Split('\n').ToList();
            return types;
        }

        public async Task<List<string>> ListTypesAsync(string query)
        {
            var types = await ListTypesAsync();
            var queryTypes = query.Split(',')
                .Select(value => value.Trim());
            return types
                .Where(type => {
                    return queryTypes.Any(a=> type.StartsWith(a, ignoreCase: true, CultureInfo.InvariantCulture));
                }).ToList();
        }
    }
}