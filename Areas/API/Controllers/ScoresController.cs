using Highscore.Areas.API.Models.DTO;
using Highscore.Data;
using Highscore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Highscore.Areas.API.Controllers;
[Area("Api")]
[Route("api/[controller]")]
[ApiController]
public class ScoresController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ScoresController(ApplicationContext context)
    {
        this._context = context;
    }

    [HttpPost]
    public IActionResult CreateScore(NewScoreDto newScoreDto)
    {
        var score = new Score
        {
            GameId = newScoreDto.GameId,
            HighscoreDate = newScoreDto.HighscoreDate,
            PlayerName = newScoreDto.PlayerName,
            Points = newScoreDto.Points,
        };

        _context.Highscores.Add(score);
        _context.SaveChanges();

        var scoreDto = ToScoreDto(score);

        return Created("", scoreDto);
    }

    private ScoreDto ToScoreDto(Score score)
   => new ScoreDto
   {
       Id = score.Id,
       Game = score.Game,
       HighscoreDate = score.HighscoreDate,
       PlayerName = score.PlayerName,
       Points = score.Points,
       GameId = score.GameId,
   };
}
