using GamingApi.WebApi.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Yld.GamingApi.WebApi.ApiContracts;
using Yld.GamingApi.WebApi.Constants;
using Yld.GamingApi.WebApi.Core.Extensions;
using Yld.GamingApi.WebApi.Extensions;

namespace Yld.GamingApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public sealed class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService.ThrowIfNull();
    }

    public async Task<ActionResult<GamesResponse>> GetGames([FromQuery] int offset, [FromQuery] int limit = 2)
    {
        if (string.IsNullOrWhiteSpace(Request.GetUserAgent()))
        {
            return BadRequest($"The header '{Headers.UserAgent}' is required");
        }

        if (offset < 0)
        {
            offset = 0;
        }

        // TODO: Move this into a validator.
        if (limit > 10)
        {
            return BadRequest($"{nameof(limit)} must not exceed 10");
        }

        var gamesResponse = await _gameService.GetPaginatedGamesAsync(offset, limit);

        return Ok(gamesResponse);
    }
}
