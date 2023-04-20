using GamingApi.WebApi.Contracts.Interfaces;
using GamingApi.WebApi.Contracts.Interfaces.Services;
using GamingApi.WebApi.Contracts.Interfaces.Stores;
using GamingApi.WebApi.Contracts.Exceptions;
using GamingApi.WebApi.Infrastructure.Entities;

using Yld.GamingApi.WebApi.ApiContracts;
using Yld.GamingApi.WebApi.Core.Extensions;

namespace GamingApi.WebApi.Core.Services
{
    public sealed class GameService : IGameService<GamesResponse>
    {
        private readonly IGamesStore<DataGame> _gamesStore;

        private readonly IMapper<DataGame, GameResponse> _dataToApiMapper;

        public GameService(
            IMapper<DataGame, GameResponse> dataToApiMapper,
            IGamesStore<DataGame> gamesStore)
        {
            _dataToApiMapper = dataToApiMapper.ThrowIfNull();
            _gamesStore = gamesStore.ThrowIfNull();
        }

        public async Task<GamesResponse> GetPaginatedGamesAsync(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new InvalidInputException(
                    $"Offset '{offset}' is invalid, it must be a positive number.",
                    nameof(offset));
            }

            if (limit < 2 || limit > 10)
            {
                throw new InvalidInputException(
                    $"Limit '{limit}' is invalid, it must be a number between 2 and 10.",
                    nameof(limit));
            }

            var allGames = await _gamesStore.GetAllGamesAsync()
                ?? throw new InternalServerErrorException("Error getting the Steam Games repository response.");

            allGames = allGames!
                .OrderByDescending(dataGame => dataGame.ReleaseDate)
                .Skip(offset)
                .Take(limit);

            if (allGames?.Any() == false)
            {
                throw new NotFoundException("No games were found.");
            }

            return new GamesResponse
            {
                Items = allGames!
                    .Select(dataGame => _dataToApiMapper.Map(dataGame))
                    .AsEnumerable(),
            };
        }
    }
}
