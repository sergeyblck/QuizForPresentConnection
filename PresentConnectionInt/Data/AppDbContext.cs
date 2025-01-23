using Microsoft.EntityFrameworkCore;
using PresentConnectionInt.Models;

namespace PresentConnectionInt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<HighScore> HighScores { get; set; }
    }

}
