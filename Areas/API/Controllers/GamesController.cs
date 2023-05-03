using Highscore.Areas.API.Models.DTO;
using Highscore.Data;
using Highscore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;

namespace Highscore.Areas.API.Controllers;

[Area("Api")]
[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly ApplicationContext _context;

    public GamesController(ApplicationContext context)
    {
        this._context = context;
    }

    public IEnumerable<GameDto> GetGames([FromQuery] string? name)
    {
        var games = name is null
        ? _context.Games.ToList()
        : _context.Games.Where(x => x.Name.Contains(name)).ToList();

        var gamesDtos = games.Select(ToGamesDto);

        return gamesDtos;
    }

    [HttpGet("{id}")]
    public ActionResult<GameDto> GetGame(int id)
    {
        var game =
           _context.Games.FirstOrDefault(x => x.Id == id);

        if (game is null)
        {
            // Returnera 404 Not Found om produkten inte hittades
            return NotFound();
        }

        var gameDto = ToGamesDto(game);

        return gameDto;
    }

    [HttpPost]
    public IActionResult CreateGame(NewGameDto newGameDto)
    {
        var game = new Game
        {
            Description = newGameDto.Description,
            Name = newGameDto.Name,
            ImageUrl = newGameDto.ImageUrl,
            ReleaseDate = newGameDto.ReleaseDate,
        };



        game.UrlSlug = game.Name
            .Replace("-", "")
            .Replace(" ", "-")
            .ToLower();

        _context.Games.Add(game);
        _context.SaveChanges();

        var gameDto = ToGamesDto(game);

        return Created("", gameDto);
    }

    private GameDto ToGamesDto(Game game)
    => new GameDto
    {
        HighScores = game.HighScores,
        Id = game.Id,
        Name = game.Name,
        Description = game.Description,
        ReleaseDate = game.ReleaseDate,
        ImageUrl = game.ImageUrl,
        UrlSlug = game.UrlSlug,
    };
}

