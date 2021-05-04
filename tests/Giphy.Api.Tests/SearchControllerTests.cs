using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Giphy.Api.Controllers;
using Giphy.Api.Model;
using Giphy.Api.Persistence;
using Giphy.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace Giphy.Api.Tests
{
    public class SearchControllerTests
    {
        [Fact]
        public async Task WhenGetThenReturnsCorrectData()
        {
            // Arrange
            var responseJson = System.IO.File.ReadAllText("../../../Responses/search.response.json");

            var factory = TestHelper.GetIHttpClientFactoryMockObject(responseJson);

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var servicesProvider = services.BuildServiceProvider();
            var memroyCache = servicesProvider.GetService<IMemoryCache>();
            var cacheManager = new CacheManager(memroyCache);

            var options = Options.Create( new GiphyClientOptions() {ApiKey = "aaa"});
            var client = new GiphyClient(factory, options);
            var service = new SearchService(client, cacheManager);
            var controller = new SearchController(null, service);
            
            // Act
            var result = await controller.Get("good time");
            
            // Assert
            Assert.Equal(TestHelper.JsonToDtosArray(responseJson), result);
        }

        [Fact]
        public async Task WhenGetIsCalledTwiceThenReturnsDataFromCache()
        {
            // Arrange
            var responseJson = System.IO.File.ReadAllText("../../../Responses/search.response.json");

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseJson),
                    });

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock
                .Setup(f => f.CreateClient("giphy"))
                .Returns(new HttpClient(handlerMock.Object)
                {
                    BaseAddress = new Uri("http://test.com")
                });

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var servicesProvider = services.BuildServiceProvider();
            var memroyCache = servicesProvider.GetService<IMemoryCache>();
            var cacheManager = new CacheManager(memroyCache);

            var options = Options.Create(new GiphyClientOptions() {ApiKey = "aaa"});
            var client = new GiphyClient(factoryMock.Object, options);
            var service = new SearchService(client, cacheManager);
            var controller = new SearchController(null, service);
            
            var query = "good time";
            await controller.Get(query);
            
            // Act
            var result = await controller.Get(query);

            // Assert
            handlerMock.Protected().Verify<Task<HttpResponseMessage>>(
                    "SendAsync", new Type[] {}, Times.Once(),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}