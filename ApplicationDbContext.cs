using Microsoft.EntityFrameworkCore;

namespace BugReportModule
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BugReport> BugReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("Host=db;Database=postgres;Username=postgres;Password=password");
    }
}