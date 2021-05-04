using System.Collections.Generic;
using System.Threading.Tasks;
using Giphy.Api.Dto;
using Giphy.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Giphy.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrendingController : ControllerBase
    {
        private readonly ILogger<TrendingController> _logger;
        private readonly TrendingService _trendingService;

        public TrendingController(ILogger<TrendingController> logger, TrendingService trendingService)
        {
            _logger = logger;
            _trendingService = trendingService;
        }

        [HttpGet]
        public async Task<IEnumerable<GiphyDto>> Get(int limit = 25, int offset = 0)
        {
            return await _trendingService.GetAsync(limit, offset);
        }
    }
}