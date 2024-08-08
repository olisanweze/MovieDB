using Microsoft.EntityFrameworkCore;
using MovieDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MovieDB.DAL
{
    public class MovieDBContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviePlayList> MoviePlayLists { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Movie
            modelBuilder.Entity<Movie>()
                .HasKey(m => m.movieID);

            modelBuilder.Entity<Movie>()
                .Property(m => m.title)
                .IsRequired()
                .HasMaxLength(2000);

            modelBuilder.Entity<Movie>()
                .Property(m => m.year)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(m => m.rating)
                .HasDefaultValue(0);

            modelBuilder.Entity<Movie>()
                .Property(m => m.plot)
                .HasMaxLength(2000);

            modelBuilder.Entity<Movie>()
                .Property(m => m.poster)
                .HasMaxLength(2000);

            //moviePlayList
            modelBuilder.Entity<MoviePlayList>()
                .HasKey(mp => new { mp.PlayListId, mp.MovieId });

            modelBuilder.Entity<MoviePlayList>()
                .Property(m => m.MovieName)
                .IsRequired()
                .HasMaxLength(200);

            //playList
            modelBuilder.Entity<PlayList>()
                .HasKey(pl => pl.PlayListId);

            modelBuilder.Entity<PlayList>()
                .Property(pl => pl.PlayListName)
                .IsRequired()
                .HasMaxLength(200);

            //review
            modelBuilder.Entity<Review>()
                .HasKey(m => m.ReviewId);

            modelBuilder.Entity<Review>()
                .Property(r => r.MovieName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Review>()
                .Property(r => r.UserName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Review>()
                .Property(r => r.Star)
                .IsRequired()
                .HasDefaultValue(0);

            modelBuilder.Entity<Review>()
                .Property(r => r.Comment)
                .HasMaxLength(2000);

            //user
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            // relationships

            modelBuilder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(p => p.Reviews)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(u => u.Movie)
                .WithMany(p => p.Reviews)
                .HasForeignKey(u => u.movieID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
