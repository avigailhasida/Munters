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
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly SearchService _searchService;

        public SearchController(ILogger<SearchController> logger, SearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IEnumerable<GiphyDto>> Get(string query, int limit = 25, int offset = 0)
        {
            return await _searchService.GetAsync(query, limit, offset);
        }
    }
}