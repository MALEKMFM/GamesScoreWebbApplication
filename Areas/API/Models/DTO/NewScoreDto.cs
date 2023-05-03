using Highscore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Highscore.Areas.API.Models.DTO;

public class NewScoreDto
{
    public int GameId { get; set; }
    public virtual GameDto? GameDto { get; set; }
    public string PlayerName { get; set; }
    public DateTime HighscoreDate { get; set; }
    public int Points { get; set; }
}
