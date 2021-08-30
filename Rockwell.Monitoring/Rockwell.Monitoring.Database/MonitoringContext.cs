using Microsoft.EntityFrameworkCore;
using Rockwell.Monitoring.Database.Entities;

namespace Rockwell.Monitoring.Database
{
    public class MonitoringContext : DbContext
    {
        public MonitoringContext()
            : base()
        {
        }

        public MonitoringContext(DbContextOptions<MonitoringContext> options)
            : base(options)
        {
        }

        public DbSet<ExecutionResult> ExecutionResults { get; set; }
        public DbSet<ScrapeJob> ScrapeJobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL("server=monitoring.cdf0gtfypn3q.us-east-2.rds.amazonaws.com;database=monitoring;user=admin;password=admin123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScrapeJob>()
                .HasIndex(st => new { st.Url, st.CronExpression }).IsUnique();
        }
    }
}
