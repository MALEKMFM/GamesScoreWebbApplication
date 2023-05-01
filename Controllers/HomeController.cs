using Highscore.Data;
using Highscore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Controllers;
public class HomeController : Controller
{

    private readonly ApplicationContext _context;

    public HomeController(ApplicationContext context)
    {
        this._context = context;
    }
    public IActionResult Index()
    {
        var highScores = _context.Highscores
          .Include(x => x.Game)
          .GroupBy(x => x.Game)
          .Select(x => new Score
          {
              Id = x.First().Id,
              GameId = x.Key.Id,
              Points = x.Max(y => y.Points),
              HighscoreDate = x.OrderByDescending(y => y.Points).First().HighscoreDate,
              PlayerName = x.OrderByDescending(y => y.Points).First().PlayerName,
              Game = x.Key
          })
          .OrderByDescending(x => x.Points)
          .ToList();

        var games = _context.Games.ToList();

        var viewModel = new GameXLeaderboardModel
        {
            Games = games,
            highscores = highScores,
        };

        return View(viewModel);
    }

    public class GameXLeaderboardModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Score> highscores { get; set; }
    }
}
