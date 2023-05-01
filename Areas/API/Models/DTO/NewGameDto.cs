﻿using System.ComponentModel.DataAnnotations;

namespace Highscore.Areas.API.Models.DTO;

public class NewGameDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Uri ImageUrl { get; set; }

    [MaxLength(50)]
    public string? UrlSlug { get; set; }
}
