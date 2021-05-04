using Xunit;
using System.Threading.Tasks;
using Giphy.Api.Services;
using Giphy.Api.Controllers;
using Giphy.Api.Persistence;
using Microsoft.Extensions.Options;
using Giphy.Api.Model;

namespace Giphy.Api.Tests
{
    public class TrendingControllerTests
    {
        [Fact]
        public async Task WhenGetThenReturnsCorrectData()
        {
            // Arrange
            var responseJson = System.IO.File.ReadAllText("../../../Responses/trending.response.json");

            var factory = TestHelper.GetIHttpClientFactoryMockObject(responseJson);

            var options = Options.Create(new GiphyClientOptions() {ApiKey = "aaa"});
            var client = new GiphyClient(factory, options);
            var service = new TrendingService(client);
            var controller = new TrendingController(null, service);
            
            // Act
            var result = await controller.Get();
            
            // Assert
            Assert.Equal(TestHelper.JsonToDtosArray(responseJson), result);
        }
    }
}
