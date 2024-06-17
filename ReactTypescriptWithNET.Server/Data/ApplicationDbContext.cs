using Microsoft.EntityFrameworkCore;
using ReactTypescriptWithNET.Server.Models;

namespace ReactTypescriptWithNET.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Forecast> Forecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Forecast>().HasData(
                new Forecast { Id = 1, Date = DateTime.Now.AddDays(1), TemperatureC = 25, Summary = "Mild" },
                new Forecast { Id = 2, Date = DateTime.Now.AddDays(2), TemperatureC = 20, Summary = "Cool" },
                new Forecast { Id = 3, Date = DateTime.Now.AddDays(3), TemperatureC = 30, Summary = "Warm" }
            );
        }
    }
}
