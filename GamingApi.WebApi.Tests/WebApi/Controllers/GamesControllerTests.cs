
using AutoFixture;

using FluentAssertions;

using GamingApi.WebApi.Contracts.Interfaces;
using GamingApi.WebApi.Contracts.Interfaces.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Yld.GamingApi.WebApi.ApiContracts;
using Yld.GamingApi.WebApi.Controllers;

namespace GamingApi.WebApi.Tests.WebApi.Controllers
{
    public class GamesControllerTests : BaseTests
    {
        private readonly GamesController _sut;

        private readonly Mock<IGameService<GamesResponse>> _gameServiceMock;

        private readonly Mock<HttpRequest> _httpRequestMock;
        private readonly Mock<HttpContext> _httpContextMock;

        public GamesControllerTests()
        {
            _httpContextMock = new Mock<HttpContext>();
            _httpRequestMock = new Mock<HttpRequest>();

            _gameServiceMock = new Mock<IGameService<GamesResponse>>();

            _sut = new GamesController(_gameServiceMock.Object);
            SetupHttpContextMock();
        }

        private void ResetHttpContextMock()
        {
            _httpRequestMock.Reset();
            _httpContextMock.Reset();
            _sut.ControllerContext = new ControllerContext();
        }

        private void SetupHttpContextMock()
        {
            ResetHttpContextMock();

            _httpRequestMock
                .SetupGet(x => x.Headers)
                .Returns(
                    new HeaderDictionary
                    {
                        {"User-Agent", "Supa Hacka"}
                    });

            _httpContextMock.SetupGet(x => x.Request).Returns(_httpRequestMock.Object);

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContextMock.Object,
            };
        }

        private void SetupGameServiceMock(int limit)
        {
            _gameServiceMock.Reset();

            var fakeGames = DataGenerator.CreateMany<GameResponse>(100);

            _gameServiceMock
                .Setup(mock => mock.GetPaginatedGamesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new GamesResponse()
                {
                    Items = fakeGames.Take(limit),
                });
        }

        private void AssertGetAllGamesAsyncResponsePasses(ActionResult<GamesResponse> result, int limit)
        {
            var successResult = result.Result.As<OkObjectResult>();
            successResult.Should().NotBeNull();
            successResult.StatusCode.Should().Be(200);

            var gamesResponse = successResult.Value.As<GamesResponse>();
            gamesResponse.Should().NotBeNull();
            gamesResponse.Items.Count().Should().Be(limit);
            gamesResponse.TotalItems.Should().Be(limit);

            _gameServiceMock.VerifyAll();
            _httpRequestMock.VerifyAll();
            _httpContextMock.VerifyAll();
        }

        [Fact]
        public async Task GetAllGames_Passes()
        {
            const int limit = 10;

            SetupHttpContextMock();
            SetupGameServiceMock(limit);

            var result = await _sut.GetAllGamesAsync(1, limit);
            AssertGetAllGamesAsyncResponsePasses(result, limit);
        }

        [Fact]
        public async Task GetAllGames_LimitNull_Passes()
        {
            const int defaultLimit = 2;

            SetupHttpContextMock();
            SetupGameServiceMock(defaultLimit);

            var result = await _sut.GetAllGamesAsync(1, null);
            AssertGetAllGamesAsyncResponsePasses(result, defaultLimit);
        }

        [Fact]
        public async Task GetAllGames_LimitHigherThan10_Fails()
        {
            const int limit = 11;

            SetupHttpContextMock();
            SetupGameServiceMock(limit);

            var result = await _sut.GetAllGamesAsync(1, limit);
            result.Value.Should().BeNull();
            result.Result.Should().NotBeNull();

            var errorResult = result.Result.As<BadRequestObjectResult>();
            errorResult.Should().NotBeNull();
            errorResult.StatusCode.Should().Be(400);
            errorResult.Value.As<string>().Should().Contain("limit");
        }

        [Fact]
        public async Task GetAllGames_NoUserAgent_Fails()
        {
            ResetHttpContextMock();
            SetupGameServiceMock(0);

            var result = await _sut.GetAllGamesAsync(null, null);
            result.Value.Should().BeNull();
            result.Result.Should().NotBeNull();

            var errorResult = result.Result.As<BadRequestObjectResult>();
            errorResult.Should().NotBeNull();
            errorResult.StatusCode.Should().Be(400);
            errorResult.Value.As<string>().Should().Contain("'User-Agent'");
        }
    }
}
