using GamingApi.WebApi.Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

using Yld.GamingApi.WebApi.ApiContracts;
using Yld.GamingApi.WebApi.Constants;
using Yld.GamingApi.WebApi.Core.Extensions;
using Yld.GamingApi.WebApi.Extensions;

namespace Yld.GamingApi.WebApi.Controllers;

[ApiController]
[Route("api/games")]
[Produces("application/json")]
public sealed class GamesController : ControllerBase
{
    private readonly IGameService<GamesResponse> _gameService;

    public GamesController(IGameService<GamesResponse> gameService)
    {
        _gameService = gameService.ThrowIfNull();
    }

    [HttpGet]
    // We use attribute routing so there's no unexpected breaking changes to the API because of a name change or other refactoring.
    [Route("")]
    public async Task<ActionResult<GamesResponse>> GetAllGames([FromQuery] int offset, [FromQuery] int limit = 2)
    {
        if (string.IsNullOrWhiteSpace(Request.GetUserAgent()))
        {
            return BadRequest($"The header '{Headers.UserAgent}' is required");
        }

        if (limit > 10)
        {
            return BadRequest($"{nameof(limit)} must not exceed 10");
        }

        if (offset < 0)
        {
            offset = 0;
        }

        var gamesResponse = await _gameService.GetPaginatedGamesAsync(offset, limit);

        return Ok(gamesResponse);
    }
}
