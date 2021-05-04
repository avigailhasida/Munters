using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Giphy.Api.Model;
using Microsoft.Extensions.Options;

namespace Giphy.Api.Persistence
{
    public class GiphyClient
    {
        public const string Name = "giphy";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GiphyClientOptions _options;

        public GiphyClient(IHttpClientFactory httpClientFactory, IOptions<GiphyClientOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<GiphyModel> GetTrendingsAsync(int limit, int offset)
        {
            var client = _httpClientFactory.CreateClient(Name);

            var json = await client.GetStringAsync($"trending?api_key={_options.ApiKey}&limit={limit}&offset={offset}rating=g");
            
            var model = JsonSerializer.Deserialize<GiphyModel>(json);

            return model;
        }

        public async Task<GiphyModel> SearchAsync(string query, int limit, int offset)
        {
            var client = _httpClientFactory.CreateClient(Name);

            var json = await client.GetStringAsync($"search?api_key={_options.ApiKey}&q={query}&limit={limit}&offset={offset}&rating=g&lang=en");
            
            var model = JsonSerializer.Deserialize<GiphyModel>(json);

            return model;
        }
    }
}