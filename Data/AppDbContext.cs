using Microsoft.EntityFrameworkCore;

namespace EventSourcingExample.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEntity>().ToTable("Events");
        }
    }
}
