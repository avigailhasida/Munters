using System.Collections.Generic;
using System.Threading.Tasks;
using Giphy.Api.Converters;
using Giphy.Api.Dto;
using Giphy.Api.Persistence;

namespace Giphy.Api.Services
{
    public class TrendingService
    {
        private readonly GiphyClient _giphyClient;

        public TrendingService(GiphyClient giphyClient)
        {
            _giphyClient = giphyClient;
        }

        public async Task<IEnumerable<GiphyDto>> GetAsync(int limit, int offset)
        {
            var model = await _giphyClient.GetTrendingsAsync(limit, offset);
            return model.ToDtosArray();
        }
    }
}