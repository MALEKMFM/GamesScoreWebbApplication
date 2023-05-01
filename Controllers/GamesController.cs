using Microsoft.AspNetCore.Mvc;
using Highscore.Models;
using Highscore.Data;

namespace Highscore.Controllers;
public class GamesController : Controller
{
    private readonly ApplicationContext _db;

    public GamesController(ApplicationContext db)
    {
        this._db = db;
    }

    [Route("games/{urlSlug}")]
    public IActionResult Details(string urlSlug)
    {
        var game = _db.Games.FirstOrDefault(x => x.UrlSlug == urlSlug);

        var scores = _db.Highscores
            .Where(x => x.GameId == game.Id)
            .OrderByDescending(x => x.Points)
            .ToList();

        var viewModel = new GameDetailsViewModel
        {
            Game = game,
            Scores = scores
        };

        return View(viewModel);
    }
}

public class GameDetailsViewModel
{
    public Game Game { get; set; }
    public List<Score> Scores { get; set; }
}