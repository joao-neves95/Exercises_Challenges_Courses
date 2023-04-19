
using System.Net;

using FluentAssertions;

using GamingApi.WebApi.Infrastructure;
using GamingApi.WebApi.Infrastructure.Entities;

using Moq;
using Moq.Protected;

namespace GamingApi.WebApi.Tests.Infrastructure
{
    public class DotnetHttpClientTests
    {
        private readonly DotnetHttpClient _sut;

        private readonly NewtonsoftJsonClient _newtonsoftJsonClient;

        private readonly HttpClient _httpClientMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        private readonly DateTime _fakeReleaseDate = DateTime.Parse("2022-11-06T00:09:00");
        private readonly Stream _stream = new MemoryStream();

        public DotnetHttpClientTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StreamContent(GetFakeJsonStream(_stream)),
            };

            // https://stackoverflow.com/questions/50378991/how-to-mock-a-stream-response-when-calling-httpclient-getstreamasync
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                     ItExpr.IsAny<HttpRequestMessage>(),
                     ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            _httpClientMock = new HttpClient(_httpMessageHandlerMock.Object);
            _newtonsoftJsonClient = new NewtonsoftJsonClient();
            _sut = new DotnetHttpClient(_httpClientMock, _newtonsoftJsonClient);
        }

        ~DotnetHttpClientTests()
        {
            _stream.Dispose();
            _httpClientMock.Dispose();
        }

        [Fact]
        public async Task GetAsync_GetsStreamAndDeserializesResponse()
        {
            var result = await _sut.GetAsync<IEnumerable<DataGame>>("https://super-endpoint.pt/games.json");

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(GetFakeGameIntances());
            _httpMessageHandlerMock.VerifyAll();
        }

        private static Stream GetFakeJsonStream(Stream stream)
        {
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteAsync(GetFakeGamesJson());
            streamWriter.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private IEnumerable<DataGame> GetFakeGameIntances()
        {
            for (var i = 1; i <= 3; ++i)
            {
                yield return new DataGame()
                {
                    AppId = i,
                    Name = $"game_{i}",
                    Developer = $"dev_{i}",
                    ReleaseDate = _fakeReleaseDate,
                    Platforms = new DataPlatforms()
                    {
                        Linux = i % 2 == 0,
                        Mac = i % 2 == 0,
                        Windows = i % 2 != 0,
                    }
                };
            }
        }

        private static string GetFakeGamesJson()
        {
            return @"
[
    {
        ""appid"": 1,
        ""name"":""game_1"",
        ""developer"":""dev_1"",
        ""release_date"":""2022-11-06T00:09:00"",
        ""platforms"": {
            ""windows"":true,
            ""mac"":false,
            ""linux"":false
        }
    },
    {
        ""appid"": 2,
        ""name"":""game_2"",
        ""developer"":""dev_2"",
        ""release_date"":""2022-11-06T00:09:00"",
        ""platforms"": {
            ""windows"":false,
            ""mac"":true,
            ""linux"":true
        }
    },
    {
        ""appid"": 3,
        ""name"":""game_3"",
        ""developer"":""dev_3"",
        ""release_date"":""2022-11-06T00:09:00"",
        ""platforms"":{
            ""windows"":true,
            ""mac"":false,
            ""linux"":false
        }
    }
]
";
        }
    }
}
