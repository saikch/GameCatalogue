using Microsoft.EntityFrameworkCore;
using GamesCatalogue.Application.Entities;

namespace GamesCatalogue.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VideoGame> VideoGames => Set<VideoGame>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Platform)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Genre)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.ReleaseDate)
                      .IsRequired(false);

                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.IsAvailable)
                      .HasDefaultValue(true);
            });
        }
    }
}
