using System.Threading.Tasks;
using Giphy.Api.Converters;
using Giphy.Api.Dto;
using Giphy.Api.Persistence;

namespace Giphy.Api.Services
{
    public class SearchService
    {
        private readonly GiphyClient _client;
        private readonly CacheManager _cache;
        public SearchService(GiphyClient client, CacheManager cache)
        {
            _client = client;
            _cache = cache;
        }

        public Task<GiphyDto[]> GetAsync(string query, int limit, int offset)
        {
            var key = GetCacheKey(query, limit, offset);

            return _cache.GetOrCreateAsync(key, 
                async () => {
                    var model = await _client.SearchAsync(query, limit, offset);
                    return model.ToDtosArray();
                });
        }

        private string GetCacheKey(string query, int limit, int offset)
        {
            return $"{query}[{limit}:{offset}]";
        }
    }
}