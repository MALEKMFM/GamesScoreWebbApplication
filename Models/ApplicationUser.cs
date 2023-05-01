using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Highscore.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(250)]
    public string? Address { get; set; }

}

