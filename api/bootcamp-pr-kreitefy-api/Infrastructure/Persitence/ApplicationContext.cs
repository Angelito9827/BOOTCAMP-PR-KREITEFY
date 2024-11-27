using bootcamp_pr_kreitefy_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
           .HasOne(i => i.Role)
           .WithMany()
           .HasForeignKey(i => i.RoleId)
           .IsRequired();

            modelBuilder.Entity<History>()
           .HasOne(i => i.Song)
           .WithMany()
           .HasForeignKey(i => i.SongId)
           .IsRequired();

            modelBuilder.Entity<History>()
           .HasOne(i => i.User)
           .WithMany()
           .HasForeignKey(i => i.UserId)
           .IsRequired();

            modelBuilder.Entity<Score>()
           .HasOne(i => i.Song)
           .WithMany()
           .HasForeignKey(i => i.SongId)
           .IsRequired();

            modelBuilder.Entity<Score>()
           .HasOne(i => i.User)
           .WithMany()
           .HasForeignKey(i => i.UserId)
           .IsRequired();

            modelBuilder.Entity<Song>()
                .HasOne(i => i.Album)
                .WithMany()
                .HasForeignKey(i => i.AlbumId)
                .IsRequired();

            modelBuilder.Entity<Song>()
                .HasOne(i => i.Artist)
                .WithMany()
                .HasForeignKey(i => i.ArtistId)
                .IsRequired();

            modelBuilder.Entity<Song>()
                .HasOne(i => i.Style)
                .WithMany()
                .HasForeignKey(i => i.StyleId)
                .IsRequired();
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
    }
}
