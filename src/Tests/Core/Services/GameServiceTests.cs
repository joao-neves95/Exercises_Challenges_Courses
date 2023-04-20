using AutoFixture;

using FluentAssertions;

using GamingApi.WebApi.Contracts.Interfaces;
using GamingApi.WebApi.Contracts.Interfaces.Stores;
using GamingApi.WebApi.Contracts.Exceptions;
using GamingApi.WebApi.Core.Services;
using GamingApi.WebApi.Infrastructure.Entities;

using Moq;

using Yld.GamingApi.WebApi.ApiContracts;

namespace GamingApi.WebApi.Tests.Core.Services
{
    public class GameServiceTests : BaseTests
    {
        private const int _dataGamesCount = 200;

        private readonly GameService _sut;

        private readonly Mock<IMapper<DataGame, GameResponse>> _dataToApiMapperMock;
        private readonly Mock<IGamesStore<DataGame>> _gamesStoreMock;
        private readonly IEnumerable<DataGame> _fakeDataGames;

        public GameServiceTests()
        {
            _dataToApiMapperMock = new Mock<IMapper<DataGame, GameResponse>>();
            _dataToApiMapperMock
                .Setup(mock => mock.Map(It.IsAny<DataGame>()))
                .Returns((DataGame dataGame) => MapDataToApi(dataGame));

            _fakeDataGames = DataGenerator.CreateMany<DataGame>(_dataGamesCount);

            _gamesStoreMock = new Mock<IGamesStore<DataGame>>();
            _gamesStoreMock
                .Setup(mock => mock.GetAllGamesAsync())
                .ReturnsAsync(_fakeDataGames);

            _sut = new GameService(_dataToApiMapperMock.Object, _gamesStoreMock.Object);
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_LimitResponse()
        {
            const int limit = 7;

            var result = await _sut.GetPaginatedGamesAsync(0, limit);

            result.Should().NotBeNull();
            ValidateResultCount(result, limit);

            result?.Items?
                .Should()
                .BeEquivalentTo(MapDataToApi(_fakeDataGames.OrderByDescending(game => game.ReleaseDate).Take(limit)));

            _gamesStoreMock.VerifyAll();
            _dataToApiMapperMock.VerifyAll();
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_PaginateResponse()
        {
            const int limit = 8;
            const int itemNum = 32;

            var result = await _sut.GetPaginatedGamesAsync(itemNum, limit);

            result.Should().NotBeNull();
            ValidateResultCount(result, limit);

            result.Items
                .Should()
                .BeEquivalentTo(MapDataToApi(_fakeDataGames.OrderByDescending(game => game.ReleaseDate).Skip(itemNum).Take(limit)));

            _gamesStoreMock.VerifyAll();
            _dataToApiMapperMock.VerifyAll();
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_ReturnNotFoundIfOutOfRange()
        {
            var action = () => _sut.GetPaginatedGamesAsync(500, 10);

            await action.Should().ThrowExactlyAsync<NotFoundException>();
            _gamesStoreMock.VerifyAll();
            _dataToApiMapperMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_Throw_WhenInputOffsetNegative()
        {
            var action = () => _sut.GetPaginatedGamesAsync(-1, 10);

            await action.Should().ThrowExactlyAsync<InvalidInputException>();
            _gamesStoreMock.VerifyNoOtherCalls();
            _dataToApiMapperMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_Throw_WhenInputLimitOutOfRange()
        {
            var action = () => _sut.GetPaginatedGamesAsync(0, 15);

            await action.Should().ThrowExactlyAsync<InvalidInputException>();
            _gamesStoreMock.VerifyNoOtherCalls();
            _dataToApiMapperMock.VerifyNoOtherCalls();
        }

        private static void ValidateResultCount(GamesResponse response, int expectedCount)
        {
            response.Items.Should().NotBeNull();
            response?.Items?.Count().Should().Be(expectedCount);
            response?.TotalItems.Should().Be(expectedCount);
        }

        private static IEnumerable<GameResponse> MapDataToApi(IEnumerable<DataGame> dataGames)
        {
            foreach (var dataGame in dataGames)
            {
                yield return MapDataToApi(dataGame);
            }
        }

        private static GameResponse MapDataToApi(DataGame dataGame)
        {
            return new GameResponse()
            {
                Id = dataGame.AppId,
                Name = dataGame.Name,
                ShortDescription = dataGame.ShortDescription,
                Publisher = dataGame.Publisher,
                ReleaseDate = dataGame.ReleaseDate,
                RequiredAge = dataGame.RequiredAge,
                Platforms = new PlatformsResponse()
                {
                    Linux = dataGame.Platforms.Linux,
                    Mac = dataGame.Platforms.Mac,
                    Windows = dataGame.Platforms.Windows,
                },
                Genre = dataGame.Genre,
                Categories = dataGame.Categories,
            };
        }
    }
}
