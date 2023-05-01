using Highscore.Data;
using Highscore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Controllers;

public class HighScoresController : Controller
{
    private readonly ApplicationContext _context;

    public HighScoresController(ApplicationContext context)
    {
        this._context = context;
    }

    [Authorize]
    public IActionResult Create()
    {
        var games = _context.Games.ToList();
        ViewData["GameId"] = new SelectList(games, "Id", "Name");
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Score highScores)
    {
        if (ModelState.IsValid)
        {
            var game = _context.Games.FirstOrDefault(x => x.Id == highScores.GameId);  

            highScores.Game = game; 

            _context.Highscores.Add(highScores);
            _context.SaveChanges();
            return Redirect("/Home/Index");
        }
        return View(highScores);
    }
}