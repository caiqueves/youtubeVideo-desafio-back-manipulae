using Microsoft.EntityFrameworkCore;
using YoutubeVideo.Domain.Entities;

namespace YoutubeVideo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração personalizada (se necessário)
            modelBuilder.Entity<Video>()
                .ToTable("Videos")
                .HasKey(v => v.Id);
        }
    }
}
