using Highscore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>(entity =>
            entity.HasIndex(x => x.UrlSlug).IsUnique());
        }

        public DbSet<Score> Highscores { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
