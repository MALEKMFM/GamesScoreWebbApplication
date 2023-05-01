using Highscore.Areas.API.Models.DTO;
using Highscore.Data;
using Highscore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{Name}")]
    public ActionResult<IEnumerable<GameDto>>GetSearchedGame(string name)
    {
        var searchedGames = _context.Games.Where(x => x.Name == name).ToList();
        if (searchedGames is null)
        {
            return NotFound();
        }

        var searchedGameDtos = searchedGames.Select(game => new GameDto
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
        }).ToList();

        return searchedGameDtos;
    }

    [HttpPost]
    public IActionResult CreateHighscore(NewScoreDto newScoreDto)
    {
        var score = new Score
        {
            GameId = newScoreDto.GameId,
            Game = newScoreDto.Game,
            HighscoreDate = newScoreDto.HighscoreDate,
            PlayerName = newScoreDto.PlayerName,
            Points = newScoreDto.Points,
        };

        _context.Highscores.Add(score);
        _context.SaveChanges();

        var scoreDto = new ScoreDto
        {
            Id = score.Id,
            GameId = score.GameId,
            Game = score.Game,
            HighscoreDate = score.HighscoreDate,
            PlayerName = score.PlayerName,
            Points = score.Points,
        };

        return Created("", scoreDto);
    }

}
