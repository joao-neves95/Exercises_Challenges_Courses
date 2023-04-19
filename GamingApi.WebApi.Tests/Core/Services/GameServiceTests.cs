using AutoFixture;

using FluentAssertions;

using GamingApi.WebApi.Contracts.Interfaces.Stores;
using GamingApi.WebApi.Core.Exceptions;
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

        private readonly Mock<IGamesStore<DataGame>> _gamesStoreMock;
        private readonly IEnumerable<DataGame> _fakeDataGames;

        public GameServiceTests()
        {
            _fakeDataGames = DataGenerator.CreateMany<DataGame>(_dataGamesCount);

            _gamesStoreMock = new Mock<IGamesStore<DataGame>>();
            _gamesStoreMock
                .Setup(mock => mock.GetAllGamesAsync())
                .ReturnsAsync(_fakeDataGames);

            _sut = new GameService(_gamesStoreMock.Object);
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_LimitResponse()
        {
            const int limit = 7;

            var result = await _sut.GetPaginatedGamesAsync(1, limit);

            result.Should().NotBeNull();
            ValidateResultCount(result, limit);
            result.Items.Should().BeEquivalentTo(_fakeDataGames.Take(limit));

            _gamesStoreMock.VerifyAll();
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_PaginateResponse()
        {
            const int limit = 8;
            const int pageNumber = 4;
            const int nextIndex = limit * (4 - 1);

            var result = await _sut.GetPaginatedGamesAsync(pageNumber, limit);

            result.Should().NotBeNull();
            ValidateResultCount(result, limit);
            result.Items.Should().BeEquivalentTo(_fakeDataGames.Skip(nextIndex).Take(limit));

            _gamesStoreMock.VerifyAll();
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_Throw_WhenOffsetOutOfRange()
        {
            var action = () => _sut.GetPaginatedGamesAsync(-1, 10);

            await action.Should().ThrowExactlyAsync<InvalidInputException>();
            _gamesStoreMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetPaginatedGamesAsync_Should_Throw_WhenLimitOutOfRange()
        {
            var action = () => _sut.GetPaginatedGamesAsync(1, 15);

            await action.Should().ThrowExactlyAsync<InvalidInputException>();
            _gamesStoreMock.VerifyNoOtherCalls();
        }

        private static void ValidateResultCount(GamesResponse response, int expectedCount)
        {
            response.Items.Should().NotBeNull();
            response.Items.Count().Should().Be(expectedCount);
            response.TotalItems.Should().Be(expectedCount);
        }
    }
}
