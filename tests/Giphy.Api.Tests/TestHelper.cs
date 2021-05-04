using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Giphy.Api.Converters;
using Giphy.Api.Dto;
using Giphy.Api.Model;
using Moq;
using Moq.Protected;

namespace Giphy.Api.Tests
{
    public static class TestHelper
    {
        public static GiphyDto[] JsonToDtosArray(string json)
        {
            var model = JsonSerializer.Deserialize<GiphyModel>(json);
            return model.ToDtosArray();
        }

        public static IHttpClientFactory GetIHttpClientFactoryMockObject(string response)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(response),
                    });

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock
                .Setup(f => f.CreateClient("giphy"))
                .Returns(new HttpClient(handlerMock.Object)
                {
                    BaseAddress = new Uri("http://test.com")
                });

            return factoryMock.Object;

        }
    }
}