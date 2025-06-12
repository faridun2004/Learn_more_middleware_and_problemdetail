using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegisterService.Entity;

namespace RegisterService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<DateTimeToUtcConverter>();
        }

        private class DateTimeToUtcConverter : ValueConverter<DateTime, DateTime>
        {
            public DateTimeToUtcConverter()
                : base(
                    v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            { }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "JohnDoe",
                    Email = "john@example.com",
                    BirthDate = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc)
                },
                new User
                {
                    Id = 2,
                    Username = "JaneDoe",
                    Email = "jane@example.com",
                    BirthDate = DateTime.SpecifyKind(new DateTime(1985, 5, 15), DateTimeKind.Utc)
                }
            );
        }
    }
}
