
using AutoFixture;

using FluentAssertions;

using GamingApi.WebApi.Contracts.Interfaces;
using GamingApi.WebApi.Infrastructure.Entities;
using GamingApi.WebApi.Infrastructure.Network;

using Moq;

namespace GamingApi.WebApi.Tests.Infrastructure.External
{
    public class GameStoreAwsClientTests : BaseTests
    {
        private readonly GameStoreAwsClient _sut;

        private readonly Mock<IProxyHttpClient> _httpClientMock;

        public GameStoreAwsClientTests()
        {
            _httpClientMock = new Mock<IProxyHttpClient>();
            _sut = new GameStoreAwsClient(_httpClientMock.Object);
        }

        [Fact]
        public async Task GetAllGamesAsync_Passes()
        {
            var fakeGames = DataGenerator.CreateMany<DataGame>(200);

            _httpClientMock
                .Setup(mock => mock.GetAsync<IEnumerable<DataGame>>(It.IsAny<string>()))
                .ReturnsAsync(fakeGames);

            var result = await _sut.GetAllGamesAsync();

            result.Should().NotBeNull();
            result.Count().Should().Be(fakeGames.Count());
            result.Should().BeEquivalentTo(fakeGames);
            _httpClientMock.VerifyAll();
        }
    }
}
