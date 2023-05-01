using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Highscore.Models;
public class Score
{
    public int Id { get; set; }

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
