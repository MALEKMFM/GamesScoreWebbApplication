using Highscore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Highscore.Areas.API.Models.DTO;

public class NewScoreDto
{

    [Display(Name = "Game")]
    public int GameId { get; set; }

    [ForeignKey("GameId")]
    public virtual Game? Game { get; set; }

    [Required]
    [Display(Name = "Player name")]
    public string PlayerName { get; set; }

    [Required]
    [Display(Name = "Date")]
    public DateTime HighscoreDate { get; set; }

    [Required]
    [Display(Name = "Points")]
    public int Points { get; set; }
}
